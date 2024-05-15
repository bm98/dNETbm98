using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dNetBm98.Job
{

  /// <summary>
  /// Queue with Dequeue Blocking until items get available or timeout
  /// (or canceled)
  /// </summary>
  public class BlockingQueue<T> : IDisposable
  {

    private readonly Queue<T> _queue;
    private readonly SemaphoreSlim _guard;

    /// <summary>
    /// cTor:
    /// </summary>
    public BlockingQueue( )
    {
      _queue = new Queue<T>( );
      _guard = new SemaphoreSlim( 0 );
    }

    /// <summary>
    /// Tries to remove and return an object 
    /// Waits until timeout if no item is available
    /// </summary>
    /// <param name="item">Item or default</param>
    /// <param name="timeout_ms">WaitTimeout ms</param>
    /// <returns>True when successfull</returns>
    public bool TryDequeue( out T item, int timeout_ms )
    {
      item = default;
      bool waitResult = _guard.Wait( timeout_ms );
      if (!waitResult) return false; // timed out

      lock (_queue) {
        item = _queue.Dequeue( );
      }
      return true;
    }
    /// <summary>
    /// Tries to remove and return an object 
    /// Waits until timeout if no item is available
    /// </summary>
    /// <param name="item">Item or default</param>
    /// <param name="timeout_ms">WaitTimeout ms</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>True when successfull</returns>
    public bool TryDequeue( out T item, int timeout_ms, CancellationToken cancellationToken )
    {
      item = default;
      try {
        bool waitResult = _guard.Wait( timeout_ms, cancellationToken );
        if (!waitResult) return false; // timed out

        lock (_queue) {
          item = _queue.Dequeue( );
        }
        return true;
      }
      catch {
      }
      return false;
    }

    /// <summary>
    /// Add one item to the queue
    /// </summary>
    /// <param name="item"></param>
    public void Enqueue( T item )
    {
      lock (_queue) {
        _queue.Enqueue( item );
      }
      _guard.Release( ); // inc sema count
    }

    #region DISPOSE 

    private bool disposedValue;

    /// <inheritdoc/>
    protected virtual void Dispose( bool disposing )
    {
      if (!disposedValue) {
        if (disposing) {
          // TODO: dispose managed state (managed objects)
          _queue?.Clear( );
          _guard?.Dispose( );
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~BlockingQueue()
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
