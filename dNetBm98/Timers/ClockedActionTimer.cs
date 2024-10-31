using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace dNetBm98.Timers
{
  /// <summary>
  /// Timer to fire at a specific DateTime
  ///  and performs an action when elapsed
  /// </summary>
  public class ClockedActionTimer : ClockedTimer
  {
    /// <summary>
    /// Action to be performed when elapsed
    /// </summary>
    protected Action _action;

    /// <summary>
    /// cTor: a Timer for a duration with Action
    /// </summary>
    /// <param name="action">Action when elapsed</param>
    public ClockedActionTimer( Action action )
      : base( )
    {
      _action = action;
    }

    /// <summary>
    /// Issues the public Event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnElapsed( ElapsedEventArgs e )
    {
      _action?.Invoke( );
      base.OnElapsed( e );
    }

  }
}
