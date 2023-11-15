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

  }
}
