using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Timers
{
  /// <summary>
  /// An interval timer, fires event after each interval elapsed
  ///  Note: the Elapsed flag will remain true after the first event until Stopped
  /// </summary>
  public class IntervalTimer : TimerBase
  {

    /// <summary>
    /// cTor: an IntervalTimer for an inteval
    /// </summary>
    /// <param name="intervalSec">Interval in seconds</param>
    public IntervalTimer( double intervalSec )
      : base( intervalSec )
    {
      _timer.AutoReset = true;
    }

  }
}
