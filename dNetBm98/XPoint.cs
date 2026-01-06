using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace dNetBm98
{
  /// <summary>
  /// Using a .Net System.Drawing.Point/PointF: and adding some methods
  /// </summary>
  public static class XPoint
  {
    /*
     System.Drawing.Point:
        Point(Int32)            Initializes a new instance of the Point struct using coordinates specified by an integer value.
        Point(Int32, Int32)     Initializes a new instance of the Point struct with the specified coordinates.
        Point(Size)             Initializes a new instance of the Point struct from a Size.

        Empty                   Represents a Point that has X and Y values set to zero.

        IsEmpty                 Gets a value indicating whether this Point is empty.
        X                       Gets or sets the x-coordinate of this Point.
        Y                       Gets or sets the y-coordinate of this Point.

        Add(Point, Size)        Adds the specified Size to the specified Point.
        Ceiling(PointF)         Converts the specified PointF to a Point by rounding the values of the PointF to the next higher integer values.
        Equals(Object)          Specifies whether this point instance contains the same coordinates as the specified object.
        Equals(Point)           Specifies whether this point instance contains the same coordinates as another point.
        GetHashCode()           Returns a hash code for this Point.
        Offset(Int32, Int32)    Translates this Point by the specified amount.
        Offset(Point)           Translates this Point by the specified Point.
        Round(PointF)           Converts the specified PointF to a Point object by rounding the PointF values to the nearest integer.
        Subtract(Point, Size)   Returns the result of subtracting specified Size from the specified Point.
        ToString()              Converts this Point to a human-readable string.
        Truncate(PointF)        Converts the specified PointF to a Point by truncating the values of the PointF.

        Addition(Point, Size)   Translates a Point by a given Size.
        Equality(Point, Point)  Compares two Point objects. The result specifies whether the values of the X and Y properties of the two Point objects are equal.
        Explicit(Point to Size) Converts the specified Point structure to a Size structure.
        Implicit(Point to PointF) Converts the specified Point structure to a PointF structure.
        Inequality(Point, Point) Compares two Point objects. The result specifies whether the values of the X or Y properties of the two Point objects are unequal.
        Subtraction(Point, Size) Translates a Point by the negative of a given Size.

      Extension:
        ToSize                   Returns A Size item from this Point values
        OffsetNegative(Point)    Translates this Point by the inverse of the specified Point.

     */
    // public static explicit operator Size( this Point, Point p ) -- not possible

    /// <summary>
    /// Returns a Point
    /// </summary>
    public static Point ToPoint( this PointF _p ) => new Point( (int)_p.X, (int)_p.Y );
    /// <summary>
    /// Returns a PointF
    /// </summary>
    public static PointF ToPointF( this Point _p ) => new PointF( _p.X, _p.Y );


    /// <summary>
    /// Returns A Size item from this Point values
    /// </summary>
    /// <returns>A Size</returns>
    public static Size ToSize( this Point _p ) => new Size( _p.X, _p.Y );
    /// <summary>
    /// Returns A Size item from this Point values
    /// </summary>
    /// <returns>A Size</returns>
    public static SizeF ToSizeF( this PointF _p ) => new SizeF( _p.X, _p.Y );
    /// <summary>
    /// Returns A Size item from this Point values
    /// </summary>
    /// <returns>A Size</returns>
    public static SizeF ToSizeF( this Point _p ) => new SizeF( _p.X, _p.Y );


    /// <summary>
    /// Translates this Point by the inverse of the specified Point.
    /// </summary>
    public static void OffsetNegative( this Point _p, Point p ) => _p.Offset( -p.X, -p.Y );

    /// <summary>
    /// Returns a Size streching from this point and an End Point
    /// </summary>
    public static Size ToSize( this Point _p, Point _pend ) => new Size( _pend.X - _p.X, _pend.Y - _p.Y );
    /// <summary>
    /// Returns a Size streching from this point and an End Point
    /// </summary>
    public static SizeF ToSizeF( this PointF _p, PointF _pend ) => new SizeF( _pend.X - _p.X, _pend.Y - _p.Y );
    /// <summary>
    /// Returns a Size streching from this point and an End Point
    /// </summary>
    public static SizeF ToSizeF( this Point _p, Point _pend ) => new SizeF( _pend.X - _p.X, _pend.Y - _p.Y );


    /// <summary>
    /// Returns the vector distance from this point to an end point
    /// </summary>
    public static float Distance( this Point _p, Point _pend ) => (float)_p.ToSize( _pend ).Length( );
    /// <summary>
    /// Returns the vector distance from this point to an end point
    /// </summary>
    public static float Distance( this PointF _p, PointF _pend ) => _p.ToSizeF( _pend ).Length( );



    /// <summary>
    /// Returns a Point containing the result of the Addition from this Point + other Point
    /// </summary>
    public static Point Add( this Point _p, Point _other ) => new Point( _p.X + _other.X, _p.Y + _other.Y );

    /// <summary>
    /// Returns a Point containing the result of the Addition from this Point + dx,dy 
    /// </summary>
    public static Point Add( this Point _p, int dx, int dy ) => new Point( _p.X + dx, _p.Y + dy );

    /// <summary>
    /// Add the other Point to this Point
    /// </summary>
    public static void Plus( this ref Point _p, Point _other ) { _p.X += _other.X; _p.Y += _other.Y; }

    /// <summary>
    /// Add dx,dy to this Point
    /// </summary>
    public static void Plus( this ref Point _p, int dx, int dy ) { _p.X += dx; _p.Y += dy; }


    /// <summary>
    /// Returns a PointF containing the result of the Addition from this PointF + other PointF
    /// </summary>
    public static PointF Add( this PointF _p, PointF _other ) => new PointF( _p.X + _other.X, _p.Y + _other.Y );

    /// <summary>
    /// Returns a PointF containing the result of the Addition from this PointF + dx,dy
    /// </summary>
    public static PointF Add( this PointF _p, double dx, double dy ) => new PointF( _p.X + (float)dx, _p.Y + (float)dy );

    /// <summary>
    /// Add the other PointF to this PointF
    /// </summary>
    public static void Plus( this ref PointF _p, PointF _other ) { _p.X += _other.X; _p.Y += _other.Y; }
    /// <summary>
    /// Add the other Point to this PointF
    /// </summary>
    public static void Plus( this ref PointF _p, Point _other ) { _p.X += _other.X; _p.Y += _other.Y; }

    /// <summary>
    /// Add the other Point to this PointF
    /// </summary>
    public static void Plus( this ref PointF _p, double dx, double dy ) { _p.X += (float)dx; _p.Y += (float)dy; }



    /// <summary>
    /// Returns a Point containing the result of the Subtraction from this Point - other Point
    /// </summary>
    public static Point Subtract( this Point _p, Point _other ) => new Point( _p.X - _other.X, _p.Y - _other.Y );

    /// <summary>
    /// Subtract the other Point from this Point
    /// </summary>
    public static void Minus( this ref Point _p, Point _other ) { _p.X -= _other.X; _p.Y -= _other.Y; }

    /// <summary>
    /// Returns a PointF containing the result of the Subtraction from this PointF - other PointF
    /// </summary>
    public static PointF Subtract( this PointF _p, PointF _other ) => new PointF( _p.X - _other.X, _p.Y - _other.Y );

    /// <summary>
    /// Subtract the other PointF from this PointF
    /// </summary>
    public static void Minus( this ref PointF _p, PointF _other ) { _p.X -= _other.X; _p.Y -= _other.Y; }
    /// <summary>
    /// Subtract the other Point from this PointF
    /// </summary>
    public static void Minus( this ref PointF _p, Point _other ) { _p.X -= _other.X; _p.Y -= _other.Y; }


    /// <summary>
    /// Multiply this Point with a factor (rounds)
    /// </summary>
    public static void Multiply( this ref Point _p, float factor ) { _p.X = (int)(_p.X * factor); _p.Y = (int)(_p.Y * factor); }
    /// <summary>
    /// Multiply this PointF with a factor
    /// </summary>
    public static void Multiply( this ref PointF _p, float factor ) { _p.X *= factor; _p.Y *= factor; }

    /// <summary>
    /// Multiply this Point with a SizeF in X and Y (rounds)
    /// </summary>
    public static void Multiply( this ref Point _p, SizeF size ) { _p.X = (int)(_p.X * size.Width); _p.Y = (int)(_p.Y * size.Height); }
    /// <summary>
    /// Multiply this PointF with a SizeF in X and Y
    /// </summary>
    public static void Multiply( this ref PointF _p, SizeF size ) { _p.X *= size.Width; _p.Y *= size.Height; }

    /// <summary>
    /// Returns the Multiplicative inverse value
    /// </summary>
    public static PointF MInverse( this PointF _p ) => new PointF( 1f / _p.X, 1f / _p.Y );
    /// <summary>
    /// Returns the Multiplicative inverse value
    /// </summary>
    public static PointF MInverse( this Point _p ) => new PointF( 1f / _p.X, 1f / _p.Y );


    /// <summary>
    /// Returns a new Point scaled by the given Size in X and Y (rounds)
    /// </summary>
    public static Point Scale( this Point _p, Size size ) => new Point( (int)(_p.X * size.Width), (int)(_p.Y * size.Height) );
    /// <summary>
    /// Returns a new Point scaled by the given SizeF in X and Y (rounds)
    /// </summary>
    public static Point Scale( this Point _p, SizeF size ) => new Point( (int)(_p.X * size.Width), (int)(_p.Y * size.Height) );
    /// <summary>
    /// Returns a new PointF scaled by the given Size in X and Y
    /// </summary>
    public static PointF Scale( this PointF _p, Size size ) => new PointF( _p.X * size.Width, _p.Y * size.Height );
    /// <summary>
    /// Returns a new PointF scaled by the given SizeF in X and Y
    /// </summary>
    public static PointF Scale( this PointF _p, SizeF size ) => new PointF( _p.X * size.Width, _p.Y * size.Height );

    /// <summary>
    /// Return the point between this and the other point
    /// </summary>
    /// <returns>A Mid Point</returns>
    public static Point MidPointOf( Point p1, Point p2 ) => new Point( p1.X + (p2.X - p1.X) / 2, p1.Y + (p2.Y - p1.Y) / 2 );
    /// <summary>
    /// Return the point between this and the other point
    /// </summary>
    /// <returns>A Mid Point</returns>
    public static Point MidPoint( this Point _p, Point other ) => MidPointOf( _p, other );

    /// <summary>
    /// Return the point between this and the other point
    /// </summary>
    /// <returns>A Mid Point</returns>
    public static PointF MidPointOf( PointF p1, PointF p2 ) => new PointF( p1.X + (p2.X - p1.X) / 2f, p1.Y + (p2.Y - p1.Y) / 2f );
    /// <summary>
    /// Return the point between this and the other point
    /// </summary>
    /// <returns>A Mid Point</returns>
    public static PointF MidPoint( this PointF _p, PointF other ) => MidPointOf( _p, other );

    #region Serialization Support

    /// <summary>
    /// As Serialized string ({X=1,Y=2})
    ///  culture invariant
    /// </summary>
    /// <param name="p">A Point</param>
    /// <returns>A string</returns>
    public static string AsSerString( this Point p )
    {
      return string.Format( CultureInfo.InvariantCulture, "{{X={0},Y={1}}}", p.X, p.Y );
    }
    /// <summary>
    /// As Serialized string ({X=1.1,Y=2.0})
    ///  culture invariant: uses decimal point
    /// </summary>
    /// <param name="p">A PointF</param>
    /// <returns>A string</returns>
    public static string AsSerString( this PointF p )
    {
      return string.Format( CultureInfo.InvariantCulture, "{{X={0},Y={1}}}", p.X, p.Y );
    }

    private static Regex rxPtf = new Regex( @"^\{\s*X=(?<x>[+-]?\d+([.]\d+)?(E[+-]\d+)?)\s*,\s*Y=(?<y>[+-]?\d+([.]\d+)?(E[+-]\d+)?)\s*\}$",
          RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase );

    /// <summary>
    /// Convert a Point from ToSerString() back to a Point ({X=1,Y=2})
    ///  culture invariant: uses decimal point
    ///  
    /// </summary>
    /// <param name="ps">A Point.ToSerString() string</param>
    /// <returns>A Point</returns>
    public static Point PointFromSerString( string ps )
    {
      // never fail
      try {
        Match match = rxPtf.Match( ps.Trim( ) );
        if (match.Success) {
          int x = (int)Math.Round( float.Parse( match.Groups["x"].Value, CultureInfo.InvariantCulture ) );
          int y = (int)Math.Round( float.Parse( match.Groups["y"].Value, CultureInfo.InvariantCulture ) );
          return new Point( x, y );
        }
      }
      catch { }

      return new Point( 0, 0 );
    }

    /// <summary>
    /// Convert a PointF from ToSerString() back to a PointF ({X=1,Y=2})
    ///  culture invariant: uses decimal point
    /// </summary>
    /// <param name="ps">A Point.ToSerString() string</param>
    /// <returns>A PointF</returns>
    public static PointF PointFFromSerString( string ps )
    {
      // never fail
      try {
        Match match = rxPtf.Match( ps.Trim( ) );
        if (match.Success) {
          float x = float.Parse( match.Groups["x"].Value, CultureInfo.InvariantCulture );
          float y = float.Parse( match.Groups["y"].Value, CultureInfo.InvariantCulture );
          return new PointF( x, y );
        }
      }
      catch { }

      return new PointF( 0, 0 );
    }

    #endregion

  }
}

