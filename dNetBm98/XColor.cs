﻿using System;
using System.Drawing;

namespace dNetBm98
{
  /// <summary>
  /// Some usefull extensions
  /// </summary>
  public static class XColor
  {
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
  }
}
