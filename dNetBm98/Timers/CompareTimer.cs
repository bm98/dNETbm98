using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Timers
{
  /// <summary>
  /// A timer which accepts a value to compare and resets each time the value changes
  /// </summary>
  public class CompareTimer<T> : TimerBase where T : IComparable<T>
  {

    /// <summary>
    /// Current check value
    /// </summary>
    protected T _checkValue = default;

    /// <summary>
    /// cTor: a Timer for a duration
    /// </summary>
    /// <param name="durationSec">Duration in seconds</param>

    public CompareTimer( double durationSec )
      : base( durationSec )
    {

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
      if (_checkValue.CompareTo( checkValue ) != 0) {
        _checkValue = checkValue;
        base.Reset( );
      }
    }

  }
}
