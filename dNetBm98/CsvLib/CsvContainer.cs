using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.CsvLib
{
  /// <summary>
  /// Provides the loaded Csv File for further processing
  /// </summary>
  public class CsvContainer : List<CsvLine>
  {

    private int _numColums = 0;

    /// <summary>
    /// Number of columns
    /// </summary>
    public int NumColumns { get => _numColums; }

    /// <summary>
    /// Add a line to the container
    /// </summary>
    /// <param name="csvLine">A CSV Line</param>
    public new void Add( CsvLine csvLine )
    {
      _numColums = (csvLine.Count > _numColums) ? csvLine.Count : _numColums;

      base.Add( csvLine );
    }

    /// <summary>
    /// Clear the Container
    /// </summary>
    public new void Clear( )
    {
      _numColums = 0;
      base.Clear( );
    }

    /// <summary>
    /// Return the Container as DataSet
    /// </summary>
    /// <returns></returns>
    public DataSet AsDataSet( )
    {
      var d = new DataSet( "CsvContainer" );
      var t = d.Tables.Add( "Table" );
      for (int i = 0; i < _numColums; i++) {
        t.Columns.Add( );
      }
      foreach (CsvLine line in this) {
        object[] l = line.ToArray( );
        t.Rows.Add( l );
      }

      return d;
    }


    /// <summary>
    /// Returns an 0 based 2D array [row,col] containing string data
    /// </summary>
    /// <returns>An 0 based 2D array</returns>
    public object[,] AsArray( )
    {
      var values = Array.CreateInstance( typeof( string ), new int[] { this.Count, this.NumColumns } );
      int r = 0;
      foreach (CsvLine line in this) {
        int c = 0;
        foreach (string s in line) {
          values.SetValue( s, r, c++ );
        }
        r++;
      }

      return (object[,])values;
    }

    /// <summary>
    /// Returns an 1 based 2D array [row,col] containing string data
    /// </summary>
    /// <returns>An 1 based 2D array</returns>
    public object[,] AsArray1( )
    {
      var values = Array.CreateInstance( typeof( string ), new int[] { this.Count, this.NumColumns }, new int[] { 1, 1 } );
      int r = 1;
      foreach (CsvLine line in this) {
        int c = 1;
        foreach (string s in line) {
          values.SetValue( s, r, c++ );
        }
        r++;
      }

      return (object[,])values;
    }

  }

}
