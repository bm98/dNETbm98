using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Timers
{
  /// <summary>
  /// A simple Timer
  /// </summary>
  public class SimpleTimer : TimerBase
  {
    /// <summary>
    /// cTor: a Timer for a duration
    /// </summary>
    /// <param name="durationSec">Duration in seconds</param>
    public SimpleTimer( double durationSec )
      : base( durationSec ) { }
  }

}
