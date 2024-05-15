using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dNetBm98.Win
{
  /// <summary>
  /// Sends keyboard strokes to a target window
  /// </summary>
  public class WinKbdSender : IDisposable
  {
    // supported modifiers
    private static readonly List<Keys> c_AllowedModifiers = new List<Keys>( ) {
      Keys.LShiftKey, Keys.RShiftKey,
      Keys.LControlKey, Keys.RControlKey,
      Keys.LMenu, Keys.RMenu
    };

    #region KbdStroke Class

    /// <summary>
    /// A Keystroke
    /// </summary>
    public struct KbdStroke
    {
      private const char c_sep = '¦';

      /// <summary>
      /// True if valid
      /// </summary>
      public bool IsValid => Duration_ms > 0;

      /// <summary>
      /// Key Code to send (WinUser.Key_..
      /// </summary>
      public Keys Key { get; }

      private List<Keys> _modifiers;
      /// <summary>
      /// Modifier Keys
      /// </summary>
      public IEnumerable<Keys> Modifiers => _modifiers;

      /// <summary>
      /// The press duration in ms
      /// </summary>
      public int Duration_ms { get; }

      /// <summary>
      /// cTor: defaults to no Modifier
      /// </summary>
      public KbdStroke( Keys key, int duration_ms )
      {
        Key = key;
        Duration_ms = duration_ms;
        _modifiers = new List<Keys>( );
      }

      /// <summary>
      /// cTor: add a list of Modifiers
      /// </summary>
      public KbdStroke( Keys key, int duration_ms, Keys[] modifiers )
      {
        Key = key;
        Duration_ms = duration_ms;
        _modifiers = new List<Keys>( );
        if (modifiers != null) {
          for (int i = 0; i < modifiers.Length; i++) {
            AddModifier( modifiers[i] );
          }
        }
      }

      /// <summary>
      /// cTor: from a serialized string (by ToString())
      /// </summary>
      public KbdStroke( string strokeString )
      {
        _modifiers = new List<Keys>( );
        Key = (Keys)0;
        Duration_ms = 0; // marks Invalid

        string[] e = strokeString.Split( new char[] { c_sep }, StringSplitOptions.RemoveEmptyEntries );
        if ((e.Length > 0) && Enum.TryParse<Keys>( e[0], out var key )) { Key = key; }
        if ((e.Length > 1) && int.TryParse( e[1], out var dur )) { Duration_ms = dur; }
        for (int i = 2; i < e.Length; i++) {
          if (Enum.TryParse<Keys>( e[i], out key )) { AddModifier( key ); }
        }
      }
      /// <inheritdoc/>
      public override string ToString( )
      {
        StringBuilder sb = new StringBuilder( );
        sb.Append( $"{Key}" );
        sb.Append( $"{c_sep}{Duration_ms}" );
        foreach (var mod in Modifiers) {
          sb.Append( $"{c_sep}{mod}" );
        }
        return sb.ToString( );
      }


      /// <summary>
      /// Add a modifier
      /// </summary>
      /// <param name="mod"></param>
      public void AddModifier( Keys mod )
      {
        // sanity
        if (!c_AllowedModifiers.Contains( mod )) return;
        if (_modifiers.Contains( mod )) return; // already

        _modifiers.Add( mod );
      }

      /// <summary>
      /// Return a deep clone of this 
      /// </summary>
      /// <returns></returns>
      public KbdStroke Clone( )
      {
        return new KbdStroke( this.Key, this.Duration_ms, _modifiers.ToArray( ) );
      }

    }

    #endregion


    private const int c_maxDuration_ms = 30_000; // not more than 30 sec...
    private BackgroundWorker _bgw;
    private ConcurrentQueue<KbdStroke> _queue = new ConcurrentQueue<KbdStroke>( );
    private AutoResetEvent _blockingHelper;


    // WindowInfo record
    private class WindowInfo
    {
      public string WindowTitle;
      public IntPtr Hwnd;
    }

    // get the window list
    private List<WindowInfo> WindowInfoList( )
    {
      var l = new List<WindowInfo>( );
      Process[] processlist = Process.GetProcesses( );

      foreach (Process process in processlist) {
        if (!String.IsNullOrEmpty( process.MainWindowTitle )) {
          var wInfo = new WindowInfo {
            WindowTitle = process.MainWindowTitle.ToLowerInvariant( ),
            Hwnd = process.MainWindowHandle
          };
          l.Add( wInfo );
          //Debug.WriteLine( "Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle );
        }
      }
      return l;
    }

    /// <summary>
    /// cTor:
    /// </summary>
    public WinKbdSender( )
    {
      _bgw = new BackgroundWorker( );
      _bgw.RunWorkerCompleted += _bgw_RunWorkerCompleted;
      _bgw.DoWork += _bgw_DoWork;
      _bgw.WorkerSupportsCancellation = true;
    }

    /// <summary>
    /// Add Keystrokes to exec
    /// </summary>
    /// <param name="stroke">A Keystroke</param>
    public void AddStroke( KbdStroke stroke )
    {
      // sanity..
      if (!stroke.IsValid) return;
      if (stroke.Duration_ms > c_maxDuration_ms) return; // not allowed

      _queue.Enqueue( stroke );
    }

    /// <summary>
    /// Run all strokes added to the Window with Handle
    /// </summary>
    /// <param name="hWnd">A target window Handle</param>
    /// <param name="blocking">True when blocking is requested</param>
    /// <returns>True when about to send or sent (blocking)</returns>
    public bool RunStrokes( IntPtr hWnd, bool blocking )
    {
      // sanity
      if (hWnd == IntPtr.Zero) return false;
      if (_bgw == null) return false;
      if (_queue.Count == 0) return false;
      if (_bgw.IsBusy) return false;

      var wList = WindowInfoList( );
      var wInfo = wList.FirstOrDefault( w => w.Hwnd.Equals( hWnd ) );

      if (wInfo != null) {
        return RunTask( blocking, wInfo );
      }
      return false;
    }

    /// <summary>
    /// Run all strokes added to the Window with Title
    ///  Queried as Contains() case invariant
    /// </summary>
    /// <param name="targetWindowTitle">A target window name</param>
    /// <param name="blocking">True when blocking is requested</param>
    /// <returns>True when about to send or sent (blocking)</returns>
    public bool RunStrokes( string targetWindowTitle, bool blocking )
    {
      // sanity
      if (string.IsNullOrEmpty( targetWindowTitle )) return false;

      var wList = WindowInfoList( );
      var wInfo = wList.FirstOrDefault( w => w.WindowTitle.Contains( targetWindowTitle.ToLowerInvariant( ) ) );
      if (wInfo != null) {
        // found a window with title
        return RunStrokes( wInfo.Hwnd, blocking );
      }
      return false;
    }

    /// <summary>
    /// True when Busy sending
    /// </summary>
    public bool IsBusy => _bgw.IsBusy;

    // decompose the keystroke for Modifiers 
    private void SendKeyStroke( KbdStroke key, bool press )
    {
      if (press) {
        foreach (var mod in key.Modifiers) {
          WinUser.SendKey( mod, true );
        }
        WinUser.SendKey( key.Key, true );
      }
      else {
        WinUser.SendKey( key.Key, false );
        foreach (var mod in key.Modifiers) {
          WinUser.SendKey( mod, false );
        }
      }
    }

    // start the task to run the keyboard strokes
    // if blocking, waits until completion
    private bool RunTask( bool blocking, WindowInfo context )
    {
      // sanity
      if (_bgw.IsBusy) return false;

      _blockingHelper?.Dispose( );
      _blockingHelper = new AutoResetEvent( false );

      if (!blocking) {
        _blockingHelper.Set( ); // non blocking
      }

      _bgw.RunWorkerAsync( context );

      // will block if not Set above
      _blockingHelper.WaitOne( );

      return true;
    }


    private void _bgw_DoWork( object sender, DoWorkEventArgs e )
    {
      WindowInfo context = e.Argument as WindowInfo;

      // make sure we send it to the intended window
      if (!WinUser.GetForegroundWindow( ).Equals( context.Hwnd )) {
        WinUser.ForceForegroundWindow( context.Hwnd );
      }

      while (_queue.Count > 0) {

        if (_queue.TryDequeue( out KbdStroke cmd )) {
          if (_bgw.CancellationPending) {
            continue; // will eventually clear the queue
          }
          // some delay between strokes
          Thread.Sleep( 50 );
          // send 

          // make sure we send it to the intended window
          if (!WinUser.GetForegroundWindow( ).Equals( context.Hwnd )) {
            WinUser.ForceForegroundWindow( context.Hwnd );
          }
          SendKeyStroke( cmd, true );
          Thread.Sleep( cmd.Duration_ms );

          // make sure we send it to the intended window
          if (!WinUser.GetForegroundWindow( ).Equals( context.Hwnd )) {
            WinUser.ForceForegroundWindow( context.Hwnd );
          }
          SendKeyStroke( cmd, false );
        }
      }
      // release the blocked routine, if needed
      _blockingHelper?.Set( );
    }

    private void _bgw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
    {
      Debug.WriteLine( "FINISHED SENDING KeyStrokes" );
    }


    #region DISPOSE

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    private bool disposedValue;

    protected virtual void Dispose( bool disposing )
    {
      if (!disposedValue) {
        if (disposing) {
          // TODO: dispose managed state (managed objects)
          if (_bgw.IsBusy) {
            _bgw.CancelAsync( );
          }
          _bgw?.Dispose( );
          _blockingHelper?.Dispose( );
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~WinKbdSender()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }


    public void Dispose( )
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose( disposing: true );
      GC.SuppressFinalize( this );
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    #endregion
  }
}
