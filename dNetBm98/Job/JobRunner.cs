using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Text;
using System.Collections.Concurrent;

namespace dNetBm98.Job
{
  /// <summary>
  /// Runs Jobs added asynch in a separate task
  /// After adding the first job, the runner remains active until Disposed
  /// </summary>
  public class JobRunner : IDisposable
  {
    private const int c_Timeout_ms = 1000;

    private BlockingQueue<JobObj> _jobQueue = null;
    private Task _task = null;
    private CancellationTokenSource _cancellationTokenSource = null;
    private CancellationToken _token;

    private bool _isRunning = false;

    /// <summary>
    /// cTor:
    /// </summary>
    public JobRunner( )
    {
      _jobQueue = new BlockingQueue<JobObj>( );

      _cancellationTokenSource = new CancellationTokenSource( );
      _token = _cancellationTokenSource.Token;
      _token.ThrowIfCancellationRequested( );
    }

    // start or restart the task if it is not yet created or terminated
    // the task will run until disposed
    private void StartJobRunner( )
    {
      if (_task == null) {
        _task = Task.Run( ( ) => { JobRunnerTask( ); }, _token );
        _isRunning = true;
      }
      else if (!_isRunning) {
        // restart if no longer running
        _task = Task.Run( ( ) => { JobRunnerTask( ); }, _token );
        Console.WriteLine( $"JobRunner:  RESTARTED" );
      }
    }

    // task routine, can only be cancelled or throw while processing
    private void JobRunnerTask( )
    {

      // ignore cancels and terminate
      try {
        bool done;
        do {
          // will run until cancelled or thrown
          while (_jobQueue.TryDequeue( out JobObj job, c_Timeout_ms, _token )) {
            job.DoJob( );
            Console.WriteLine( $"JobRunner:  {job} done" );
          }
          //     Debug.WriteLine( "JobRunnerTask TIMEOUT" );

          done = _token.IsCancellationRequested; // check for cancel
        } while (!done);
      }
      catch {
        Console.WriteLine( "JobRunnerTask CANCELED" );
      }
      finally {
        _isRunning = false;
      }
    }

    /// <summary>
    /// Add a Job to be performed
    /// </summary>
    /// <param name="job">A Job</param>
    public void AddJob( JobObj job )
    {
      StartJobRunner( ); // latest to start the runner

      _jobQueue.Enqueue( job );
    }


    #region DISPOSE

    private bool disposedValue;

    /// <inheritdoc/>
    protected virtual void Dispose( bool disposing )
    {
      if (!disposedValue) {
        if (disposing) {
          // TODO: dispose managed state (managed objects)
          _cancellationTokenSource?.Cancel( );
          _task?.Wait( 1000 );
          _task?.Dispose( );
          _cancellationTokenSource?.Dispose( );
          _jobQueue?.Dispose( );
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~WinJobRunnerAsync()
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
