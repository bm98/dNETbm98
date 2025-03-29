using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98
{

  /// <summary>
  /// Culture Invariant number IO
  /// </summary>
  public static class XIO
  {
    // TryParseX

    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static decimal ParseXm( string s ) => decimal.Parse( s, NumberStyles.Float, CultureInfo.InvariantCulture );
    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static double ParseXd( string s ) => double.Parse( s, NumberStyles.Float, CultureInfo.InvariantCulture );
    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static float ParseXf( string s ) => float.Parse( s, NumberStyles.Float, CultureInfo.InvariantCulture );

    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static sbyte ParseXsb( string s ) => sbyte.Parse( s, NumberStyles.Integer, CultureInfo.InvariantCulture );
    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static short ParseXs( string s ) => short.Parse( s, NumberStyles.Integer, CultureInfo.InvariantCulture );
    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static int ParseXi( string s ) => int.Parse( s, NumberStyles.Integer, CultureInfo.InvariantCulture );
    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static long ParseXl( string s ) => long.Parse( s, NumberStyles.Integer, CultureInfo.InvariantCulture );

    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static byte ParseXb( string s ) => byte.Parse( s, NumberStyles.Integer, CultureInfo.InvariantCulture );
    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static ushort ParseXus( string s ) => ushort.Parse( s, NumberStyles.Integer, CultureInfo.InvariantCulture );
    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static uint ParseXui( string s ) => uint.Parse( s, NumberStyles.Integer, CultureInfo.InvariantCulture );
    /// <summary>
    /// Culture Invariant Parse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <returns>True if successful</returns>
    public static ulong ParseXul( string s ) => ulong.Parse( s, NumberStyles.Integer, CultureInfo.InvariantCulture );


    // TryParseX

    /// <summary>
    /// Culture Invariant TryParse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out decimal num )
      => decimal.TryParse( s, NumberStyles.Float, CultureInfo.InvariantCulture, out num );
    /// <summary>
    /// Culture Invariant TryParse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out double num )
      => double.TryParse( s, NumberStyles.Float, CultureInfo.InvariantCulture, out num );
    /// <summary>
    /// Culture Invariant TryParse (assumes decimal point)
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out float num )
      => float.TryParse( s, NumberStyles.Float, CultureInfo.InvariantCulture, out num );

    /// <summary>
    /// Culture Invariant TryParse
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out sbyte num )
      => sbyte.TryParse( s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num );
    /// <summary>
    /// Culture Invariant TryParse
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out short num )
      => short.TryParse( s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num );
    /// <summary>
    /// Culture Invariant TryParse
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out int num )
      => int.TryParse( s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num );
    /// <summary>
    /// Culture Invariant TryParse
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out long num )
      => long.TryParse( s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num );

    /// <summary>
    /// Culture Invariant TryParse
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out byte num )
      => byte.TryParse( s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num );
    /// <summary>
    /// Culture Invariant TryParse
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out ushort num )
      => ushort.TryParse( s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num );
    /// <summary>
    /// Culture Invariant TryParse
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out uint num )
      => uint.TryParse( s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num );
    /// <summary>
    /// Culture Invariant TryParse
    /// </summary>
    /// <param name="s">A string</param>
    /// <param name="num">out a number</param>
    /// <returns>True if successful</returns>
    public static bool TryParseX( string s, out ulong num )
      => ulong.TryParse( s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num );

    // ToStringX

    /// <summary>
    /// A full precision number string with invariant culture (decimal point)
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this decimal num ) => num.ToString( "G", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture (decimal point)
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this double num ) => num.ToString( "G17", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture (decimal point)
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this float num ) => num.ToString( "G9", CultureInfo.InvariantCulture );

    /// <summary>
    /// A full precision number string with invariant culture
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this sbyte num ) => num.ToString( "G", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this short num ) => num.ToString( "G", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this int num ) => num.ToString( "G", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this long num ) => num.ToString( "G", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this byte num ) => num.ToString( "G", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this ushort num ) => num.ToString( "G", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this uint num ) => num.ToString( "G", CultureInfo.InvariantCulture );
    /// <summary>
    /// A full precision number string with invariant culture
    /// </summary>
    /// <param name="num">A number</param>
    /// <returns>A string</returns>
    public static string ToStringX( this ulong num ) => num.ToString( "G", CultureInfo.InvariantCulture );

    // FormatX

    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( decimal num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );
    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( double num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );
    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( float num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );

    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( sbyte num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );
    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( short num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );
    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( int num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );
    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( long num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );

    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( byte num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );
    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( ushort num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );
    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( uint num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );
    /// <summary>
    /// Culture Invariant Formatting
    /// </summary>
    /// <param name="num">A number</param>
    /// <param name="s">A format string containing {0[format]} as placeholder</param>
    /// <returns>True if successful</returns>
    public static string FormatX( ulong num, string s ) => string.Format( CultureInfo.InvariantCulture, s, num );

  }
}
