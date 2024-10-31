using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace dNetBm98.Timers
{
  /// <summary>
  /// A timer which performs an action when elapsed
  /// </summary>
  public class ActionTimer : TimerBase
  {
    /// <summary>
    /// Action to be performed when elapsed
    /// </summary>
    protected Action _action;

    /// <summary>
    /// cTor: a Timer for a duration with Action
    /// </summary>
    /// <param name="durationSec">Duration in seconds</param>
    /// <param name="action">Action when elapsed</param>
    public ActionTimer( double durationSec, Action action )
      : base( durationSec )
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
