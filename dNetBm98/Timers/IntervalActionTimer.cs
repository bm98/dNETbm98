using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace dNetBm98.Timers
{
  /// An interval timer, fires event after each interval elapsed
  ///  and performs an action when elapsed
  ///  Note: the Elapsed flag will remain true after the first event until Stopped
  public class IntervalActionTimer : IntervalTimer
  {
    /// <summary>
    /// Action to be performed when elapsed
    /// </summary>
    protected Action _action;

    /// <summary>
    /// cTor: an IntervalTimer for an interval with Action
    /// </summary>
    /// <param name="intervalSec">Interval in seconds</param>
    /// <param name="action">Action when elapsed</param>
    public IntervalActionTimer( double intervalSec, Action action )
      : base( intervalSec )
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
