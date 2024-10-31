using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace dNetBm98.Timers
{
  /// <summary>
  /// Timer Interface
  /// </summary>
  public interface ITimer
  {
    /// <summary>
    /// Event fired when the timer has elapsed
    /// </summary>
    event EventHandler<ElapsedEventArgs> Elapsed;

    /// <summary>
    /// Stop the timer without further effects
    /// </summary>
    void Stop( );

    /// <summary>
    /// Reset the timer and start again
    /// </summary>
    void Reset( );

    /// <summary>
    /// Elapsed Flag, true when the timer has elapsed
    /// </summary>
    bool HasElapsed { get; }

    /// <inheritdoc/>
    void Dispose( );


  }
}
