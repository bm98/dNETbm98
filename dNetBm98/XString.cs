﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dNetBm98
{
  /// <summary>
  /// String Utils
  /// </summary>
  public static class XString
  {
    #region String TO Stream 

    /// <summary>
    /// Returns a Stream from a String
    /// </summary>
    /// <param name="s">The String</param>
    /// <param name="encoding">Encoding of the stream</param>
    /// <returns>An open stream</returns>
    public static Stream AsStream( this string s, Encoding encoding )
    {
      var stream = new MemoryStream( );
      var writer = new StreamWriter( stream, encoding );
      writer.Write( s );
      writer.Flush( );
      stream.Position = 0;
      return stream;
    }

    /// <summary>
    /// Returns a Stream from a String
    /// </summary>
    /// <param name="s">The String</param>
    /// <param name="encoding">Encoding of the stream</param>
    /// <returns>An open stream</returns>
    public static Stream StreamFromString( string s, Encoding encoding ) => s.AsStream( encoding );

    #endregion

    #region String FROM Stream 

    /// <summary>
    /// Returns a String from a Stream
    /// </summary>
    /// <param name="s">The Stream</param>
    /// <param name="encoding">Encoding of the stream</param>
    /// <returns>An string</returns>
    public static string AsString( this Stream s, Encoding encoding )
    {
      StringBuilder sb = new StringBuilder( );
      using (var sw = new StreamReader( s, encoding, true )) {
        sb.Append( sw.ReadToEnd( ) );
      }
      return sb.ToString( );
    }

    /// <summary>
    /// Returns a String from a Stream
    /// </summary>
    /// <param name="s">The Stream</param>
    /// <param name="encoding">Encoding of the stream</param>
    /// <returns>An string</returns>
    public static string StringFromStream( Stream s, Encoding encoding ) => s.AsString( encoding );

    #endregion

    #region String FROM File 

    /// <summary>
    /// Returns a String from a File
    ///   retries for max 100ms if the file is unavailable
    /// </summary>
    /// <param name="fileName">The Filename</param>
    /// <param name="encoding">Encoding of the stream</param>
    /// <returns>An string can be empty on error</returns>
    public static string StringFromFile( string fileName, Encoding encoding )
    {
      string retVal = "";
      if (!File.Exists( fileName )) { return retVal; }

      int retries = 10; // 100ms worst case
      while (retries-- > 0) {
        try {
          using (var ts = File.Open( fileName, FileMode.Open, FileAccess.Read, FileShare.Read )) {
            retVal = ts.AsString( encoding );
          }
          return retVal;
        }
        catch (IOException ioex) {
          _ = ioex; // for the compiler..
          // retry after a short wait
          Thread.Sleep( 10 ); // allow the others fileIO to be completed
        }
        catch (Exception ex) {
          _ = ex; // for the compiler..
          // not an IO exception - just fail
          return retVal;
        }
      }

      return retVal;
    }

    #endregion

    /// <summary>
    /// True when the argument is NOT empty or whitespace
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True when NOT empty</returns>
    public static bool NotEmpty( string s ) => !string.IsNullOrWhiteSpace( s );

    /// <summary>
    /// Returns the left portion of a string up to n characters
    /// </summary>
    /// <param name="str">A string</param>
    /// <param name="n">Max number of chars to return</param>
    /// <returns>A string</returns>
    /// <exception cref="ArgumentOutOfRangeException">for n less than 1</exception>
    public static string LeftString( this string str, int n )
    {
      // sanity
      if (n < 1) throw new ArgumentOutOfRangeException( "n cannot be <1" );
      if (string.IsNullOrEmpty( str )) return str;

      if (str.Length > n) return str.Substring( 0, n );
      return str;
    }

    /// <summary>
    /// Returns the left portion of a string up to n characters
    /// </summary>
    /// <param name="str">A string</param>
    /// <param name="n">Max number of chars to return</param>
    /// <returns>A string</returns>
    /// <exception cref="ArgumentOutOfRangeException">for n less than 1</exception>
    public static string LeftStringOf( string str, int n ) => str.LeftString( n );


  }
}
