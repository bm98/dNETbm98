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

  }
}
