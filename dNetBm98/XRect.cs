using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace dNetBm98
{
  /// <summary>
  /// Using a .Net System.Drawing.Point: and adding some methods
  /// </summary>
  public static class XRect
  {
    /*
     System.Drawing.Rectangle:
        Rectangle(Int32, Int32, Int32, Int32)     Initializes a new instance of the Rectangle class with the specified location and size.
        Rectangle(Point, Size)                    Initializes a new instance of the Rectangle class with the specified location and size.

        Empty                                     Represents a Rectangle structure with its properties left uninitialized.

        Bottom                                    Gets the y-coordinate that is the sum of the Y and Height property values of this Rectangle structure.
        Height                                    Gets or sets the height of this Rectangle structure.
        IsEmpty                                   Tests whether all numeric properties of this Rectangle have values of zero.
        Left                                      Gets the x-coordinate of the left edge of this Rectangle structure.
        Location                                  Gets or sets the coordinates of the upper-left corner of this Rectangle structure.
        Right                                     Gets the x-coordinate that is the sum of X and Width property values of this Rectangle structure.
        Size                                      Gets or sets the size of this Rectangle.
        Top                                       Gets the y-coordinate of the top edge of this Rectangle structure.
        Width                                     Gets or sets the width of this Rectangle structure.
        X                                         Gets or sets the x-coordinate of the upper-left corner of this Rectangle structure.
        Y                                         Gets or sets the y-coordinate of the upper-left corner of this Rectangle structure.

        Ceiling(RectangleF)       Converts the specified RectangleF structure to a Rectangle structure by rounding the RectangleF values to the next higher integer values.
        Contains(Int32, Int32)    Determines if the specified point is contained within this Rectangle structure.
        Contains(Point)           Determines if the specified point is contained within this Rectangle structure.
        Contains(Rectangle)       Determines if the rectangular region represented by rect is entirely contained within this Rectangle structure.
        Equals(Object)            Tests whether obj is a Rectangle structure with the same location and size of this Rectangle structure.
        Equals(Rectangle)         Indicates whether the current object is equal to another object of the same type.
        FromLTRB(Int32, Int32, Int32, Int32) Creates a Rectangle structure with the specified edge locations.
        GetHashCode()             Returns the hash code for this Rectangle structure. For information about the use of hash codes, see GetHashCode() .
        Inflate(Int32, Int32)     Enlarges this Rectangle by the specified amount.
        Inflate(Rectangle, Int32, Int32) Creates and returns an enlarged copy of the specified Rectangle structure. The copy is enlarged by the specified amount. The original Rectangle structure remains unmodified.
        Inflate(Size)             Enlarges this Rectangle by the specified amount.
        Intersect(Rectangle)      Replaces this Rectangle with the intersection of itself and the specified Rectangle.
        Intersect(Rectangle, Rectangle) Returns a third Rectangle structure that represents the intersection of two other Rectangle structures. If there is no intersection, an empty Rectangle is returned.
        IntersectsWith(Rectangle) Determines if this rectangle intersects with rect.
        Offset(Int32, Int32)      Adjusts the location of this rectangle by the specified amount.
        Offset(Point)             Adjusts the location of this rectangle by the specified amount.
        Round(RectangleF)         Converts the specified RectangleF to a Rectangle by rounding the RectangleF values to the nearest integer values.
        ToString()                Converts the attributes of this Rectangle to a human-readable string.
        Truncate(RectangleF)      Converts the specified RectangleF to a Rectangle by truncating the RectangleF values.
        Union(Rectangle, Rectangle) Gets a Rectangle structure that contains the union of two Rectangle structures.

        Equality(Rectangle, Rectangle)    Tests whether two Rectangle structures have equal location and size.
        Inequality(Rectangle, Rectangle)  Tests whether two Rectangle structures differ in location or size.

      Extension:
        FromLTRB      Return a Rectangle from the given left,top,right,bottom values
        RightBottom   Return the Right Bottom Point
        RightTop      Return the Right Top Point
        LeftBottom   Return the Right Bottom Point
        LeftTop      Return the Right Top Point
        OffsetNegative    Adjusts the location of this rectangle by the specified amount in inverse direction.

     */
    /// <summary>
    /// Return a Rectangle from left,top,right,bottom values
    /// </summary>
    /// <returns>A Rectangle</returns>
    public static Rectangle FromLTRB( Int32 left, Int32 top, Int32 right, Int32 bottom ) => new Rectangle( left, top, right - left, bottom - top );
    /// <summary>
    /// Return the Right Bottom Point
    /// </summary>
    /// <returns>A Point</returns>
    public static Point RightBottom( this Rectangle _r ) => new Point( _r.Right, _r.Bottom );
    /// <summary>
    /// Return the Right Top Point
    /// </summary>
    /// <returns>A Point</returns>
    public static Point RightTop( this Rectangle _r ) => new Point( _r.Right, _r.Top );
    /// <summary>
    /// Return the Left Bottom Point
    /// </summary>
    /// <returns>A Point</returns>
    public static Point LeftBottom( this Rectangle _r ) => new Point( _r.Left, _r.Bottom );
    /// <summary>
    /// Return the Left Top Point
    /// </summary>
    /// <returns>A Point</returns>
    public static Point LeftTop( this Rectangle _r ) => new Point( _r.Left, _r.Top );
    /// <summary>
    /// Adjusts the location of this rectangle by the specified amount in inverse direction.
    /// </summary>
    public static void OffsetNegative( this Rectangle _r, Point pos ) => _r.Offset( -pos.X, -pos.Y );

    #region Serialization Support

    /// <summary>
    /// As Serialized string ({X=1,Y=2,W=3,H=4})
    ///  culture invariant
    /// </summary>
    /// <param name="r">A Rectangle</param>
    /// <returns>A string</returns>
    public static string AsSerString( this Rectangle r )
    {
      return string.Format( CultureInfo.InvariantCulture, "{{X={0},Y={1},W={2},H={3}}}", r.X, r.Y, r.Width, r.Height );
    }
    /// <summary>
    /// As Serialized string ({X=1,Y=2,W=3,H=4})
    ///  culture invariant
    /// </summary>
    /// <param name="r">A RectangleF</param>
    /// <returns>A string</returns>
    public static string AsSerString( this RectangleF r )
    {
      return string.Format( CultureInfo.InvariantCulture, "{{X={0},Y={1},W={2},H={3}}}", r.X, r.Y, r.Width, r.Height );
    }

    private static Regex rxRz = new Regex( @"^\{\s*X=(?<x>[+-]?\d+([.]\d+)?(E[+-]\d+)?)\s*,\s*Y=(?<y>[+-]?\d+([.]\d+)?(E[+-]\d+)?)\s*,\s*W=(?<w>[+-]?\d+([.]\d+)?(E[+-]\d+)?)\s*,\s*H=(?<h>[+-]?\d+([.]\d+)?(E[+-]\d+)?)\s*\}$",
          RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase );

    /// <summary>
    /// Convert a Rectangle from ToSerString() back to a Rectangle ({L=1,T=2,R=3,B=4})
    ///  culture invariant
    /// </summary>
    /// <param name="ss">A Rectangle.ToSerString() string</param>
    /// <returns>A Rectangle</returns>
    public static Rectangle RectangleFromSerString( string ss )
    {
      // never fail
      try {
        Match match = rxRz.Match( ss.Trim( ) );
        if (match.Success) {
          int x = (int)Math.Round( float.Parse( match.Groups["x"].Value, CultureInfo.InvariantCulture ) );
          int y = (int)Math.Round( float.Parse( match.Groups["y"].Value, CultureInfo.InvariantCulture ) );
          int w = (int)Math.Round( float.Parse( match.Groups["w"].Value, CultureInfo.InvariantCulture ) );
          int h = (int)Math.Round( float.Parse( match.Groups["h"].Value, CultureInfo.InvariantCulture ) );
          return new Rectangle( x, y, w, h );
        }
      }
      catch { }

      return new Rectangle( 0, 0, 0, 0 );
    }

    /// <summary>
    /// Convert a RectangleF from ToSerString() back to a RectangleF ({L=1,T=2,R=3,B=4})
    ///  culture invariant
    /// </summary>
    /// <param name="ss">A RectangleF.ToSerString() string</param>
    /// <returns>A RectangleF</returns>
    public static RectangleF RectangleFFromSerString( string ss )
    {
      // never fail
      try {
        Match match = rxRz.Match( ss.Trim( ) );
        if (match.Success) {
          float x = float.Parse( match.Groups["x"].Value, CultureInfo.InvariantCulture );
          float y = float.Parse( match.Groups["y"].Value, CultureInfo.InvariantCulture );
          float w = float.Parse( match.Groups["w"].Value, CultureInfo.InvariantCulture );
          float h = float.Parse( match.Groups["h"].Value, CultureInfo.InvariantCulture );
          return new RectangleF( x, y, w, h );
        }
      }
      catch { }

      return new RectangleF( 0, 0, 0, 0 );
    }

    #endregion


  }
}
