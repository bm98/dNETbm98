using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dNetBm98
{
  /// <summary>
  /// Some helpful user32.dll functions exposed
  /// </summary>
  public class WinUser
  {
    private const int ALT = 0xA4;
    private const int EXTENDEDKEY = 0x1;
    private const int KEYUP = 0x2;

    private const uint Restore = 9;

    // 
    [DllImport( "user32.dll", SetLastError = true, CharSet = CharSet.Unicode )]
    private static extern IntPtr FindWindow( string lpClassName, string lpWindowName );

    // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
    [DllImport( "user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode )]
    private static extern IntPtr FindWindowByCaption( IntPtr ZeroOnly, string lpWindowName );


    /// <summary>
    /// Set the Windows foreground window
    /// </summary>
    /// <param name="hWnd">A window handle</param>
    /// <returns></returns>
    [DllImport( "user32.dll" )]
    public static extern bool SetForegroundWindow( IntPtr hWnd );

    /// <summary>
    /// Send a kbd event
    /// </summary>
    /// <param name="bVk"></param>
    /// <param name="bScan"></param>
    /// <param name="dwFlags"></param>
    /// <param name="dwExtraInfo"></param>
    [DllImport( "user32.dll" )]
    public static extern void keybd_event( byte bVk, byte bScan, uint dwFlags, int dwExtraInfo );

    /// <summary>
    /// True if the window is minimized
    /// </summary>
    /// <param name="hWnd">A window handle</param>
    /// <returns>True if the window is minimized</returns>
    [DllImport( "user32.dll" )]
    [return: MarshalAs( UnmanagedType.Bool )]
    public static extern bool IsIconic( IntPtr hWnd );

    /// <summary>
    /// Set the window state of a window
    /// </summary>
    /// <param name="hWnd">A window handle</param>
    /// <param name="Msg"></param>
    /// <returns></returns>
    [DllImport( "user32.dll" )]
    private static extern int ShowWindow( IntPtr hWnd, uint Msg );

    /// <summary>
    /// Returns the window handle of the foreground window
    /// </summary>
    /// <returns>A window handle</returns>
    [DllImport( "user32.dll" )]
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
      keybd_event( (byte)ALT, 0x45, EXTENDEDKEY | KEYUP, 0 );

      SetForegroundWindow( hWnd );
    }

    /// <summary>
    /// Find a window by WindowTitle
    /// </summary>
    /// <param name="windowTitle">Window Title Caption </param>
    /// <returns>The window handle or IntPtr.Zero</returns>
    public static IntPtr FindWindow( string windowTitle ) => FindWindowByCaption( IntPtr.Zero, windowTitle );

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
