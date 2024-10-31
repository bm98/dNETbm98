using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Timers
{
  /// <summary>
  /// Timer to fire at a specific DateTime
  /// </summary>
  public class ClockedTimer : TimerBase
  {
    /// <summary>
    /// cTor: 
    /// </summary>
    public ClockedTimer( )
      : base( 10 )
    {
      // duration will be set on Reset(DateTime)
    }

    /// <summary>
    /// Use Reset with DateTime
    /// </summary>
    public override void Reset( )
    {
      ; // does not perform any action
    }

    /// <summary>
    /// Reset and start the time to fire at a specific DateTime
    ///  don't expect milliseconds precision due to processing lags
    ///  starts only if the duration is positive i.e. fire DateTime is in the future
    /// </summary>
    /// <param name="fireTime">DateTime to fire</param>
    public virtual void Reset( DateTime fireTime )
    {
      base.Stop( );

      double duration = (fireTime - DateTime.Now).TotalMilliseconds;
      if (duration > 0) {
        _timer.Interval = (fireTime - DateTime.Now).TotalMilliseconds;
        _timer.Start( );
      }
    }

  }
}
