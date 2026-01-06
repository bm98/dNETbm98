using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// <summary>
    /// Checks if a Rectangle intersects with any screen
    /// </summary>
    /// <param name="rect">The Rectangle to check</param>
    /// <returns>True if visible</returns>
    public static bool IsOnScreen( Rectangle rect )
    {
      Screen[] screens = Screen.AllScreens;
      foreach (Screen screen in screens) {
        if (screen.WorkingArea.IntersectsWith( rect )) {
          return true;
        }
      }
      return false;
    }
    /// <summary>
    /// Checks if a rectangle of boxSize with center at location intersects with any screen
    /// </summary>
    /// <param name="location">Center location of the rectangle to check</param>
    /// <param name="boxSize">Box size with location as center point</param>
    /// <returns>True if visible</returns>
    public static bool IsOnScreen( Point location, Size boxSize )
    {
      Rectangle testR = new Rectangle( new Point( location.X - boxSize.Width / 2, location.Y - boxSize.Height / 2 ), boxSize );
      return IsOnScreen( testR );
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

    /// <summary>
    /// Open a folder in Explorer
    /// </summary>
    /// <param name="folder"></param>
    public static void OpenInExplorer( string folder )
    {
      string path = Path.GetFullPath( folder );
      if (Directory.Exists( path )) {
        // never fail
        try {
          //Process.Start( "explorer.exe", @"c:\temp" );

          Process.Start( "explorer.exe", path + Path.DirectorySeparatorChar ); // make it C:\xy\ to avoid issues with starting apps
        }
        catch { }
      }
    }

    #region Filenames

    // invalid device names (DOS times...)
    /*
      CON, PRN, AUX, NUL, 
      COM0, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM¹, COM², COM³, 
      LPT0, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9, LPT¹, LPT², and LPT³. 
      Also avoid these names followed immediately by an extension; 
      for example, NUL.txt and NUL.tar.gz are both equivalent to NUL. For more information     
     */
    private static Regex _rxDeviceNames = new Regex( @"(^(CON|PRN|AUX|NUL|COM(\d|¹|²|³)?|LPT(\d|¹|²|³)?)$)",
                RegexOptions.Compiled | RegexOptions.IgnoreCase );
    private static string _rxFnameReplace = null;
    private static string _rxDnameReplace = null;

    /// <summary>
    /// Validate a Windows Filename
    ///  Note: the directory part is not checked
    /// </summary>
    /// <param name="fileName">A raw filename</param>
    /// <returns>True when valid</returns>
    public static bool IsValidFileName( string fileName )
    {
      string validFileName = MakeValidFileName( fileName );
      return fileName == validFileName;
    }

    // returns the filename before ANY extension (before first dot)
    private static string GetFileNameWithoutAnyExtension( string fileName )
    {
      int dotPos = fileName.IndexOf( "." );
      if (dotPos == -1) {
        return fileName; // no dot
      }
      else {
        return fileName.Substring( 0, dotPos );
      }
    }

    // returns any extension (including first dot)
    private static string GetAnyExtension( string fileName )
    {
      int dotPos = fileName.IndexOf( "." );
      if (dotPos == -1) {
        return ""; // no dot
      }
      else {
        return fileName.Substring( dotPos );
      }
    }

    /// <summary>
    /// Clean and return Windows Filename
    ///  Note: the directory part is not checked and returned as given
    /// </summary>
    /// <param name="fileName">A raw filename</param>
    /// <returns>A Windows Compliant Filename</returns>
    public static string MakeValidFileName( string fileName )
    {
      // Thank you: https://stackoverflow.com/questions/309485/c-sharp-sanitize-file-name

      if (_rxFnameReplace == null) {
        // create when first used
        char[] cc = Path.GetInvalidFileNameChars( );
        //cc = cc.Union( Path.GetInvalidPathChars( ) ).ToArray( );
        string invalidChars = Regex.Escape( new string( cc ) );
        _rxFnameReplace = string.Format( @"([{0}]+)", invalidChars );
      }
      // test and replace file name+ext for invalid chars
      string fNameExt = Path.GetFileName( fileName );
      fNameExt = Regex.Replace( fNameExt, _rxFnameReplace, "_", RegexOptions.Singleline | RegexOptions.CultureInvariant );

      // test and replace the file name for Device names
      string fName = GetFileNameWithoutAnyExtension( fNameExt );
      if (_rxDeviceNames.Match( fName ).Success) {
        fName += "$"; // patch by appending a $ to the file (CON[.something.all] -> CON$[.something.all])
      }

      // patch all together
      string retName = Path.Combine( Path.GetDirectoryName( fileName ), fName + GetAnyExtension( fNameExt ) );
      // check for trailing dot
      /*
          Do not end a file or directory name with a space or a period. 
          Although the underlying file system may support such names, the Windows shell and user interface does not. 
          However, it is acceptable to specify a period as the first character of a name. For example, ".temp".       
       */
      if (retName.EndsWith( "." )) {
        retName += "_"; // patch by appending a $ (bla. -> bla._)
      }
      return retName.Trim( ); // remove spaces around if there are
    }


    /// <summary>
    /// Validate a Windows Directiryname
    ///  DON'T provide strings with multiple path entries...
    /// </summary>
    /// <param name="dirName">A raw directory</param>
    /// <returns>True when valid</returns>
    public static bool IsValidDirectoryName( string dirName )
    {
      string validDirName = MakeValidDirectoryName( dirName );
      return dirName == validDirName;
    }

    /// <summary>
    /// Clean and return Windows Directoryname
    ///  DON'T provide strings with multiple path entries...
    ///  unexpected results may happen
    ///  
    /// </summary>
    /// <param name="dirName">A raw directory</param>
    /// <returns>A Windows Compliant Directoryname</returns>
    public static string MakeValidDirectoryName( string dirName )
    {
      // Thank you: https://stackoverflow.com/questions/309485/c-sharp-sanitize-file-name

      if (_rxDnameReplace == null) {
        // create when first used
        char[] cc = Path.GetInvalidPathChars( );
        string invalidChars = Regex.Escape( new string( cc ) );
        _rxDnameReplace = string.Format( @"([{0}]+)", invalidChars );
      }
      // test and replace file name+ext for invalid chars
      string dNameExt = Regex.Replace( dirName, _rxDnameReplace, "_", RegexOptions.Singleline | RegexOptions.CultureInvariant );

      // test and replace the dir name for Device names
      string dName = dNameExt;
      if (_rxDeviceNames.Match( dName ).Success) {
        dName += "$"; // patch by appending a $ to the file (CON[.something.all] -> CON$[.something.all])
      }

      // patch all together
      string retName = dName;
      // check for trailing dot
      /*
          Do not end a file or directory name with a space or a period. 
          Although the underlying file system may support such names, the Windows shell and user interface does not. 
          However, it is acceptable to specify a period as the first character of a name. For example, ".temp".       
       */
      if (retName.EndsWith( "." )) {
        retName += "_"; // patch by appending a $ (bla. -> bla._)
      }
      return retName.Trim( ); // remove spaces around if there are
    }

    #endregion

    #region Culture

    /// <summary>
    /// Modify and return a culture with (uniform) decimal points and semi as list seperator
    /// </summary>
    /// <param name="culture">Reference culture to modify</param>
    /// <returns>Modified culture</returns>
    public static CultureInfo SetUniformat( CultureInfo culture )
    {
      if (culture == null) return CultureInfo.InvariantCulture;
      // change props prone to IO mismatches
      culture.NumberFormat.NumberDecimalDigits = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalDigits;
      culture.NumberFormat.NumberDecimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
      culture.NumberFormat.NumberGroupSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator;
      culture.NumberFormat.NumberGroupSizes = CultureInfo.InvariantCulture.NumberFormat.NumberGroupSizes;
      culture.NumberFormat.NumberNegativePattern = CultureInfo.InvariantCulture.NumberFormat.NumberNegativePattern;

      culture.NumberFormat.PercentSymbol = CultureInfo.InvariantCulture.NumberFormat.PercentSymbol;
      culture.NumberFormat.PerMilleSymbol = CultureInfo.InvariantCulture.NumberFormat.PerMilleSymbol;
      culture.NumberFormat.PercentDecimalDigits = CultureInfo.InvariantCulture.NumberFormat.PercentDecimalDigits;
      culture.NumberFormat.PercentDecimalSeparator = CultureInfo.InvariantCulture.NumberFormat.PercentDecimalSeparator;
      culture.NumberFormat.PercentGroupSeparator = CultureInfo.InvariantCulture.NumberFormat.PercentGroupSeparator;
      culture.NumberFormat.PercentGroupSizes = CultureInfo.InvariantCulture.NumberFormat.PercentGroupSizes;
      culture.NumberFormat.PercentPositivePattern = CultureInfo.InvariantCulture.NumberFormat.PercentPositivePattern;
      culture.NumberFormat.PercentNegativePattern = CultureInfo.InvariantCulture.NumberFormat.PercentNegativePattern;

      // change list separator to semi
      culture.TextInfo.ListSeparator = ";"; // CultureInfo.InvariantCulture.TextInfo.ListSeparator; invariant is ",";
      return culture;
    }

    /// <summary>
    /// Set the Default Culture used for this application and also for threads created
    /// </summary>
    /// <param name="culture">CultureInfo to be used</param>
    public static void SetApplicationDefaultCulture( CultureInfo culture )
    {
      CultureInfo.DefaultThreadCurrentCulture = culture;
      CultureInfo.DefaultThreadCurrentUICulture = culture;

      Thread.CurrentThread.CurrentCulture = culture;
      Thread.CurrentThread.CurrentUICulture = culture;
    }

    /// <summary>
    /// Set the App Culture used modified to Uniform number and list format (invariant numbers and semi as list)
    /// Starting from the given culture
    /// </summary>
    /// <param name="culture">CultureInfo to be used, if null, invariantCulture is used</param>
    public static void SetApplicationUniformat( CultureInfo culture )
    {
      if (culture == null) {
        culture = CultureInfo.InvariantCulture;
      }
      culture = SetUniformat( culture );

      CultureInfo.DefaultThreadCurrentCulture = culture;
      CultureInfo.DefaultThreadCurrentUICulture = culture;

      Thread.CurrentThread.CurrentCulture = culture;
      Thread.CurrentThread.CurrentUICulture = culture;
    }

    /// <summary>
    /// Set the Thread Culture used modified to Uniform number and list format (invariant numbers and semi as list)
    /// Starting from the given culture
    /// </summary>
    /// <param name="culture">CultureInfo to be used, if null, invariantCulture is used</param>
    public static void SetThreadUniformat( CultureInfo culture )
    {
      if (culture == null) {
        culture = CultureInfo.InvariantCulture;
      }
      culture = SetUniformat( culture );

      Thread.CurrentThread.CurrentCulture = culture;
      Thread.CurrentThread.CurrentUICulture = culture;
    }

    #endregion

    #region Serialization Support

    /// <summary>
    /// As Serialized string ({L=1,T=2,R=3,B=4})
    ///  culture invariant
    /// </summary>
    /// <param name="p">A Padding</param>
    /// <returns>A string</returns>
    public static string AsSerString( this Padding p )
    {
      return string.Format( CultureInfo.InvariantCulture, "{{L={0},T={1},R={2},B={3}}}", p.Left, p.Top, p.Right, p.Bottom );
    }

    private static Regex rxSz = new Regex( @"^\{\s*L=(?<l>[+-]?\d+)\s*,\s*T=(?<t>[+-]?\d+)\s*,\s*R=(?<r>[+-]?\d+)\s*,\s*B=(?<b>[+-]?\d+)\s*\}$",
          RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase );

    /// <summary>
    /// Convert a Padding from ToSerString() back to a Padding ({L=1,T=2,R=3,B=4})
    ///  culture invariant
    /// </summary>
    /// <param name="ss">A Padding.ToSerString() string</param>
    /// <returns>A Padding</returns>
    public static Padding PaddingFromSerString( string ss )
    {
      // never fail
      try {
        Match match = rxSz.Match( ss.Trim( ) );
        if (match.Success) {
          int l = int.Parse( match.Groups["l"].Value, CultureInfo.InvariantCulture );
          int t = int.Parse( match.Groups["t"].Value, CultureInfo.InvariantCulture );
          int r = int.Parse( match.Groups["r"].Value, CultureInfo.InvariantCulture );
          int b = int.Parse( match.Groups["b"].Value, CultureInfo.InvariantCulture );
          return new Padding( l, t, r, b );
        }
      }
      catch { }

      return new Padding( 0, 0, 0, 0 );
    }

    #endregion

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
