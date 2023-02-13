using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98
{
  /// <summary>
  /// A number of Unit Conversions and Constants
  /// </summary>
  public static class Units
  {

    private const double c_mPNm = 1852.0;
    private const double c_nmPm = 1.0 / c_mPNm;

    private const double c_kmhPkt = 1.852;
    private const double c_ktPkmh = 1.0 / c_kmhPkt;

    private const double c_mPFt = 0.3048;
    private const double c_ftPm = 1.0 / c_mPFt;

    private const double c_lbsPkg = 2.204622621848776;
    private const double c_kgPlbs = 1.0 / c_lbsPkg;

    private const double c_degF = 9f / 5f;

    /// <summary>
    /// Gravitational acceleration [m/sec2]
    /// </summary>
    public const double Newton = 9.80665;
    /// <summary>
    /// Speed of Light in Vacuum (c) [m/s]
    /// </summary>
    public const double LSpeed = 299_792_458.0;

    /// <summary>
    /// Kilograms from Pounds
    /// </summary>
    /// <param name="lbs">Pound</param>
    /// <returns>Kilograms</returns>
    public static double Kg_From_Lbs( double lbs ) => (lbs * c_kgPlbs);
    /// <summary>
    /// Kilograms from Pounds
    /// </summary>
    /// <param name="kg">Kilograms</param>
    /// <returns>Pound</returns>
    public static double Lbs_From_Kg( double kg ) => (kg * c_lbsPkg);


    /// <summary>
    /// Nautical Miles from Meters
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <returns>Nautical Miles</returns>
    public static double Nm_From_M( double meter ) => (meter * c_nmPm);
    /// <summary>
    /// Meters from Nautical Miles
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <returns>Meter</returns>
    public static double M_From_Nm( double nm ) => (nm * c_mPNm);


    /// <summary>
    /// Nautical Miles from Kilometers
    /// </summary>
    /// <param name="km">Kilometer</param>
    /// <returns>Nautical Miles</returns>
    public static double Nm_From_Km( double km ) => Nm_From_M( km * 1000.0 );
    /// <summary>
    /// Kilometers from Nautical Miles
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <returns>Kilometer</returns>
    public static double Km_From_Nm( double nm ) => M_From_Nm( nm ) / 1000.0;


    /// <summary>
    /// Meters from Foot
    /// </summary>
    /// <param name="ft">Foot</param>
    /// <returns>Meter</returns>
    public static double M_From_Ft( double ft ) => (ft * c_mPFt);
    /// <summary>
    /// Foot from Meters
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <returns>Foot</returns>
    public static double Ft_From_M( double meter ) => (meter * c_ftPm);


    /// <summary>
    /// Returns Meter/Sec from Foot/Min
    /// </summary>
    /// <param name="fpm">A foot/minute value</param>
    /// <returns>The meter/second value</returns>
    public static double Mps_From_Ftpm( double fpm ) => (fpm * c_mPFt / 60.0);
    /// <summary>
    /// Returns Foot/Min from Meter/Sec
    /// </summary>
    /// <param name="mps">A foot/minute value</param>
    /// <returns>The foot/Min value</returns>
    public static double Ftpm_From_Mps( double mps ) => (mps * 60.0 * c_ftPm);


    /// <summary>
    /// Returns m/s from kt
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Meter / second</returns>
    public static double Mps_From_Kt( double kt ) => (kt * c_mPNm / 3600f);
    /// <summary>
    /// Returns kts from m/sec
    /// </summary>
    /// <param name="mps">Meter per sec value</param>
    /// <returns>Converted Knots value</returns>
    public static double Kt_From_Mps( double mps ) => (mps * 3600f * c_nmPm);


    /// <summary>
    /// Returns kmh from kt
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static double Kmh_From_Kt( double kt ) => (kt * c_kmhPkt);
    /// <summary>
    /// Returns kt from km/h
    /// </summary>
    /// <param name="kmh">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static double Kt_From_Kmh( double kmh ) => (kmh * c_ktPkmh);

    /// <summary>
    /// Returns feet per Minute from Feet per Second
    /// </summary>
    /// <param name="fps">Feet per Second</param>
    /// <returns>Feet per Minute</returns>
    public static double Fpm_From_Fps( double fps ) => fps * 60.0;
    /// <summary>
    /// Returns feet per Second from Feet per Minute
    /// </summary>
    /// <param name="fpm">Feet per Minute</param>
    /// <returns>Feet per Second</returns>
    public static double Fps_From_Fpm( double fpm ) => fpm / 60.0;

    /// <summary>
    /// Returns km/h from m/sec
    /// </summary>
    /// <param name="kmh">m/sec</param>
    /// <returns>Kilometer per hour</returns>
    public static double Kmh_From_Mps( double kmh ) => kmh * 3600.0 / 1000.0;
    /// <summary>
    /// Returns m/sec from km/h
    /// </summary>
    /// <param name="kmh">km/h</param>
    /// <returns>Meter per sec</returns>
    public static double Mps_From_Kmh( double kmh ) => kmh * 1000.0 / 3600.0;


    /// <summary>
    /// Returns DegF from DegC ((DEG°C * 9/5) + 32)
    /// </summary>
    /// <param name="degC">Temp deg C</param>
    /// <returns>Temp in deg F</returns>
    public static double DegF_From_DegC( double degC ) => ((degC * c_degF) + 32.0f);

    /// <summary>
    /// Returns DegC from DegF  ((DegF-32) / (9/5))
    /// </summary>
    /// <param name="degF">Temp deg C</param>
    /// <returns>Temp in deg F</returns>
    public static double DegC_From_DegF( double degF ) => ((degF - 32.0) / c_degF);



  }
}
