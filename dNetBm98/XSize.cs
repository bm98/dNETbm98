using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace dNetBm98
{
  /// <summary>
  /// Using a .Net System.Drawing.Size/SizeF: and adding some methods
  /// </summary>
  public static class XSize
  {
    /*
     System.Drawing.Size:
        Size(Int32, Int32)      Initializes a new instance of the Size structure from the specified dimensions.
        Size(Point)             Initializes a new instance of the Size structure from the specified Point structure.

        Empty                   Gets a Size structure that has a Height and Width value of 0.

        Height                  Gets or sets the vertical component of this Size structure.
        IsEmpty                 Tests whether this Size structure has width and height of 0.
        Width                   Gets or sets the horizontal component of this Size structure.

        Add(Size, Size)         Adds the width and height of one Size structure to the width and height of another Size structure.
        Ceiling(SizeF)          Converts the specified SizeF structure to a Size structure by rounding the values of the Size structure to the next higher integer values.
        Equals(Object)          Tests to see whether the specified object is a Size structure with the same dimensions as this Size structure.
        Equals(Size)            Indicates whether the current object is equal to another object of the same type.
        GetHashCode()           Returns a hash code for this Size structure.
        Round(SizeF)            Converts the specified SizeF structure to a Size structure by rounding the values of the SizeF structure to the nearest integer values.
        Subtract(Size, Size)    Subtracts the width and height of one Size structure from the width and height of another Size structure.
        ToString()              Creates a human-readable string that represents this Size structure.
        Truncate(SizeF)         Converts the specified SizeF structure to a Size structure by truncating the values of the SizeF structure to the next lower integer values.

        Addition(Size, Size)    Adds the width and height of one Size structure to the width and height of another Size structure.
        Equality(Size, Size)    Tests whether two Size structures are equal.
        Explicit(Size to Point) Converts the specified Size structure to a Point structure.
        Implicit(Size to SizeF) Converts the specified Size structure to a SizeF structure.
        Inequality(Size, Size)  Tests whether two Size structures are different.
        Subtraction(Size, Size) Subtracts the width and height of one Size structure from the width and height of another Size structure.

      Extension:
        ToPoint                 Returns a Point item from the Size Values
        Multiply(Int32)         Scale the Size with a factor
        Multiply(Single)        Scale the Size with a factor
        Length                  Length (as Vector length)
     */



    /// <summary>
    /// Returns a Size
    /// </summary>
    /// <returns>A Size</returns>
    public static Size ToSize( this SizeF _s ) => new Size( (int)_s.Width, (int)_s.Height );
    /// <summary>
    /// Returns a SizeF
    /// </summary>
    /// <returns>A Size</returns>
    public static SizeF ToSizeF( this Size _s ) => new SizeF( _s.Width, _s.Height );


    /// <summary>
    /// Returns a Point item from the Size Values
    /// </summary>
    /// <returns>A Point</returns>
    public static Point ToPoint( this Size _s ) => new Point( _s.Width, _s.Height );
    /// <summary>
    /// Returns a Point item from the Size Values
    /// </summary>
    /// <returns>A Point</returns>
    public static PointF ToPointF( this Size _s ) => new PointF( _s.Width, _s.Height );

    /// <summary>
    /// Returns this scaled with the given factor
    /// </summary>
    public static Size Multiply( this Size _s, Int32 f ) { return new Size( _s.Width * f, _s.Height * f ); }
    /// <summary>
    /// Returns this scaled with the given factor
    /// </summary>
    public static Size Multiply( this Size _s, Single f ) { return new Size( (int)(_s.Width * f), (int)(_s.Height * f) ); }
    /// <summary>
    /// Returns this scaled with the Size
    /// </summary>
    public static Size Scale( this Size _s, Size size ) { return new Size( (int)(_s.Width * size.Width), (int)(_s.Height * size.Height) ); }
    /// <summary>
    /// Returns this scaled with the SizeF
    /// </summary>
    public static Size Scale( this Size _s, SizeF size ) { return new Size( (int)(_s.Width * size.Width), (int)(_s.Height * size.Height) ); }

    /// <summary>
    /// Length of this Size item (Vector Size)
    /// </summary>
    public static float Length( this Size _s ) => (float)Math.Sqrt( _s.Width * _s.Width + _s.Height * _s.Height );
    /// <summary>
    /// Returns the Center Point of the Size
    /// </summary>
    public static Point Center( this Size _s ) => new Point( _s.Width / 2, _s.Height / 2 );


    // SizeF

    /// <summary>
    /// Returns a PointF item from the SizeF Values
    /// </summary>
    /// <returns>A Point</returns>
    public static PointF ToPointF( this SizeF _s ) => new PointF( _s.Width, _s.Height );
    /// <summary>
    /// Returns this scaled with the given factor
    /// </summary>
    public static SizeF Multiply( this SizeF _s, Int32 f ) { return new SizeF( _s.Width * f, _s.Height * f ); }
    /// <summary>
    /// Returns this scaled with the given factor
    /// </summary>
    public static SizeF Multiply( this SizeF _s, Single f ) { return new SizeF( _s.Width * f, _s.Height * f ); }
    /// <summary>
    /// Returns this scaled with the give Size
    /// </summary>
    public static SizeF Scale( this SizeF _s, Size size ) { return new SizeF( _s.Width * size.Width, _s.Height * size.Height ); }
    /// <summary>
    /// Returns this scaled with the give SizeF
    /// </summary>
    public static SizeF Scale( this SizeF _s, SizeF size ) { return new SizeF( _s.Width * size.Width, _s.Height * size.Height ); }

    /// <summary>
    /// Length of this Size item (Vector Size)
    /// </summary>
    public static float Length( this SizeF _s ) => (float)Math.Sqrt( _s.Width * _s.Width + _s.Height * _s.Height );
    /// <summary>
    /// Returns the Center PointF of the SizeF
    /// </summary>
    public static PointF Center( this SizeF _s ) => new PointF( _s.Width / 2f, _s.Height / 2f );

    #region Serialization Support

    /// <summary>
    /// As Serialized string ({W=1,H=2})
    ///  culture invariant
    /// </summary>
    /// <param name="s">A Size</param>
    /// <returns>A string</returns>
    public static string AsSerString( this Size s )
    {
      return string.Format( CultureInfo.InvariantCulture, "{{W={0},H={1}}}", s.Width, s.Height );
    }
    /// <summary>
    /// As Serialized string ({W=1.1,H=2.0})
    ///  culture invariant: uses decimal point
    /// </summary>
    /// <param name="s">A SizeF</param>
    /// <returns>A string</returns>
    public static string AsSerString( this SizeF s )
    {
      return string.Format( CultureInfo.InvariantCulture, "{{W={0},H={1}}}", s.Width, s.Height );
    }

    private static Regex rxSzf = new Regex( @"^\{\s*W=(?<w>[+-]?\d+([.]\d+)?(E[+-]\d+)?)\s*,\s*H=(?<h>[+-]?\d+([.]\d+)?(E[+-]\d+)?)\s*\}$",
          RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase );

    /// <summary>
    /// Convert a Size from ToSerString() back to a Size ({X=1,Y=2})
    ///  culture invariant: uses decimal point
    /// </summary>
    /// <param name="ss">A Size.ToSerString() string</param>
    /// <returns>A Size</returns>
    public static Size SizeFromSerString( string ss )
    {
      // never fail
      try {
        Match match = rxSzf.Match( ss.Trim( ) );
        if (match.Success) {
          int w = (int)Math.Round( float.Parse( match.Groups["w"].Value, CultureInfo.InvariantCulture ) );
          int h = (int)Math.Round( float.Parse( match.Groups["h"].Value, CultureInfo.InvariantCulture ) );
          return new Size( w, h );
        }
      }
      catch { }

      return new Size( 0, 0 );
    }

    /// <summary>
    /// Convert a SizeF from ToSerString() back to a SizeF ({X=1,Y=2})
    ///  culture invariant: uses decimal point
    /// </summary>
    /// <param name="ss">A SizeF.ToSerString() string</param>
    /// <returns>A SizeF</returns>
    public static SizeF SizeFFromSerString( string ss )
    {
      // never fail
      try {
        Match match = rxSzf.Match( ss.Trim( ) );
        if (match.Success) {
          float w = float.Parse( match.Groups["w"].Value, CultureInfo.InvariantCulture );
          float h = float.Parse( match.Groups["h"].Value, CultureInfo.InvariantCulture );
          return new SizeF( w, h );
        }
      }
      catch { }

      return new SizeF( 0, 0 );
    }

    #endregion


  }

}