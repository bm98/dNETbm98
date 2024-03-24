using System;
using System.Drawing;

using static dNetBm98.XColor;

namespace dNetBm98.ColorModel
{
  /// <summary>
  /// Color Model HSL
  /// 
  ///  H(ue):        0..&lt;360 [°]
  ///  HS(caled):    0..1  [%/100] (Hue / 360)
  ///  S(aturation): 0..1  [%/100]
  ///  L(ightness ): 0..1  [%/100]
  /// </summary>
  public struct HSL
  {
    /// <summary>
    /// An Empty HSL (set to 0) 
    /// </summary>
    public static HSL Empty => new HSL( 0, 0, 0 );

    double _h;
    double _s;
    double _l;

    /// <summary>
    /// cTor:
    /// </summary>
    public HSL( double h = 0, double s = 0, double l = 0 )
    {
      _h = h;
      _s = s;
      _l = l;
    }

    /// <summary>
    /// cTor: Copy
    /// </summary>
    /// <param name="other"></param>
    public HSL( HSL other )
    {
      _h = other._h;
      _s = other._s;
      _l = other._l;
    }

    /// <summary>
    /// Hue Part
    /// </summary>
    public double H {
      get { return _h; }
      set {
        _h = value >= 360.0 ? value % 360d : value < 0.0 ? value + 360d : value;   // wrap around
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
    /// Lightness Part
    /// </summary>
    public double L {
      get { return _l; }
      set {
        _l = value > 1.0 ? 1.0 : value < 0.0 ? 0.0 : value;
      }
    }

    /// <summary>
    /// String representation of this object
    /// </summary>
    /// <returns>A String</returns>
    public override string ToString( )
    {
      return $"({_h:##0},{_s:0.00},{_l:0.00})";
    }

    #region RGB vs HSL

    /// <summary> 
    /// Converts a colour from HSL to RGB  (Transparency set to 255)
    /// </summary> 
    /// <remarks>Adapted from the algorithm in Foley and Van-Dam</remarks> 
    /// <param name="hsl">The HSL value</param> 
    /// <returns>A Color structure containing the equivalent RGB values</returns> 
    public static Color ToRgb( HSL hsl )
    {
      /*
       * https://www.rapidtables.com/convert/color/hsl-to-rgb.html

        When 0 ≤ H < 360, 0 ≤ S ≤ 1 and 0 ≤ L ≤ 1:

            C = (1 - |2L - 1|) × S

            X = C × (1 - |(H / 60°) mod 2 - 1|)

            m = L - C/2

            R',G',B' = (C,X,0) | (X,C,0) | (0,C,X) | (0,X,C) | (X,0,C) | (C,0,X) for H intervals of 60°

           (R,G,B) = ((R'+m)×255, (G'+m)×255,(B'+m)×255)

       */

      double C = (1 - Math.Abs( 2 * hsl.L - 1 )) * hsl.S;

      double X = C * (1 - Math.Abs( (hsl.H / 60) % 2 - 1 ));

      double m = hsl.L - C / 2;

      double r = 0, g = 0, b = 0;
      if ((0 <= hsl.H) && (hsl.H < 60)) { r = C; g = X; b = 0; }
      else if ((60 <= hsl.H) && (hsl.H < 120)) { r = X; g = C; b = 0; }
      else if ((120 <= hsl.H) && (hsl.H < 180)) { r = 0; g = C; b = X; }
      else if ((180 <= hsl.H) && (hsl.H < 240)) { r = 0; g = X; b = C; }
      else if ((240 <= hsl.H) && (hsl.H < 300)) { r = X; g = 0; b = C; }
      else if ((300 <= hsl.H) && (hsl.H < 360)) { r = C; g = 0; b = X; }

      Color c = Color.FromArgb( ColorComp( (r + m) * 255 ), ColorComp( (g + m) * 255 ), ColorComp( (b + m) * 255 ) );

      return c;
    }


    /// <summary> 
    /// Converts RGB to HSL (not considering Transparency)
    /// </summary> 
    /// <param name="c">A Color to convert</param> 
    /// <returns>An HSL value</returns> 
    public static HSL FromRgb( Color c )
    {
      /*
       * https://www.rapidtables.com/convert/color/rgb-to-hsl.html
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
               0            , Cmax=0
               Δ/(1-|2L-1|) , Cmax!=0   
              
            Lightness calculation:
              L = (Cmax + Cmin) / 2
       */
      double r = c.R / 255.0;
      double g = c.G / 255.0;
      double b = c.B / 255.0;

      double Cmax = Math.Max( r, Math.Max( g, b ) );
      double Cmin = Math.Min( r, Math.Min( g, b ) ); ;
      double delta = Cmax - Cmin;

      var hsl = new HSL( );
      if (delta == 0) { hsl.H = 0; }
      else if (Cmax == r) { hsl.H = 60 * (((g - b) / delta) % 6); }
      else if (Cmax == g) { hsl.H = 60 * (((b - r) / delta + 2) % 6); }
      else if (Cmax == b) { hsl.H = 60 * (((r - g) / delta + 4) % 6); }

      hsl.L = (Cmax + Cmin) / 2;

      if (Cmax == 0) { hsl.S = 0; }
      else { hsl.S = delta / (1 - Math.Abs( 2 * hsl.L - 1 )); }

      return hsl;
    }

    #endregion

  }

}
