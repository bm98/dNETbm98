using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace dNetBm98.Timers
{
  /// <summary>
  /// A timer which accepts a value to compare and resets each time the value changes
  ///  and performs an action when elapsed
  /// </summary>
  public class CompareActionTimer<T> : TimerBase where T : IComparable<T>
  {
    /// <summary>
    /// Current check value
    /// </summary>
    protected T _checkValue = default;
    /// <summary>
    /// Action to be performed when elapsed
    /// </summary>
    protected Action _action;

    /// <summary>
    /// cTor: an IntervalTimer for an interval with Action
    /// </summary>
    /// <param name="durationSec">Duration in seconds</param>
    /// <param name="action">Action when elapsed</param>
    public CompareActionTimer( double durationSec, Action action )
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

    /// <summary>
    /// Stop the timer an set the check Value to default(T)
    /// </summary>
    public override void Stop( )
    {
      base.Stop( );
      _checkValue = default;
    }

    /// <summary>
    /// Reset if the check Value has changed since the last call
    /// </summary>
    /// <param name="checkValue">A value to compare the current with</param>
    public void Reset( T checkValue )
    {
      if (_elapsed.CompareTo( checkValue ) != 0) {
        _checkValue = checkValue;
        base.Reset( );
      }
    }

  }
}
