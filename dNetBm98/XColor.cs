using System;
using System.Drawing;

using dNetBm98.ColorModel;

namespace dNetBm98
{
  /// <summary>
  /// Some usefull extensions
  /// </summary>
  public static class XColor
  {
    #region Dimming

    /// <summary>
    /// Dimms a color by an amount percent (default 70%)
    /// </summary>
    public static void Dimm( this ref Color color, float percent = 30f )
    {
      percent = (100 - percent) * 0.01f;
      color = Color.FromArgb( color.A, (int)(color.R * percent), (int)(color.G * percent), (int)(color.B * percent) );
    }

    /// <summary>
    /// Returns a Dimmed color by an amount percent (default 70%)
    /// </summary>
    public static Color Dimmed( this Color color, float percent = 30f )
    {
      percent = (100 - percent) * 0.01f;
      return Color.FromArgb( color.A,
        (int)(color.R * percent), (int)(color.G * percent), (int)(color.B * percent) );
    }

    #endregion

    #region ToFrom ColorS(tring)

    // convert Color to and from string
    private static ColorConverter ccv = new ColorConverter( );

    /// <summary>
    /// Convert A Color to a ColorS ('r,g,b')
    /// </summary>
    /// <param name="color">A Color</param>
    /// <returns>A ColorS</returns>
    public static string ToColorS( this Color color )
    {
      // shall never fail...
      try {
        return ccv.ConvertToInvariantString( color );
      }
      catch { }
      return ccv.ConvertToInvariantString( Color.Black );
    }

    /// <summary>
    /// Convert a ColorS ( 'r,g,b' )  to a Color
    /// </summary>
    /// <param name="colorS">A ColorS</param>
    /// <returns>A Color</returns>
    public static Color FromColorS( this string colorS )
    {
      // shall never fail...
      try {
        return (Color)ccv.ConvertFromInvariantString( colorS );
      }
      catch { }
      return Color.Black;
    }

    /// <summary>
    /// Convert A Color to a ColorH ('#aarrggbb' hex notation)
    /// </summary>
    /// <param name="color">A Color</param>
    /// <returns>A ColorH</returns>
    public static string ToColorH( this Color color )
    {
      return "#" + color.ToArgb( ).ToString( "x8" );
    }

    /// <summary>
    /// Convert a ColorH ('#aarrggbb' hex notation)  to a Color
    /// </summary>
    /// <param name="colorH">A ColorH</param>
    /// <returns>A Color</returns>
    public static Color FromColorH( this string colorH )
    {
      // sanity
      if (string.IsNullOrEmpty( colorH )) return Color.Black;
      if (colorH.Length != 9) return Color.Black;
      if (colorH[0] != '#') return Color.Black;

      // shall never fail...
      try {
        int i = Convert.ToInt32( colorH.Substring( 1 ), 16 );
        return Color.FromArgb( i );
      }
      catch { }
      return Color.Black;
    }

    /// <summary>
    /// Convert A Color to a ColorH RGB ('#rrggbb' hex notation) ignoring transparency
    /// </summary>
    /// <param name="color">A Color</param>
    /// <returns>A ColorH</returns>
    public static string ToColorHrgb( this Color color )
    {
      return "#" + color.ToArgb( ).ToString( "x8" ).Substring( 2, 6 );
    }

    /// <summary>
    /// Convert a ColorH RGB ('#rrggbb' hex notation)  to a Color let A=255
    /// </summary>
    /// <param name="colorH">A ColorH</param>
    /// <returns>A Color</returns>
    public static Color FromColorHrgb( this string colorH )
    {
      // sanity
      if (string.IsNullOrEmpty( colorH )) return Color.Black;
      if (colorH.Length != 7) return Color.Black;
      if (colorH[0] != '#') return Color.Black;

      // shall never fail...
      try {
        int i = (int)(Convert.ToInt32( colorH.Substring( 1 ), 16 ) | 0xFF_000000);
        return Color.FromArgb( i );
      }
      catch { }
      return Color.Black;
    }

    #endregion

    #region Color Component Clamping

    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    /// <param name="component">An R,G, or B value</param>
    /// <returns>An int (clamped at 255)</returns>
    public static int ColorComp( double component )
    {
      if (component < 0) return 0;
      var i = (int)Math.Round( component );
      return Math.Min( i, 255 );
    }
    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    /// <param name="component">An R,G, or B value</param>
    /// <returns>An int (clamped at 255)</returns>
    public static int ColorComp( float component )
    {
      if (component < 0) return 0;
      var i = (int)Math.Round( (decimal)component ); // fload to decimal is recommeded for Rounding
      return Math.Min( i, 255 );
    }

    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    /// <param name="component">An R,G, or B value</param>
    /// <returns>An int (clamped at 255)</returns>
    public static int ColorComp( long component )
    {
      if (component < 0) return 0;
      return (int)Math.Min( component, 255 );
    }
    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    /// <param name="component">An R,G, or B value</param>
    /// <returns>An int (clamped at 255)</returns>
    public static int ColorComp( int component ) => ColorComp( (long)component );
    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    /// <param name="component">An R,G, or B value</param>
    /// <returns>An int (clamped at 255)</returns>
    public static int ColorComp( short component ) => ColorComp( (int)component );

    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    public static int AsColorComp( this double component ) => ColorComp( component );
    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    public static int AsColorComp( this float component ) => ColorComp( component );
    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    public static int AsColorComp( this long component ) => ColorComp( component );
    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    public static int AsColorComp( this int component ) => ColorComp( component );
    /// <summary>
    /// Round and clamp a Color Component to an int i.e. 0..255
    /// </summary>
    public static int AsColorComp( this short component ) => ColorComp( component );

    #endregion

    #region Max/Min Component Values

    /// <summary>
    /// Returns the largest R,G,B component value of a Color (Alpha not considered)
    /// </summary>
    /// <param name="color">A Color</param>
    /// <returns>An Int</returns>
    public static int MaxComp( Color color ) => XMath.Max( color.R, color.G, color.B );

    /// <summary>
    /// Returns the smallest R,G,B component value of a Color (Alpha not considered)
    /// </summary>
    /// <param name="color">A Color</param>
    /// <returns>An Int</returns>
    public static int MinComp( Color color ) => XMath.Min( color.R, color.G, color.B );

    /// <summary>
    /// Returns the largest R,G,B component value of a Color (Alpha not considered)
    /// </summary>
    public static int MaxCompOf( this Color color ) => MaxComp( color );

    /// <summary>
    /// Returns the smallest R,G,B component value of a Color (Alpha not considered)
    /// </summary>
    public static int MinCompOf( this Color color ) => MinComp( color );

    #endregion

    #region Color Type Manipulations

    /// <summary>
    /// Return a Color where the absolute Brightness level 0..1 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="brightness">Brightness level 0..1</param>
    /// <returns>A Color</returns>
    public static Color SetBrightness( this Color color, double brightness )
    {
      // sanity 
      brightness = XMath.Clip( brightness, 0d, 1d );

      HSB hsb = HSB.FromRgb( color );
      hsb.B = brightness;
      return HSB.ToRgb( hsb );
    }

    /// <summary>
    /// Return a Color where the absolute Saturation level 0..1 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="saturation">Saturation level 0..1</param>
    /// <returns>A Color</returns>
    public static Color SetSaturation( this Color color, double saturation )
    {
      // sanity 
      saturation = XMath.Clip( saturation, 0d, 1d );

      HSB hsb = HSB.FromRgb( color );
      hsb.S = saturation;
      return HSB.ToRgb( hsb );
    }

    /// <summary>
    /// Return a Color where the absolute Hue  0..&lt;360
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="hue">Hue </param>
    /// <returns>A Color</returns>
    public static Color SetHue( this Color color, double hue )
    {
      // sanity 
      hue = XMath.Clip( hue, 0d, 359.9999999d );

      HSB hsb = HSB.FromRgb( color );
      hsb.H = hue;
      return HSB.ToRgb( hsb );
    }


    /// <summary>
    /// Return a Color where the Brightness level is scaled by 0..100 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="scaling">Scaling 0.0 .. 100.0</param>
    /// <returns>A Color</returns>
    public static Color ScaleBrightness( this Color color, double scaling )
    {
      // sanity 
      scaling = XMath.Clip( scaling, 0d, 100d );

      HSB hsb = HSB.FromRgb( color );
      hsb.B = (scaling * hsb.B).Clip( min: 0d, max: 1d );
      return HSB.ToRgb( hsb );
    }

    /// <summary>
    /// Return a Color where the Saturation level is scaled by 0..100 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="scaling">Scaling 0.0 .. 100.0</param>
    /// <returns>A Color</returns>
    public static Color ScaleSaturation( this Color color, double scaling )
    {
      // sanity 
      scaling = XMath.Clip( scaling, 0d, 100d );

      HSB hsb = HSB.FromRgb( color );
      hsb.S = (scaling * hsb.S).Clip( min: 0d, max: 1d );
      return HSB.ToRgb( hsb );
    }

    /// <summary>
    /// Return a Color where the Hue deg is scaled by 0..100 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="scaling">Scaling 0.0 .. 100.0</param>
    /// <returns>A Color</returns>
    public static Color ScaleHue( this Color color, double scaling )
    {
      // sanity 
      scaling = XMath.Clip( scaling, 0d, 100d );

      HSB hsb = HSB.FromRgb( color );
      hsb.H = (scaling * hsb.H).Clip( min: 0d, max: 359.9999999d );
      return HSB.ToRgb( hsb );
    }

    /// <summary>
    /// Return a Color where the R,G,B Color Components are scaled by 0..100 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="scaling">Scaling 0.0 .. 100.0</param>
    /// <returns>A Color</returns>
    public static Color ScaleColor( this Color color, double scaling )
    {
      // sanity 
      scaling = XMath.Clip( scaling, 0d, 100d );

      Color rgb = Color.FromArgb(
          color.A,
          (scaling * color.R).AsColorComp( ),
          (scaling * color.G).AsColorComp( ),
          (scaling * color.B).AsColorComp( )
      );
      return rgb;
    }

    /// <summary>
    /// Return a Color where the Red Color Component is scaled by 0..100 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="scaling">Scaling 0.0 .. 100.0</param>
    /// <returns>A Color</returns>
    public static Color ScaleR( this Color color, double scaling )
    {
      // sanity 
      scaling = XMath.Clip( scaling, 0d, 100d );

      Color rgb = Color.FromArgb(
          color.A,
          (scaling * color.R).AsColorComp( ),
          color.G,
          color.B
      );
      return rgb;
    }
    /// <summary>
    /// Return a Color where the Green Color Component is scaled by 0..100 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="scaling">Scaling 0.0 .. 100.0</param>
    /// <returns>A Color</returns>
    public static Color ScaleG( this Color color, double scaling )
    {
      // sanity 
      scaling = XMath.Clip( scaling, 0d, 100d );

      Color rgb = Color.FromArgb(
          color.A,
          color.R,
          (scaling * color.G).AsColorComp( ),
          color.B
      );
      return rgb;
    }
    /// <summary>
    /// Return a Color where the Blue Color Component is scaled by 0..100 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="scaling">Scaling 0.0 .. 100.0</param>
    /// <returns>A Color</returns>
    public static Color ScaleB( this Color color, double scaling )
    {
      // sanity 
      scaling = XMath.Clip( scaling, 0d, 100d );

      Color rgb = Color.FromArgb(
          color.A,
          color.R,
          color.G,
          (scaling * color.B).AsColorComp( )
      );
      return rgb;
    }

    /// <summary>
    /// Return a Color where the Tranparency Color Component is scaled by 0..100 [%/100]
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="scaling">Scaling 0.0 .. 100.0</param>
    /// <returns>A Color</returns>
    public static Color ScaleTranparency( this Color color, double scaling )
    {
      // sanity 
      scaling = XMath.Clip( scaling, 0d, 100d );

      Color rgb = Color.FromArgb(
          (scaling * color.A).AsColorComp( ),
          color.R,
          color.G,
          color.B
      );
      return rgb;
    }

    /// <summary>
    /// Return a Color where the Tranparency Color Component is set [0..255] 
    ///  is changed from the target Color
    /// </summary>
    /// <param name="color">Reference Color</param>
    /// <param name="transparency">Transparency 0..255</param>
    /// <returns>A Color</returns>
    public static Color SetTranparency( this Color color, int transparency )
    {
      // sanity 
      transparency = transparency.Clip( min: 0, max: 255 );

      Color rgb = Color.FromArgb(
         transparency,
          color.R,
          color.G,
          color.B
      );
      return rgb;
    }

    #endregion
  }
}
