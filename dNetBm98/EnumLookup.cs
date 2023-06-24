using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dNetBm98
{
  /// <summary>
  /// Array based Lookup Table for Enum based access
  /// The Enum Type cannot be dynamically extended 
  /// 
  ///  replaces a Dictionary of Enum,Type where the enum Hash is just a distraction..
  ///  NOTE: The class pre-allocates elements for the whole Enum range  (max-min+1)
  /// </summary>
  /// <typeparam name="E">An Enum to as indexer</typeparam>
  /// <typeparam name="T">A type of stored items</typeparam>
  public class EnumLookup<E, T> : IEnumerable<T> where E : Enum
  {
    // max number of elements supported (else it is probably a programm error)
    private const int c_maxLen = 10_000;
    private T[] _table = null;
    private int _minValue = 0;
    private int _maxValue = 0;
    private int _len = 0;
    private int _count = 0;

    /// <summary>
    /// cTor:
    /// </summary>
    public EnumLookup( )
    {
      // GetValues:
      //  The elements of the array are sorted by the binary values of the enumeration constants (that is, by their unsigned magnitude)
      var values = Enum.GetValues( typeof( E ) );
      _minValue = (int)values.GetValue( 0 );
      _maxValue = (int)values.GetValue( values.Length - 1 );
      _len = _maxValue - _minValue + 1;
      _count = 0;
      // sanity check
      if (_len > c_maxLen) {
        throw new ArgumentOutOfRangeException( $"Item allocation limit exceeded, asks for {_len} items (max {c_maxLen})" );
      }

      _table = new T[_len];
      Clear( );
    }

    /// <summary>
    /// Get;Set: Element indexed by the Enum
    /// </summary>
    /// <param name="item">An Enum E</param>
    /// <returns>An element T</returns>
    public T this[E item] {
      get {
        int index = (int)Convert.ChangeType( item, typeof( int ) );
        return _table[index - _minValue];
      }
      set {
        // was it default and now it's not ?
        bool wasDefault = IsDefault( item );
        int index = (int)Convert.ChangeType( item, typeof( int ) );
        _table[index - _minValue] = value;
        if (wasDefault) {
          _count += IsDefault( item ) ? 0 : 1; // did we add one
        }
        else {
          _count -= IsDefault( item ) ? 1 : 0; // did we default (remove) one
        }
      }
    }

    /// <summary>
    /// True if the item is default(T)
    /// </summary>
    /// <param name="item">An Enum E</param>
    /// <returns>True if the item is default(T)</returns>
    public bool IsDefault( E item ) => EqualityComparer<T>.Default.Equals( this[item], default );

    /// <summary>
    /// Mock for the Dictionary Function ContainsKey
    /// 
    /// </summary>
    /// <param name="item">An Enum E</param>
    /// <returns>True if the item is NOT default(T)</returns>
    public bool ContainsKey( E item ) => !IsDefault( item );

    /// <summary>
    /// Mock for the Dictionary Add
    /// </summary>
    /// <param name="item">An Enum E</param>
    /// <param name="element">An item of type T</param>
    public void Add( E item, T element ) => this[item] = element;

    /// <summary>
    /// Clears the Table by assiging default(T) to each element
    /// </summary>
    public void Clear( )
    {
      for (int i = 0; i < _len; i++) {
        _table[i] = default;
      }
      _count = 0;
    }

    /// <summary>
    /// Returns the number of non Default items in the Lookup
    /// </summary>
    public int Count => _count;

    #region Enumerable Implementation

    // curtesy MS Documentation

    /// <summary>
    /// Returns the Enumerator for this object
    /// </summary>
    /// <returns>An enumerator</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator( ) => new MyEnumerator( _table );
    IEnumerator IEnumerable.GetEnumerator( ) => new MyEnumerator( _table );

    /// <summary>
    /// Returns the Enumerator for this object
    /// </summary>
    /// <returns>An enumerator</returns>
    //   public IEnumerator<T> GetEnumerator( )



    //private enumerator class
    private class MyEnumerator : IEnumerator<T>, IEnumerator
    {
      public T[] table;
      int position = -1;

      //constructor
      public MyEnumerator( T[] list )
      {
        table = list;
      }
      private IEnumerator getEnumerator( ) => (IEnumerator)this;

      //IEnumerator
      public bool MoveNext( )
      {
        position++;
        // scan for non empty elements
        while ((position < table.Length)
          && EqualityComparer<T>.Default.Equals( table[position], default )) {
          position++;
        }

        return (position < table.Length);
      }
      //IEnumerator
      public void Reset( ) => position = -1;

      //IEnumerator
      public T Current {
        get {
          try {
            return table[position];
          }
          catch (IndexOutOfRangeException) {
            throw new InvalidOperationException( );
          }
        }
      }

      object IEnumerator.Current {
        get {
          try {
            return table[position];
          }
          catch (IndexOutOfRangeException) {
            throw new InvalidOperationException( );
          }
        }
      }

      // Dispose this Enumerator
      public void Dispose( ) { }
    }  //end nested class

    #endregion

  }
}
