using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98
{
  /// <summary>
  /// String Utils
  /// </summary>
  public static class XString
  {

    /// <summary>
    /// Returns a Stream from a String
    /// </summary>
    /// <param name="s">The String</param>
    /// <param name="encoding">Encoding of the stream</param>
    /// <returns>An open stream</returns>
    public static Stream StreamFromString( string s, Encoding encoding )
    {
      var stream = new MemoryStream( );
      var writer = new StreamWriter( stream,encoding );
      writer.Write( s );
      writer.Flush( );
      stream.Position = 0;
      return stream;
    }

    /// <summary>
    /// Returns a String from a Stream
    /// </summary>
    /// <param name="s">The Stream</param>
    /// <param name="encoding">Encoding of the stream</param>
    /// <returns>An string</returns>
    public static string StringFromStream( Stream s, Encoding encoding )
    {
      StringBuilder sb = new StringBuilder( );
      using (var sw = new StreamReader( s, encoding, true )) {
        sb.Append( sw.ReadToEnd( ) );
      }
      return sb.ToString( );
    }

  }
}
