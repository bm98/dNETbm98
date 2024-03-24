using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dNetBm98
{
  /// <summary>
  /// Utility Bag of static methods ..
  /// A number of widely used items 
  /// </summary>
  public static class Utilities
  {

    /// <summary>
    /// A Knuth Hash function for Strings
    /// </summary>
    /// <param name="aString"></param>
    /// <returns>A Hash number for the string</returns>
    public static UInt64 KnuthHash( string aString )
    {
      UInt64 hashedValue = 3074457345618258791ul;
      for (int i = 0; i < aString.Length; i++) {
        hashedValue += aString[i];
        hashedValue *= 3074457345618258799ul;
      }
      return hashedValue;
    }

    /// <summary>
    /// Checks if a Point is visible on any screen
    /// </summary>
    /// <param name="point">The Location to check</param>
    /// <returns>True if visible</returns>
    public static bool IsOnScreen( Point point )
    {
      Screen[] screens = Screen.AllScreens;
      foreach (Screen screen in screens) {
        if (screen.WorkingArea.Contains( point )) {
          return true;
        }
      }
      return false;
    }

    // readout letter from value 0..6 (+1 extra for number conversion issues)
    private static readonly string[] _rsiScale = new string[] { "F", "E", "D", "C", "B", "A", "A", };
    /// <summary>
    /// Returns an RSI (radio signale intensity) scale Letter A..F
    /// where A is strongest to F is least strength
    /// </summary>
    /// <param name="dist">The distance from the Sender</param>
    /// <param name="range">The reange of the Sender</param>
    /// <returns>An RSI letter A..F</returns>
    public static string RSI( double dist, double range )
    {
      // rsi drops by dist^2; d=0 -> rsi=1; d=r -> rsi=0; d>r -> rsi is negative 
      var rsivalue = (range - dist) / range; rsivalue *= rsivalue; // ^2 .. cheap
      var rsiS = _rsiScale[(int)XMath.Clip( XMath.RoundInt( rsivalue * 6.0, 1 ), 0, 6 )]; // round to 0..6 and clip to index the Scale
      return rsiS;
    }

    /// <summary>
    /// Get the quoted part of this string or all when the input does not start with a quote
    ///  the Quote character is " by default but can be set as argument
    /// </summary>
    /// <param name="inp">Input string where a quoted part must be extracted</param>
    /// <param name="quoteChar">The quote character, defaults to " (double quote)</param>
    /// <returns>The unqoted string</returns>
    public static string FromQuoted( string inp, char quoteChar = '"' )
    {
      if (inp.TrimStart( ).StartsWith( quoteChar.ToString( ) )) {
        var sx = inp.ToArray( )
          .SkipWhile( c => c != quoteChar )
          .Skip( 1 )
          .TakeWhile( c => c != quoteChar );
        var v = new string( sx.ToArray( ) );
        return v;
      }
      // no starting quote found
      return inp;
    }


    #region Endianess

    /*
    * Thank You for the Swaps !
    *    https://stackoverflow.com/questions/19560436/bitwise-endian-swap-for-various-types
    */

    private static UInt16 SwapBytes( UInt16 x )
    {
      // swap adjacent 8-bit blocks
      return (ushort)(((UInt32)x >> 8) | ((UInt32)x << 8));
    }

    private static UInt32 SwapBytes( UInt32 x )
    {
      // swap adjacent 16-bit blocks
      x = (x >> 16) | (x << 16);
      // swap adjacent 8-bit blocks
      return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
    }

    private static UInt64 SwapBytes( UInt64 x )
    {
      // swap adjacent 32-bit blocks
      x = (x >> 32) | (x << 32);
      // swap adjacent 16-bit blocks
      x = ((x & 0xFFFF0000FFFF0000) >> 16) | ((x & 0x0000FFFF0000FFFF) << 16);
      // swap adjacent 8-bit blocks
      return ((x & 0xFF00FF00FF00FF00) >> 8) | ((x & 0x00FF00FF00FF00FF) << 8);
    }

    /// <summary>
    /// Returns an unsigned long with Swapped bytes
    /// </summary>
    public static ulong ReverseEndianess( ulong be ) => SwapBytes( be );

    /// <summary>
    /// Returns an unsigned int with Swapped bytes
    /// </summary>
    public static uint ReverseEndianess( uint be ) => SwapBytes( be );

    /// <summary>
    /// Returns an unsigned short with Swapped bytes
    /// </summary>
    public static ushort ReverseEndianess( ushort be ) => SwapBytes( be );

    /// <summary>
    /// Returns a signed long with Swapped bytes
    /// </summary>
    public static long ReverseEndianess( long be ) => (long)SwapBytes( (ulong)be );

    /// <summary>
    /// Returns a signed int with Swapped bytes
    /// </summary>
    public static int ReverseEndianess( int be ) => (int)SwapBytes( (uint)be );

    /// <summary>
    /// Returns a signed short with Swapped bytes
    /// </summary>
    public static short ReverseEndianess( short be ) => (short)SwapBytes( (ushort)be );

    #endregion

  }
}
