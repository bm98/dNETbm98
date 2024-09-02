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

    private readonly BlockingQueue<JobObjBase> _jobQueue = null;
    private readonly Task[] _task = null;
    private readonly CancellationTokenSource _cancellationTokenSource = null;
    private readonly CancellationToken _token;

    private bool _isRunning = false;

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="numThreads">Number of parallel threads to run (default=1, max=10)</param>
    public JobRunner( int numThreads = 1 )
    {
      // sanity
      int nThreads = numThreads > 0 ? numThreads : (numThreads <= 10) ? numThreads : 1;

      _task = new Task[nThreads];
      _jobQueue = new BlockingQueue<JobObjBase>( );

      _cancellationTokenSource = new CancellationTokenSource( );
      _token = _cancellationTokenSource.Token;
      _token.ThrowIfCancellationRequested( );
    }

    // start or restart the task if it is not yet created or terminated
    // the task will run until disposed
    private void StartJobRunner( )
    {
      if (_task[0] == null) {
        // lazy start
        for (int i = 0; i < _task.Length; i++) {
          _task[i] = Task.Run( ( ) => { JobRunnerTask( i ); }, _token );
        }
        _isRunning = true;
      }
      if (_isRunning) {
        for (int i = 0; i < _task.Length; i++) {
          if (_task[i].IsFaulted) {
            // restart if no longer running
            _task[i].Dispose( );
            _task[i] = Task.Run( ( ) => { JobRunnerTask( i ); }, _token );
            Console.WriteLine( $"JobRunner:  task[{i}] RESTARTED" );
          }
        }
      }
    }

    // task routine, can only be cancelled or throw while processing
    private void JobRunnerTask( int taskIndex )
    {
      var taskIdx = taskIndex;
      // ignore cancels and terminate
      try {
        bool done;
        do {
          // will run until cancelled or thrown
          while (_jobQueue.TryDequeue( out JobObjBase job, c_Timeout_ms, _token )) {
            Debug.WriteLine( $"JobRunner: task[{taskIdx}] {job} about to run" );
            try {
              job.DoJob( );
            }
            catch (Exception ex) {
              if (_token.IsCancellationRequested == false) {
                Console.WriteLine( $"JobRunnerTask task[{taskIdx}] job<{job.JobName}> failed with exception\n{ex}" );
              }
              else {
                throw ex; // terminate
              }
            }
          }
          //     Debug.WriteLine( "JobRunnerTask TIMEOUT" );
          done = _token.IsCancellationRequested; // check for cancel
        } while (!done);
      }
      catch (Exception ex) {
        if (_token.IsCancellationRequested == false) {
          // trigger restart if not cancelled by token
          Console.WriteLine( $"JobRunnerTask task[{taskIdx}] CANCELED" );
          throw ex;
        }
      }
    }

    /// <summary>
    /// Add a Job to be performed
    /// </summary>
    /// <param name="job">A Job</param>
    public void AddJob( JobObjBase job )
    {
      StartJobRunner( ); // latest to start the runner

      _jobQueue.Enqueue( job );

      // Monitor Size for now TODO remove or handle overloads
      if (_jobQueue.Count > 10) {
        Console.WriteLine( $"JobRunnerQueue Size {_jobQueue.Count}" );
      }
    }


    #region DISPOSE

    private bool disposedValue;

    /// <inheritdoc/>
    protected virtual void Dispose( bool disposing )
    {
      if (!disposedValue) {
        if (disposing) {
          // TODO: dispose managed state (managed objects)
          _isRunning = false;
          _cancellationTokenSource?.Cancel( );

          for (int i = 0; i < _task.Length; i++) {
            _task[i]?.Wait( );
            _task[i]?.Dispose( );
            Debug.WriteLine( $"JobRunner: task[{i}] disposed" );
          }
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
