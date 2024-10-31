using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace dNetBm98.Timers
{
  /// <summary>
  /// Base Timer
  ///  A resetable one shot timer
  /// </summary>
  public abstract class TimerBase : ITimer, IDisposable
  {
    /// <summary>
    /// Internal timer
    /// </summary>
    protected Timer _timer;
    /// <summary>
    /// Duration [sec]
    /// </summary>
    protected double _duration_Sec = 0;
    /// <summary>
    /// Elapsed flag
    /// </summary>
    protected bool _elapsed = false;

    /// <summary>
    /// Event fired when the timer has elapsed
    /// </summary>
    public event EventHandler<ElapsedEventArgs> Elapsed;
    /// <summary>
    /// Issues the public Event
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnElapsed( ElapsedEventArgs e )
    {
      _elapsed = true;
      Elapsed?.Invoke( this, e );
    }

    /// <summary>
    /// cTor: a Timer for a duration
    /// </summary>
    /// <param name="durationSec">Duration in seconds</param>
    public TimerBase( double durationSec )
    {
      // sanity
      if (durationSec < 0.001) throw new ArgumentException( "Duration must be >= 1 ms" );

      _duration_Sec = durationSec;
      _elapsed = false;
      _timer = new Timer( _duration_Sec * 1000.0 );
      _timer.Elapsed += _timer_Elapsed;
      _timer.Enabled = false;
      _timer.AutoReset = false;
    }

    // internal timer has elapsed
    private void _timer_Elapsed( object sender, ElapsedEventArgs e )
    {
      _elapsed = true;
      OnElapsed( e ); // signal
    }

    /// <summary>
    /// Stop the timer without further effects
    /// </summary>
    public virtual void Stop( )
    {
      _timer.Stop( );
      _elapsed = false;
    }

    /// <summary>
    /// Reset the timer and start again
    /// </summary>
    public virtual void Reset( )
    {
      _timer.Stop( );
      _elapsed = false;
      _timer.Start( );
    }

    /// <summary>
    /// Elapsed Flag, true when the timer has elapsed
    /// </summary>
    public virtual bool HasElapsed => _elapsed;

    #region DISPOSE

    private bool disposedValue;

    /// <inheritdoc/>
    protected virtual void Dispose( bool disposing )
    {
      if (!disposedValue) {
        if (disposing) {
          // TODO: dispose managed state (managed objects)
          _timer.Stop( );
          _elapsed = false;
          _timer.Dispose( );
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~TimerBase()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    /// <inheritdoc/>
    public void Dispose( )
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose( disposing: true );
      GC.SuppressFinalize( this );
    }

    #endregion

  }
}
