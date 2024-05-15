using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace dNetBm98.Win
{
  /// <summary>
  /// Some helpful user32.dll functions exposed
  /// </summary>
  public class WinUser
  {
    /// <summary>
    /// DLL name
    /// </summary>
    public const string LibraryName = "user32";

    internal static class Properties
    {
#if !ANSI
      public const CharSet BuildCharSet = CharSet.Unicode;
#else
        public const CharSet BuildCharSet = CharSet.Ansi;
#endif
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public const uint KEYDOWN = 0x0;
    public const uint EXTENDEDKEY = 0x1;
    public const uint KEYUP = 0x2;

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    private const uint Restore = 9;

    // 
    [DllImport( LibraryName, CharSet = Properties.BuildCharSet, SetLastError = true )]
    private static extern IntPtr FindWindow( string lpClassName, string lpWindowName );

    // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
    [DllImport( "user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode )]
    private static extern IntPtr FindWindowByCaption( IntPtr ZeroOnly, string lpWindowName );


    /// <summary>
    /// Set the Windows foreground window
    /// </summary>
    /// <param name="hWnd">A window handle</param>
    /// <returns></returns>
    [DllImport( LibraryName, CharSet = Properties.BuildCharSet, SetLastError = true )]
    public static extern bool SetForegroundWindow( IntPtr hWnd );

    /// <summary>
    /// True if the window is minimized
    /// </summary>
    /// <param name="hWnd">A window handle</param>
    /// <returns>True if the window is minimized</returns>
    [DllImport( LibraryName, CharSet = Properties.BuildCharSet, SetLastError = true )]
    [return: MarshalAs( UnmanagedType.Bool )]
    public static extern bool IsIconic( IntPtr hWnd );

    /// <summary>
    /// Set the window state of a window
    /// </summary>
    /// <param name="hWnd">A window handle</param>
    /// <param name="Msg"></param>
    /// <returns></returns>
    [DllImport( LibraryName, CharSet = Properties.BuildCharSet, SetLastError = true )]
    private static extern int ShowWindow( IntPtr hWnd, uint Msg );

    /// <summary>
    /// Returns the window handle of the foreground window
    /// </summary>
    /// <returns>A window handle</returns>
    [DllImport( LibraryName, CharSet = Properties.BuildCharSet, SetLastError = true )]
    public static extern IntPtr GetForegroundWindow( );

    /// <summary>
    /// Restores a window from minimized
    /// </summary>
    /// <param name="hWnd">A window handle</param>
    public static void RestoreWindow( IntPtr hWnd ) => ShowWindow( hWnd, Restore );


    /// <summary>
    /// Try to force a window into foreground
    /// </summary>
    /// <param name="hWnd">A window handle</param>
    public static void ForceForegroundWindow( IntPtr hWnd )
    {
      // Seems to work... when sending ALT up only
      // https://stackoverflow.com/questions/10740346/setforegroundwindow-only-working-while-visual-studio-is-open/13881647#13881647

      // sanity
      if (hWnd == IntPtr.Zero) return;

      //check if already has focus
      if (hWnd == GetForegroundWindow( )) return;

      //check if window is minimized, then restore it
      if (IsIconic( hWnd )) {
        ShowWindow( hWnd, Restore );
      }

      // Simulate an ALT key release
      SendKey( Keys.LMenu, false );
      //keybd_event( Key_ALT, 0x45, EXTENDEDKEY | KEYUP, IntPtr.Zero );

      SetForegroundWindow( hWnd );
    }


    /// <summary>
    /// Find a window by WindowTitle
    /// </summary>
    /// <param name="windowTitle">Window Title Caption </param>
    /// <returns>The window handle or IntPtr.Zero</returns>
    public static IntPtr FindWindow( string windowTitle ) => FindWindowByCaption( IntPtr.Zero, windowTitle );


    // WindowInfo record
    private class WindowInfo
    {
      public string WindowTitle;
      public IntPtr Hwnd;
    }


    // get the window list
    private static List<WindowInfo> WindowInfoList( )
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


    #region KEYBOARD Handling

    private static List<WindowInfo> _windowInfos = new List<WindowInfo>( );

    /// <summary>
    /// Send a kbd event to the active window
    /// </summary>
    [DllImport( LibraryName, CharSet = Properties.BuildCharSet, SetLastError = true )]
    internal static extern void keybd_event( byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo );


    /// <summary>
    /// Send a Key to the active Window
    /// </summary>
    /// <param name="vk">A Winforms.Keys Key</param>
    /// <param name="press">True to press, else release</param>
    public static void SendKey( Keys vk, bool press )
    {
      SendKey( (byte)vk, press );
    }

    /// <summary>
    /// Send a Key to the active Window
    /// </summary>
    /// <param name="key">A virtual key code</param>
    /// <param name="press">True to press, else release</param>
    public static void SendKey( byte key, bool press )
    {
      uint flag = press ? KEYDOWN : KEYUP;
      uint scanCode = GetScanCode( key );
      flag |= ExtendedFlag( scanCode );
      keybd_event( key, (byte)(scanCode & 0xff), flag, IntPtr.Zero );
    }

    /// <summary>
    /// Send a Key to the Window with Title
    /// </summary>
    /// <param name="vk">A Winforms.Keys Key</param>
    /// <param name="press">True to press, else release</param>
    /// <param name="windowTitle">Window to send strokes to</param>
    public static void SendKey( Keys vk, bool press, string windowTitle )
    {
      SendKey( (byte)vk, press, windowTitle );
    }

    /// <summary>
    /// Send a Key to the Window with Title
    /// </summary>
    /// <param name="key">A virtual key code</param>
    /// <param name="press">True to press, else release</param>
    /// <param name="windowTitle">Window to send strokes to</param>
    public static void SendKey( byte key, bool press, string windowTitle )
    {
      if (string.IsNullOrEmpty( windowTitle )) return;

      if (_windowInfos.Count == 0) {
        // rebuild if empty
        _windowInfos = WindowInfoList( );
      }
      var wInfo = _windowInfos.FirstOrDefault( w => w.WindowTitle.Contains( windowTitle.ToLowerInvariant( ) ) );
      if (wInfo == null) {
        // rebuild once if not found
        _windowInfos = WindowInfoList( );
      }
      wInfo = _windowInfos.FirstOrDefault( w => w.WindowTitle.Contains( windowTitle.ToLowerInvariant( ) ) );
      if (wInfo == null) return; // no such window


      // make sure we send it to the intended window
      if (!WinUser.GetForegroundWindow( ).Equals( wInfo.Hwnd )) {
        WinUser.ForceForegroundWindow( wInfo.Hwnd );
      }
      uint flag = press ? KEYDOWN : KEYUP;
      uint scanCode = GetScanCode( key );
      flag |= ExtendedFlag( scanCode );
      keybd_event( key, (byte)(scanCode & 0xff), flag, IntPtr.Zero );
    }

    /// <summary>
    /// Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a virtual-key code.
    /// </summary>
    /// <returns>The return value is either a scan code, a virtual-key code, or a character value, depending on the value of uCode and uMapType. If there is no translation, the return value is zero.</returns>
    [DllImport( LibraryName, CharSet = Properties.BuildCharSet, SetLastError = true )]
    internal static extern uint MapVirtualKey( uint uCode, uint uMapType );

    /// <summary>
    /// Translates (maps) a virtual-key code into a scan code or character value, 
    /// or translates a scan code into a virtual-key code. 
    /// The function translates the codes using the input language and an input locale identifier.
    /// 
    /// NOTE: DX Keycodes are VSC codes (Scan Codes)
    /// </summary>
    /// <param name="uCode">Scan code for a key. 
    /// Starting with Windows Vista, the high byte of the uCode value can contain 
    /// either 0xe0 or 0xe1 to specify the extended scan code.
    /// </param>
    /// <param name="uMapType">MAPVK_VSC_TO_VK, MAPVK_VSC_TO_VK_EX</param>
    /// <param name="dwhkl">nput locale identifier to use for translating the specified code.</param>
    /// <returns>Either a scan code, a virtual-key code, or a character value, 
    /// depending on the value of uCode and uMapType. 
    /// If there is no translation, the return value is zero.
    /// </returns>
    [DllImport( LibraryName, CharSet = Properties.BuildCharSet )]
    internal static extern uint MapVirtualKeyEx( uint uCode, VirtualKeyMapType uMapType, IntPtr dwhkl );

    /// <summary>
    /// Returns a ScanCode for the Virtual Key
    /// </summary>
    /// <param name="vk">Virtual Key</param>
    /// <returns>A scan code</returns>
    /// 
    /// <remarks>The uCode parameter is a scan code and is translated into a virtual-key code that distinguishes between left- and right-hand keys. 
    /// If there is no translation, the function returns 0.
    /// Windows Vista and later: the high byte of the uCode value can contain either 0xe0 or 0xe1 to specify the extended scan code.
    /// </remarks>
    internal static uint GetScanCode( byte vk )
    {
      var result = MapVirtualKey( (uint)vk, (uint)VirtualKeyMapType.MAPVK_VK_TO_VSC_EX );

      return result;
    }

    private const uint c_E0Flag = 0xe000_0000;
    private const uint c_E1Flag = 0xe100_0000;

    /// <summary>
    /// Returns the Extended Flag for Scancodes
    /// </summary>
    /// <param name="scanCode">A ScanCode</param>
    /// <returns>A flag</returns>
    internal static uint ExtendedFlag( uint scanCode )
    {
      if ((scanCode & c_E1Flag) > 0) {
        return EXTENDEDKEY;
      }
      else if ((scanCode & c_E0Flag) > 0) {

      }
      return 0;
    }

    #endregion

    #region Push Pop Ops

    // use for push pop operations
    private static readonly ConcurrentStack<IntPtr> _windowStack = new ConcurrentStack<IntPtr>( );

    /// <summary>
    /// Pushes the currently active Window on the stack and makes the hwnd one active
    /// Use PopForeground() when done
    /// </summary>
    /// <param name="hWnd">A Window handle</param>
    public static void PushAndSetForeground( IntPtr hWnd )
    {
      // sanity
      if (hWnd == IntPtr.Zero) return;

      IntPtr curr_hWnd = GetForegroundWindow( );
      if (curr_hWnd != IntPtr.Zero) { _windowStack.Push( curr_hWnd ); }
      ForceForegroundWindow( hWnd );
    }

    /// <summary>
    /// Pops the last foreground window from the stack and makes it the new foreground window
    /// </summary>
    public static void PopForeground( )
    {
      if (_windowStack.TryPop( out IntPtr hWnd )) {
        ForceForegroundWindow( hWnd );
      }
    }

    #endregion

    #region WinForms specific

    // return the window handle of a form or controls form
    private static IntPtr WinFormHWND( Control control )
    {
      // sanity 
      if (control == null) return IntPtr.Zero;

      if (control is Form) {
        return (control as Form).Handle;
      }
      else {
        return control.FindForm( ).Handle;
      }
    }

    /// <summary>
    /// Activates the Main Form in order to prevent that further (Mouse) events are sent to the prev. Active Application
    /// </summary>
    public static void SetForeground( Control control )
    {
      ForceForegroundWindow( WinFormHWND( control ) );
    }

    /// <summary>
    /// Activates the Main Form in order to prevent that further (Mouse) events are sent to the prev. Active Application
    /// Sets the mouse event as handled
    /// </summary>
    /// <param name="control"></param>
    /// <param name="e">The MouseEvents</param>
    public static void SetForeground( Control control, MouseEventArgs e )
    {
      SetForeground( control );
      (e as HandledMouseEventArgs).Handled = true; // don't bubble up the scroll wheel
    }

    /// <summary>
    /// Pushes the currently active Window on the stack and makes the hwnd one active
    /// Use PopForeground() when done
    /// </summary>
    /// <param name="control">A WinForms Control or Form</param>
    public static void PushAndSetForeground( Control control )
    {
      PushAndSetForeground( WinFormHWND( control ) );
    }

    #endregion

  }
}
