using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Win
{
  /// <summary>
  /// Type of Memory reported
  /// </summary>
  public enum WinMemoryType
  {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    Virtual,
    Private,
    Paged,
    PeakPaged,
    PagedSystem,
    NonpagedSystem,

    GC_Total,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
  }

  /// <summary>
  /// A catalog of Memory items
  /// </summary>
  public class MemoryCat : Dictionary<WinMemoryType, long>
  {
    /// <summary>
    /// Formatted string of used memory in MB
    /// </summary>
    public string PrettyString_MB( )
    {
      var sb = new StringBuilder( );
      foreach (var item in this) {
        sb.AppendLine( $"{item.Key,-15}: {item.Value / 1_000_000.0:#0.000} MB" );
      }
      return sb.ToString( );
    }

    /// <summary>
    /// Formatted string of used memory in MB
    /// </summary>
    public string PrettyString_kB( )
    {
      var sb = new StringBuilder( );
      foreach (var item in this) {
        sb.AppendLine( $"{item.Key,-15}: {item.Value / 1_000.0:#0.000} kB" );
      }
      return sb.ToString( );
    }
  }

  /// <summary>
  /// A Win / .Net Memory Monitor
  /// </summary>
  public class WinMemMonitor
  {
    /// <summary>
    /// Internal list of collected snapshots
    /// </summary>
    protected List<MemoryCat> _snapShots = new List<MemoryCat>( );

    /// <summary>
    /// Clear all snapshots
    /// </summary>
    public void ClearSnapshots( ) => _snapShots.Clear( );

    /// <summary>
    /// Take a memory snapshot an return the current readings
    /// </summary>
    /// <returns>A MemoryCat</returns>
    public MemoryCat TakeSnapshot( )
    {
      var s = GetMemoryAllocated( );
      _snapShots.Add( s );
      return s;
    }

    /// <summary>
    /// Get the n'th snapshot
    /// </summary>
    /// <param name="n">0...N</param>
    /// <returns>A MemoryCat (can be empty)</returns>
    public MemoryCat GetSnapshot( uint n )
    {
      if (n < _snapShots.Count) {
        return _snapShots[(int)n];
      }
      else {
        return new MemoryCat( );
      }
    }

    /// <summary>
    /// Return the current memory allocation
    /// </summary>
    /// <returns>A MemoryCat</returns>
    public MemoryCat GetMemoryAllocated( )
    {
      var p = Process.GetCurrentProcess( );
      MemoryCat memUse = new MemoryCat{
        { WinMemoryType.Virtual, p.VirtualMemorySize64 },
        { WinMemoryType.Private, p.PrivateMemorySize64 },
        { WinMemoryType.Paged, p.PagedMemorySize64 },
        { WinMemoryType.PeakPaged, p.PeakPagedMemorySize64 },
        { WinMemoryType.PagedSystem, p.PagedSystemMemorySize64 },
        { WinMemoryType.NonpagedSystem, p.NonpagedSystemMemorySize64 },
        { WinMemoryType.GC_Total, GC.GetTotalMemory( true ) }
      };

      return memUse;
    }

    /// <summary>
    /// Return the diff of Current - Reference 
    ///  i.e. growth or decline of vs. Reference
    /// </summary>
    /// <param name="reference">Cat to compare with</param>
    /// <returns>A MemoryCat</returns>
    public MemoryCat GetMemoryChange( MemoryCat reference )
    {
      return GetMemoryChange( reference, GetMemoryAllocated( ) );
    }


    /// <summary>
    /// Return the diff of New - Reference 
    ///  i.e. growth or decline of vs. Reference
    /// </summary>
    /// <param name="reference">Cat to compare with</param>
    /// <param name="newCat">Cat to compare</param>
    /// <returns>A MemoryCat</returns>
    public MemoryCat GetMemoryChange( MemoryCat reference, MemoryCat newCat )
    {
      MemoryCat delta = new MemoryCat( );
      foreach (var item in newCat) {
        if (reference.ContainsKey( item.Key )) {
          delta.Add( item.Key, item.Value - reference[item.Key] );
        }
        else {
          delta.Add( item.Key, item.Value );
        }
      }
      return delta;
    }


  }
}
