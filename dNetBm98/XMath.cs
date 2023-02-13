using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace dNetBm98
{
  /// <summary>
  /// Math extensions
  /// </summary>
  public static class XMath
  {
    static readonly double R2D = 180.0 / PI;
    static readonly double D2R = PI / 180.0;

    #region Clip (MinMax)

    // double

    /// <summary>
    /// Clips a number to the specified minimum and maximum values.
    /// Returns the clipped value
    /// </summary>
    public static double Clip( this double _d, double minValue, double maxValue ) => Min( Max( _d, minValue ), maxValue );

    /// <summary>
    /// Clips a number to the specified minimum and maximum values.
    /// Returns the clipped value
    /// </summary>
    public static double Clip( this int _i, double minValue, double maxValue ) => Min( Max( _i, minValue ), maxValue );

    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static double Clip( this ref double _d, double dMin, double dMax ) => _d = Clip( _d, dMin, dMax );

    // float

    /// <summary>
    /// Clips a number to the specified minimum and maximum values.
    /// Returns the clipped value
    /// </summary>
    public static float Clip( this float _f, double minValue, double maxValue ) => (float)Min( Max( _f, minValue ), maxValue );

    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static float Clip( this ref float _f, double dMin, double dMax ) => _f = Clip( _f, dMin, dMax );

    // long

    /// <summary>
    /// Returns MinMax value of the argument and the borders (inclusive)
    /// </summary>
    public static long Clip( this long _l, long dMin, long dMax ) => Max( Min( _l, dMax ), dMin );

    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static long Clip( this ref long _l, long dMin, long dMax ) => _l = Clip( _l, dMin, dMax );

    // int

    /// <summary>
    /// Clips a number to the specified minimum and maximum values.
    /// Returns the clipped value
    /// </summary>
    public static int Clip( this int _i, int minValue, int maxValue ) => (int)Min( Max( _i, minValue ), maxValue );

    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static int Clip( this ref int _i, long dMin, long dMax ) => _i = (int)Clip( (long)_i, dMin, dMax );

    // decimal

    /// <summary>
    /// Returns MinMax value of the argument and the borders (inclusive)
    /// </summary>
    public static decimal Clip( this decimal _dec, decimal dMin, decimal dMax ) => Max( Min( _dec, dMax ), dMin );
    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static decimal Clip( this ref decimal _dec, decimal dMin, decimal dMax ) => _dec = Clip( _dec, dMin, dMax );

    #endregion

    #region Deg / Rad

    /// <summary>
    /// Returns the angle in radians
    /// </summary>
    public static double ToRadians( this double angleInDegree ) => angleInDegree * D2R;

    /// <summary>
    /// Returns the angle in Degrees
    /// </summary>
    public static double ToDegrees( this double angleInRadians ) => angleInRadians * R2D;

    /// <summary>
    /// Returns the angle in radians
    /// </summary>
    public static float ToRadians( this float angleInDegree ) => (float)(angleInDegree * D2R);
    /// <summary>
    /// Returns the angle in Degrees
    /// </summary>
    public static float ToDegrees( this float angleInRadians ) => (float)(angleInRadians * R2D);

    #endregion

    #region Even / Odd

    /// <summary>
    /// True for an Even number
    /// </summary>
    public static bool Even( this byte _i ) => ((_i % 2) == 0);
    /// <summary>
    /// True for an Odd number
    /// </summary>
    public static bool Odd( this byte _i ) => !_i.Even( );
    /// <summary>
    /// True for an Even number
    /// </summary>
    public static bool Even( this short _i ) => ((_i % 2) == 0);
    /// <summary>
    /// True for an Odd number
    /// </summary>
    public static bool Odd( this short _i ) => !_i.Even( );
    /// <summary>
    /// True for an Even number
    /// </summary>
    public static bool Even( this ushort _i ) => ((_i % 2) == 0);
    /// <summary>
    /// True for an Odd number
    /// </summary>
    public static bool Odd( this ushort _i ) => !_i.Even( );
    /// <summary>
    /// True for an Even number
    /// </summary>
    public static bool Even( this int _i ) => ((_i % 2) == 0);
    /// <summary>
    /// True for an Odd number
    /// </summary>
    public static bool Odd( this int _i ) => !_i.Even( );
    /// <summary>
    /// True for an Even number
    /// </summary>
    public static bool Even( this uint _i ) => ((_i % 2) == 0);
    /// <summary>
    /// True for an Odd number
    /// </summary>
    public static bool Odd( this uint _i ) => !_i.Even( );
    /// <summary>
    /// True for an Even number
    /// </summary>
    public static bool Even( this long _i ) => ((_i % 2) == 0);
    /// <summary>
    /// True for an Odd number
    /// </summary>
    public static bool Odd( this long _i ) => !_i.Even( );
    /// <summary>
    /// True for an Even number
    /// </summary>
    public static bool Even( this ulong _i ) => ((_i % 2) == 0);
    /// <summary>
    /// True for an Odd number
    /// </summary>
    public static bool Odd( this ulong _i ) => !_i.Even( );

    #endregion

  }
}
