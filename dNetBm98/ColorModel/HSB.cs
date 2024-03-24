using System;
using System.Drawing;

using static dNetBm98.XColor;

namespace dNetBm98.ColorModel
{
  /// <summary>
  /// HSB color model
  ///  H(ue):        0..&lt;360 [°]
  ///  HS(caled):    0..1  [%/100] (Hue / 360)
  ///  S(aturation): 0..1  [%/100]
  ///  B(rightness): 0..1  [%/100]  V in HSV
  /// </summary>
  public struct HSB
  {
    /// <summary>
    /// An Empty HSL (set to 0) 
    /// </summary>
    public static HSB Empty => new HSB( 0, 0, 0 );

    double _h;
    double _s;
    double _b;

    /// <summary>
    /// cTor:
    /// </summary>
    public HSB( double h = 0, double s = 0, double b = 0 )
    {
      _h = h;
      _s = s;
      _b = b;
    }

    /// <summary>
    /// cTor: Copy
    /// </summary>
    /// <param name="other"></param>
    public HSB( HSB other )
    {
      _h = other._h;
      _s = other._s;
      _b = other._b;
    }

    /// <summary>
    /// Hue Part
    /// </summary>
    public double H {
      get { return _h; }
      set {
        _h = value >= 360.0 ? value % 360d : value < 0.0 ? value+360d : value;   // wrap around
      }
    }
    /// <summary>
    /// Scaled Hue Part (0..&lt;360.0 -> 0..&lt;1.0)
    /// </summary>
    public double HS {
      get { return _h / 360.0; }
      set {
        var hs = value >= 1.0 ? 0.0 : value < 0.0 ? 0.0 : value; // clamp before 360
        _h = hs * 360.0;
      }
    }

    /// <summary>
    /// Saturation Part
    /// </summary>
    public double S {
      get { return _s; }
      set {
        _s = value > 1.0 ? 1.0 : value < 0.0 ? 0.0 : value;
      }
    }

    /// <summary>
    /// Brightness Part
    /// </summary>
    public double B {
      get { return _b; }
      set {
        _b = value > 1.0 ? 1.0 : value < 0.0 ? 0.0 : value;
      }
    }

    /// <summary>
    /// String representation of this object
    /// </summary>
    /// <returns>A String</returns>
    public override string ToString( )
    {
      return $"({_h:##0},{_s:0.00},{_b:0.00})";
    }


    #region RGB vs HSB / HSV

    /// <summary> 
    /// Converts a colour from HSB to RGB (set Transparency to 255)
    /// </summary> 
    /// <param name="hsb">The HSB value</param> 
    /// <returns>A Color structure containing the equivalent RGB values</returns> 
    public static Color ToRgb( HSB hsb )
    {
      /*
       * https://www.rapidtables.com/convert/color/hsv-to-rgb.html

        When 0 ≤ H < 360, 0 ≤ S ≤ 1 and 0 ≤ L ≤ 1:

            C = B × S

            X = C × (1 - |(H / 60°) mod 2 - 1|)

            m = B - C

            R',G',B' = (C,X,0) | (X,C,0) | (0,C,X) | (0,X,C) | (X,0,C) | (C,0,X) for H intervals of 60°

           (R,G,B) = ((R'+m)×255, (G'+m)×255,(B'+m)×255)

       */

      double C = hsb.B * hsb.S;

      double X = C * (1 - Math.Abs( (hsb.H / 60) % 2 - 1 ));

      double m = hsb.B - C;

      double r = 0, g = 0, b = 0;
      if ((0 <= hsb.H) && (hsb.H < 60)) { r = C; g = X; b = 0; }
      else if ((60 <= hsb.H) && (hsb.H < 120)) { r = X; g = C; b = 0; }
      else if ((120 <= hsb.H) && (hsb.H < 180)) { r = 0; g = C; b = X; }
      else if ((180 <= hsb.H) && (hsb.H < 240)) { r = 0; g = X; b = C; }
      else if ((240 <= hsb.H) && (hsb.H < 300)) { r = X; g = 0; b = C; }
      else if ((300 <= hsb.H) && (hsb.H < 360)) { r = C; g = 0; b = X; }

      Color c = Color.FromArgb( ColorComp( (r + m) * 255 ), ColorComp( (g + m) * 255 ), ColorComp( (b + m) * 255 ) );

      return c;
    }

    /// <summary> 
    /// Converts RGB to HSB (not considering Transparency)
    /// </summary> 
    /// <param name="c">A Color to convert</param> 
    /// <returns>An HSB value</returns> 
    public static HSB FromRgb( Color c )
    {
      /*
       * https://www.rapidtables.com/convert/color/rgb-to-hsv.html
       * 
       * The R,G,B values are divided by 255 to change the range from 0..255 to 0..1:

            R' = R/255
            G' = G/255
            B' = B/255

            Cmax = max(R', G', B')
            Cmin = min(R', G', B')
            Δ = Cmax - Cmin

            Hue calculation:
             H=
                0                    , Δ=0
                60 x ( (G'-B')/Δ % 6 ) , Cmax=R
                60 x ( (B'-R')/ + 2 )  , Cmax=G
                60 x ( (R'-G')/ + 4 )  , Cmax=B

            Saturation calculation:
             S=
               0        , Cmax=0
               Δ/Cmax   , Cmax!=0   
              
            Brightness calculation actually B in HSB:
              V = Cmax
       */
      double r = c.R / 255.0;
      double g = c.G / 255.0;
      double b = c.B / 255.0;

      double Cmax = Math.Max( r, Math.Max( g, b ) );
      double Cmin = Math.Min( r, Math.Min( g, b ) ); ;
      double delta = Cmax - Cmin;

      var hsb = new HSB( );
      if (delta == 0) { hsb.H = 0; }
      else if (Cmax == r) { hsb.H = 60 * (((g - b) / delta) % 6); }
      else if (Cmax == g) { hsb.H = 60 * (((b - r) / delta + 2)); }
      else if (Cmax == b) { hsb.H = 60 * (((r - g) / delta + 4)); }

      if (Cmax == 0) { hsb.S = 0; }
      else { hsb.S = delta / Cmax; }

      hsb.B = Cmax;

      return hsb;
    }

    #endregion

  }
}
