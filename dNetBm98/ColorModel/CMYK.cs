using System.Drawing;

using static dNetBm98.XColor;

namespace dNetBm98.ColorModel
{
  /// <summary>
  /// CMYK ColorModel
  ///  C(yan)    : 0..1  [%/100]
  ///  M(agenta) : 0..1  [%/100]
  ///  Y(ellow)  : 0..1  [%/100]
  ///  K(ey)     : 0..1  [%/100] (Black)
  /// </summary>
  public struct CMYK
  {
    /// <summary>
    /// An Empty CMYK (set to 0) 
    /// </summary>
    public static CMYK Empty => new CMYK( 0, 0, 0, 0 );

    double _c;
    double _m;
    double _y;
    double _k;

    /// <summary>
    /// cTor:
    /// </summary>
    public CMYK( double c = 0, double m = 0, double y = 0, double k = 0 )
    {
      _c = c;
      _m = m;
      _y = y;
      _k = k;
    }


    /// <summary>
    /// Cyan Part
    /// </summary>
    public double C {
      get { return _c; }
      set {
        _c = value;
        _c = _c > 1 ? 1 : _c < 0 ? 0 : _c;
      }
    }

    /// <summary>
    /// Magenta Part
    /// </summary>
    public double M {
      get { return _m; }
      set {
        _m = value;
        _m = _m > 1 ? 1 : _m < 0 ? 0 : _m;
      }
    }

    /// <summary>
    /// Yellow Part
    /// </summary>
    public double Y {
      get { return _y; }
      set {
        _y = value;
        _y = _y > 1 ? 1 : _y < 0 ? 0 : _y;
      }
    }

    /// <summary>
    /// Key (Blackness) Part
    /// </summary>
    public double K {
      get { return _k; }
      set {
        _k = value;
        _k = _k > 1 ? 1 : _k < 0 ? 0 : _k;
      }
    }

    /// <summary>
    /// String representation of this object
    /// </summary>
    /// <returns>A String</returns>
    public override string ToString( )
    {
      return $"({_c:0.00},{_m:0.00},{_y:0.00},{_k:0.00})";
    }


    #region RGB vs CMYK

    /// <summary>
    /// Converts CMYK to RGB (set Transparency to 255)
    /// </summary>
    /// <param name="cmyk">A color to convert</param>
    /// <returns>A Color object</returns>
    public static Color ToRgb( CMYK cmyk )
    {
      /*
       * https://www.rapidtables.com/convert/color/cmyk-to-rgb.html
       * 
          The R,G,B values are given in the range of 0..255.

          The red (R) color is calculated from the cyan (C) and black (K) colors:
              R = 255 × (1-C) × (1-K)

          The green color (G) is calculated from the magenta (M) and black (K) colors:
              G = 255 × (1-M) × (1-K)

          The blue color (B) is calculated from the yellow (Y) and black (K) colors:
              B = 255 × (1-Y) × (1-K)
       
       */
      int red, green, blue;
      red = ColorComp( 255 * (1 - cmyk.C) * (1 - cmyk.K) );
      green = ColorComp( 255 * (1 - cmyk.M) * (1 - cmyk.K) );
      blue = ColorComp( 255 * (1 - cmyk.Y) * (1 - cmyk.K) );

      return Color.FromArgb( red, green, blue );
    }
    
    /// <summary>
         /// Converts RGB to CMYK (not considering Transparency)
         /// </summary>
         /// <param name="rgb">A color to convert.</param>
         /// <returns>A CMYK object</returns>
    public static CMYK FromRgb( Color rgb )
    {
      /*
       * https://www.rapidtables.com/convert/color/rgb-to-cmyk.html
       * 
          The R,G,B values are divided by 255 to change the range from 0..255 to 0..1:

          R' = R/255
          G' = G/255
          B' = B/255

          The black key (K) color is calculated from the red (R'), green (G') and blue (B') colors:
            K = 1-max(R', G', B')

          The cyan color (C) is calculated from the red (R') and black (K) colors:
            C = (1-R'-K) / (1-K)

          The magenta color (M) is calculated from the green (G') and black (K) colors:
            M = (1-G'-K) / (1-K)

          The yellow color (Y) is calculated from the blue (B') and black (K) colors:
            Y = (1-B'-K) / (1-K)       
       */

      double r = rgb.R / 255.0;
      double g = rgb.G / 255.0;
      double b = rgb.B / 255.0;

      CMYK _cmyk = CMYK.Empty;
      _cmyk.K = 1 - XMath.Max( r, g, b );
      _cmyk.C = (1 - r - _cmyk.K) / (1 - _cmyk.K);
      _cmyk.M = (1 - g - _cmyk.K) / (1 - _cmyk.K);
      _cmyk.Y = (1 - b - _cmyk.K) / (1 - _cmyk.K);

      return _cmyk;
    }

    #endregion

  }

}
