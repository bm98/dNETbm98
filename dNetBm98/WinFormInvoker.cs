using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dNetBm98
{
  /// <summary>
  /// Handles Events in WinForms and invokes a delegate if required
  /// </summary>
  public class WinFormInvoker
  {

    private ContainerControl _cctrl;

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="cctrl">The ContainertControl to handle</param>
    public WinFormInvoker( ContainerControl cctrl )
    {
      _cctrl = cctrl;
    }

    /// <summary>
    /// Handle Events on behalf of the Form
    /// </summary>
    /// <param name="method">An parameterless method to execute</param>
    public void HandleEvent( Action method )
    {
      // sanity 
      if (_cctrl == null) return;

      if (_cctrl.InvokeRequired) {
        _cctrl.Invoke( (MethodInvoker)delegate { method( ); } );
      }
      else {
        method( );
      }
    }

  }
}
