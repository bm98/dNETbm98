using System;

namespace dNetBm98
{
  /// <summary>
  /// Math extensions
  /// </summary>
  public static class XMath
  {
    private const double R2D = 180.0 / Math.PI;
    private const decimal R2Dm = 180.0m / (decimal)Math.PI;
    private const double D2R = Math.PI / 180.0;
    private const decimal D2Rm = (decimal)Math.PI / 180.0m;

    #region Rounding

    /// <summary>
    /// Rounds to the quant given
    /// i.e. RoundInt( 12345, 100) = 12300
    /// </summary>
    public static int AsRoundInt( this long x, int quant ) => (quant > 0) ? (int)Math.Round( (double)x / quant ) * quant : int.MinValue;
    /// <summary>
    /// Rounds to the quant given
    /// i.e. RoundInt( 12345, 100) = 12300
    /// </summary>
    public static int RoundInt( long x, int quant ) => x.AsRoundInt( quant );


    /// <summary>
    /// Rounds to the quant given
    /// i.e. RoundInt( 12345, 100) = 12300
    /// </summary>
    public static int AsRoundInt( this double x, int quant ) => (quant > 0) ? (int)Math.Round( x / quant ) * quant : int.MinValue;
    /// <summary>
    /// Rounds to the quant given
    /// i.e. RoundInt( 12345, 100) = 12300
    /// </summary>
    public static int RoundInt( double x, int quant ) => x.AsRoundInt( quant );

    /// <summary>
    /// Returns a Rounded Up integer
    /// </summary>
    /// <param name="value">double value to be rounded to an integer</param>
    /// <returns>An Int</returns>
    public static int RoundUp( double value )
    {
      return (int)Math.Round( value, mode: MidpointRounding.AwayFromZero );
    }


    /// <summary>
    /// Rounds to the quant given
    /// i.e. RoundInt( 12345, 100) = 12300
    /// </summary>
    public static int AsRoundInt( this decimal x, int quant ) => (quant > 0) ? (int)Math.Round( x / quant ) * quant : int.MinValue;
    /// <summary>
    /// Rounds to the quant given
    /// i.e. RoundInt( 12345, 100) = 12300
    /// </summary>
    public static int RoundInt( decimal x, int quant ) => x.AsRoundInt( quant );

    /// <summary>
    /// Returns a Rounded Up integer
    /// </summary>
    /// <param name="value">double value to be rounded to an integer</param>
    /// <returns>An Int</returns>
    public static int RoundUp( decimal value )
    {
      return (int)Math.Round( value, mode: MidpointRounding.AwayFromZero );
    }


    #endregion

    #region Comparing values to be within an Epsilon

    /// <summary>
    /// Returns wether or not 2 values are closer together than epsilon
    /// </summary>
    /// <param name="v1">Value 1</param>
    /// <param name="v2">Value 2</param>
    /// <param name="epsilon">Closeness value (> double.Epsilon)</param>
    /// <returns>True if the the values are closer together than epsilon</returns>
    public static bool AboutEqual( decimal v1, decimal v2, decimal epsilon )
    {
      // sanity
      if (epsilon <= 1e-27m) throw new ArgumentException( "Epsilon minimum is 1e-27m" );

      if (v1.Equals( v2 )) return true; // trivial

      return Math.Abs( v1 - v2 ) < epsilon;
    }

    /// <summary>
    /// Returns wether or not 2 values are closer together than epsilon
    /// </summary>
    /// <param name="v1">Value 1</param>
    /// <param name="v2">Value 2</param>
    /// <param name="epsilon">Closeness value (> double.Epsilon)</param>
    /// <returns>True if the the values are closer together than epsilon</returns>
    public static bool AboutEqual( double v1, double v2, double epsilon )
    {
      // sanity
      if (epsilon <= double.Epsilon) throw new ArgumentException( "Epsilon minimum is double.Epsilon" );

      if (v1.Equals( v2 )) return true; // trivial
      // consider those as non equal
      if (double.IsNaN( v1 )) return false;
      if (double.IsNaN( v2 )) return false;
      if (double.IsInfinity( v1 )) return false;
      if (double.IsInfinity( v2 )) return false;

      return Math.Abs( v1 - v2 ) < epsilon;
    }

    /// <summary>
    /// Returns wether or not 2 values are closer together than epsilon
    /// </summary>
    /// <param name="v1">Value 1</param>
    /// <param name="v2">Value 2</param>
    /// <param name="epsilon">Closeness value (> float.Epsilon)</param>
    /// <returns>True if the the values are closer together than epsilon</returns>
    public static bool AboutEqual( float v1, float v2, float epsilon )
    {
      // sanity
      if (epsilon <= float.Epsilon) throw new ArgumentException( "Epsilon minimum is float.Epsilon" );

      if (v1.Equals( v2 )) return true; // trivial
      // consider those as non equal
      if (float.IsNaN( v1 )) return false;
      if (float.IsNaN( v2 )) return false;
      if (float.IsInfinity( v1 )) return false;
      if (float.IsInfinity( v2 )) return false;

      return Math.Abs( v1 - v2 ) < epsilon;
    }

    /// <summary>
    /// Returns wether or not 2 values are closer together than epsilon
    /// </summary>
    /// <param name="v1">Value 1</param>
    /// <param name="v2">Value 2</param>
    /// <param name="epsilon">Closeness value (> 0)</param>
    /// <returns>True if the the values are closer together than epsilon</returns>
    public static bool AboutEqual( long v1, long v2, long epsilon )
    {
      // sanity
      if (epsilon <= 0) throw new ArgumentException( "Epsilon minimum is 1" );

      if (v1.Equals( v2 )) return true; // trivial

      return Math.Abs( v1 - v2 ) < epsilon;
    }

    /// <summary>
    /// Returns wether or not 2 values are closer together than epsilon
    /// </summary>
    /// <param name="v1">Value 1</param>
    /// <param name="v2">Value 2</param>
    /// <param name="epsilon">Closeness value (> 0)</param>
    /// <returns>True if the the values are closer together than epsilon</returns>
    public static bool AboutEqual( int v1, int v2, int epsilon )
    {
      // sanity
      if (epsilon <= 0) throw new ArgumentException( "Epsilon minimum is 1" );

      if (v1.Equals( v2 )) return true; // trivial

      return Math.Abs( v1 - v2 ) < epsilon;
    }


    #endregion

    #region Clip (MinMax)

    // decimal

    /// <summary>
    /// Returns MinMax value of the argument and the borders (inclusive)
    /// </summary>
    public static decimal Clip( this decimal _dec, decimal min, decimal max ) => Math.Max( Math.Min( _dec, max ), min );
    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static decimal Clip( this ref decimal _dec, decimal mMin, decimal mMax ) => _dec = _dec.Clip( min: mMin, max: mMax );

    // double

    /// <summary>
    /// Clips a number to the specified minimum and maximum values.
    /// Returns the clipped value
    /// </summary>
    public static double Clip( this double _d, double min, double max ) => Math.Min( Math.Max( _d, min ), max );

    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static double Clip( this ref double _d, double dMin, double dMax ) => _d = _d.Clip( min: dMin, max: dMax );

    // float

    /// <summary>
    /// Clips a number to the specified minimum and maximum values.
    /// Returns the clipped value
    /// </summary>
    public static float Clip( this float _f, double min, double max ) => (float)Math.Min( Math.Max( _f, min ), max );

    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static float Clip( this ref float _f, double dMin, double dMax ) => _f = _f.Clip( min: dMin, max: dMax );

    // long

    /// <summary>
    /// Returns MinMax value of the argument and the borders (inclusive)
    /// </summary>
    public static long Clip( this long _l, long min, long max ) => Math.Min( Math.Max( _l, min ), max );

    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static long Clip( this ref long _l, long dMin, long dMax ) => _l = _l.Clip( min: dMin, max: dMax );

    // int

    /// <summary>
    /// Clips a number to the specified minimum and maximum values.
    /// Returns the clipped value
    /// </summary>
    public static int Clip( this int _i, int min, int max ) => (int)Math.Min( Math.Max( _i, min ), max );

    /// <summary>
    /// Clips the variable by the Min, Max argument
    /// </summary>
    public static int Clip( this ref int _i, int lMin, int lMax ) => _i = _i.Clip( min: lMin, max: lMax );

    #endregion

    #region Min/Max of comparable types

    /// <summary>
    /// Generic Minimum for comparable types
    /// </summary>
    /// <typeparam name="T">A comparable type</typeparam>
    /// <param name="v1">Value1</param>
    /// <param name="v2">Value2</param>
    /// <returns>The lesser of the two values</returns>
    public static T Min<T>( T v1, T v2 ) where T : IComparable<T>
    {
      if (v1.CompareTo( v2 ) < 0) return v1;
      else return v2;
    }
    /// <summary>
    /// Generic Maximum for comparable types
    /// </summary>
    /// <typeparam name="T">A comparable type</typeparam>
    /// <param name="v1">Value1</param>
    /// <param name="v2">Value2</param>
    /// <returns>The larger of the two values</returns>
    public static T Max<T>( T v1, T v2 ) where T : IComparable<T>
    {
      if (v1.CompareTo( v2 ) > 0) return v1;
      else return v2;
    }

    #endregion

    #region Min/Max of many

    /// <summary>
    /// Returns Max of the arguments
    /// </summary>
    public static decimal Max( decimal val1, decimal val2, decimal val3 ) => Math.Max( val1, Math.Max( val2, val3 ) );
    /// <summary>
    /// Returns Max of the arguments
    /// </summary>
    public static double Max( double val1, double val2, double val3 ) => Math.Max( val1, Math.Max( val2, val3 ) );
    /// <summary>
    /// Returns Max of the arguments
    /// </summary>
    public static float Max( float val1, float val2, float val3 ) => Math.Max( val1, Math.Max( val2, val3 ) );
    /// <summary>
    /// Returns Max of the arguments
    /// </summary>
    public static long Max( long val1, long val2, long val3 ) => Math.Max( val1, Math.Max( val2, val3 ) );
    /// <summary>
    /// Returns Max of the arguments
    /// </summary>
    public static int Max( int val1, int val2, int val3 ) => Math.Max( val1, Math.Max( val2, val3 ) );
    /// <summary>
    /// Returns Max of the arguments
    /// </summary>
    public static short Max( short val1, short val2, short val3 ) => Math.Max( val1, Math.Max( val2, val3 ) );


    /// <summary>
    /// Returns Min of the arguments
    /// </summary>
    public static decimal Min( decimal val1, decimal val2, decimal val3 ) => Math.Min( val1, Math.Min( val2, val3 ) );
    /// <summary>
    /// Returns Min of the arguments
    /// </summary>
    public static double Min( double val1, double val2, double val3 ) => Math.Min( val1, Math.Min( val2, val3 ) );
    /// <summary>
    /// Returns Min of the arguments
    /// </summary>
    public static float Min( float val1, float val2, float val3 ) => Math.Min( val1, Math.Min( val2, val3 ) );
    /// <summary>
    /// Returns Min of the arguments
    /// </summary>
    public static long Min( long val1, long val2, long val3 ) => Math.Min( val1, Math.Min( val2, val3 ) );
    /// <summary>
    /// Returns Min of the arguments
    /// </summary>
    public static int Min( int val1, int val2, int val3 ) => Math.Min( val1, Math.Min( val2, val3 ) );
    /// <summary>
    /// Returns Max of the arguments
    /// </summary>
    public static short Min( short val1, short val2, short val3 ) => Math.Min( val1, Math.Min( val2, val3 ) );

    #endregion

    #region Deg / Rad

    /// <summary>
    /// Returns the angle in radians
    /// </summary>
    public static decimal ToRadians( this decimal angleInDegree ) => angleInDegree * D2Rm;
    /// <summary>
    /// Returns the angle in Degrees
    /// </summary>
    public static decimal ToDegrees( this decimal angleInRadians ) => angleInRadians * R2Dm;

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
