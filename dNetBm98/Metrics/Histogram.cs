using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Metrics
{
  /// <summary>
  /// Implements an object to capture histogram data
  ///  2 versions, one with a simply Dictionary (default), another with a LinkedList
  ///  where in general the default version is faster but at least not slower than the LList version
  /// </summary>
  public class Histogram<T>
  {

    // internal bucket containing the histogram count for this value
    [DefaultMember( "Value" )]
    private class Bucket
    {
      public int Count { get; set; } = 1; // count on creation
      public T Item { get; set; } = default;

      public Bucket( T item ) => this.Item = item;

      public void IncValue( ) => Count++;
    }

    // Value counter
    private int _count = 0;

    // our buckets
    private Dictionary<T, Bucket> _buckets;

    // via linked list
    // lookup for nodes to not traverse the list each time
    private Dictionary<T, LinkedListNode<Bucket>> _llNodeLookup;
    // linked list where the Max count item is always the First, the least count the Last
    private LinkedList<Bucket> _bucketLList;

    private bool _usingLL = false;

    /// <summary>
    /// Number of values added
    /// </summary>
    public int Count => _count;

    /// <summary>
    /// cTor:
    /// </summary>
    public Histogram( bool usingLL = false )
    {
      _buckets = new Dictionary<T, Bucket>( );

      _usingLL = usingLL;
      _bucketLList = new LinkedList<Bucket>( );
      _llNodeLookup = new Dictionary<T, LinkedListNode<Bucket>>( );
    }

    /// <summary>
    /// Reset the Histogram as Empty
    /// </summary>
    public void Reset( )
    {
      _count = 0;
      if (_usingLL) {
        _bucketLList.Clear( );
        _llNodeLookup.Clear( );
      }
      else {
        _buckets.Clear( );
      }
    }



    /// <summary>
    /// Add one item
    /// </summary>
    /// <param name="item">An item</param>
    public void Add( T item )
    {
      _count++;
      if (_usingLL) {
        if (_llNodeLookup.TryGetValue( item, out var node )) {
          // item is part of the histogram
          node.Value.IncValue( );

          var prevNode = node.Previous;
          if (prevNode == null) return; // shortcut: node is already the top item

          do { // maintain max..min list order
            if (node.Value.Count > prevNode.Value.Count) {
              // not top and value > prev ==> shift one up
              _bucketLList.Remove( node );
              var newNode = _bucketLList.AddBefore( prevNode, node.Value );
              _llNodeLookup[item] = newNode;
              // prep next round
              node = newNode;
              prevNode = node.Previous;
            }
            else {
              break; // finished
            }
          } while (prevNode != null);

          return;
        }
        // item not found - add a new bucket with count =1 at the end
        var newB = new Bucket( item );
        var listItem = new LinkedListNode<Bucket>( newB );
        _llNodeLookup.Add( item, listItem );
        _bucketLList.AddLast( listItem ); // new must be last

      }
      else {
        // using Dictionary
        if (_buckets.TryGetValue( item, out var bucket )) {
          bucket.IncValue( );
          return;
        }
        // item not found  add a new bucket with count =1
        _buckets.Add( item, new Bucket( item ) );
      }
    }



    /// <summary>
    /// Return the first item with Min count
    /// </summary>
    /// <returns>An item T</returns>
    public T Min( )
    {
      if (_usingLL) {
        return _bucketLList.LastOrDefault( ).Item;
      }
      else {
        // shortcuts
        if (_buckets.Count == 0) return default;
        if (_buckets.Count == 1) return _buckets.First( ).Key;

        // find and return the first with max count
        return _buckets.Where( bx => bx.Value.Count == _buckets.Min( x => x.Value.Count ) ).FirstOrDefault( ).Key;
      }
    }

    /// <summary>
    /// Return the first item with Max count
    /// </summary>
    /// <returns>An item T</returns>
    public T Max( )
    {
      if (_usingLL) {
        return _bucketLList.FirstOrDefault( ).Item;
      }
      else {
        // shortcuts
        if (_buckets.Count == 0) return default;
        if (_buckets.Count == 1) return _buckets.First( ).Key;

        // find and return the first with max count
        return _buckets.Where( bx => bx.Value.Count == _buckets.Max( x => x.Value.Count ) ).FirstOrDefault( ).Key;
      }
    }


  }
}
