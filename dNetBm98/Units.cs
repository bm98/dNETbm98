using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace dNetBm98
{
  /// <summary>
  /// A number of Unit Conversions and Constants
  ///  Syntax of the function is:
  ///    "newValue = NEWUNIT_From_OLDUNIT(oldValue)"
  ///   if extending value types
  ///    "newValue = AsNEWUNIT_From_OLDUNIT(this)"
  /// </summary>
  public static class Units
  {
    private const double c_mPNm = 1852.0;
    private const double c_nmPm = 1.0 / c_mPNm;

    private const double c_kmhPkt = 1.852;
    private const double c_ktPkmh = 1.0 / c_kmhPkt;

    private const double c_mPFt = 0.3048;
    private const double c_ftPm = 1.0 / c_mPFt;

    private const double c_m2PFt2 = c_mPFt * c_mPFt; // 0.09290304
    private const double c_ft2Pm2 = 1.0 / c_m2PFt2;

    private const double c_lbsPkg = 2.204622621848776;
    private const double c_kgPlbs = 1.0 / c_lbsPkg;

    private const double c_degF = 9f / 5f;

    private const double K0deg = 273.15; // K at 0°C 

    /// <summary>
    /// Gravitational acceleration [m/sec2]
    /// </summary>
    public const double Newton = 9.80665;
    /// <summary>
    /// Speed of Light in Vacuum (c) [m/s]
    /// </summary>
    public const double LSpeed = 299_792_458.0;


    //*** Kg_From_Lbs
    /// <summary>
    /// Kilograms from Pounds
    /// </summary>
    /// <param name="lbs">Pound</param>
    /// <returns>Kilograms</returns>
    public static double Kg_From_Lbs( double lbs ) => (lbs * c_kgPlbs);
    /// <summary>
    /// Kilograms from Pounds
    /// </summary>
    /// <param name="lbs">Pound</param>
    /// <returns>Kilograms</returns>
    public static double AsKg_From_Lbs( this double lbs ) => Kg_From_Lbs( lbs );
    /// <summary>
    /// Kilograms from Pounds
    /// </summary>
    /// <param name="lbs">Pound</param>
    /// <returns>Kilograms</returns>
    public static float AsKg_From_Lbs( this float lbs ) => (float)Kg_From_Lbs( lbs );


    //*** Lbs_From_Kg
    /// <summary>
    /// Kilograms from Pounds
    /// </summary>
    /// <param name="kg">Kilograms</param>
    /// <returns>Pound</returns>
    public static double Lbs_From_Kg( double kg ) => (kg * c_lbsPkg);
    /// <summary>
    /// Kilograms from Pounds
    /// </summary>
    /// <param name="kg">Kilograms</param>
    /// <returns>Pound</returns>
    public static double AsLbs_From_Kg( this double kg ) => Lbs_From_Kg( kg );
    /// <summary>
    /// Kilograms from Pounds
    /// </summary>
    /// <param name="kg">Kilograms</param>
    /// <returns>Pound</returns>
    public static float AsLbs_From_Kg( this float kg ) => (float)Lbs_From_Kg( kg );

    /// <summary>
    /// Kilograms from Pounds rounding to quantity
    /// </summary>
    /// <param name="kg">Kilograms</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Pound</returns>
    public static int Lbs_From_Kg( double kg, int quant ) => Lbs_From_Kg( kg ).AsRoundInt( quant );
    /// <summary>
    /// Kilograms from Pounds rounding to quantity
    /// </summary>
    /// <param name="kg">Kilograms</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Pound</returns>
    public static int AsLbs_From_Kg( this double kg, int quant ) => Lbs_From_Kg( kg, quant );


    //*** Nm_From_M
    /// <summary>
    /// Nautical Miles from Meters
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <returns>Nautical Miles</returns>
    public static double Nm_From_M( double meter ) => (meter * c_nmPm);
    /// <summary>
    /// Nautical Miles from Meters
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <returns>Nautical Miles</returns>
    public static double AsNm_From_M( this double meter ) => Nm_From_M( meter );
    /// <summary>
    /// Nautical Miles from Meters
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <returns>Nautical Miles</returns>
    public static float AsNm_From_M( this float meter ) => (float)Nm_From_M( meter );

    /// <summary>
    /// Nautical Miles from Meters rounding to quantity
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Nautical Miles</returns>
    public static int Nm_From_M( double meter, int quant ) => Nm_From_M( meter ).AsRoundInt( quant );
    /// <summary>
    /// Nautical Miles from Meters rounding to quantity
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Nautical Miles</returns>
    public static int AsNm_From_M( this double meter, int quant ) => Nm_From_M( meter, quant );


    //*** M_From_Nm
    /// <summary>
    /// Meters from Nautical Miles
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <returns>Meter</returns>
    public static double M_From_Nm( double nm ) => (nm * c_mPNm);
    /// <summary>
    /// Meters from Nautical Miles
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <returns>Meter</returns>
    public static double AsM_From_Nm( this double nm ) => M_From_Nm( nm );
    /// <summary>
    /// Meters from Nautical Miles
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <returns>Meter</returns>
    public static float AsM_From_Nm( this float nm ) => (float)M_From_Nm( nm );

    /// <summary>
    /// Meters from Nautical Miles rounding to quantity
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Meter</returns>
    public static int M_From_Nm( double nm, int quant ) => M_From_Nm( nm ).AsRoundInt( quant );
    /// <summary>
    /// Meters from Nautical Miles rounding to quantity
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Meter</returns>
    public static int AsM_From_Nm( this double nm, int quant ) => M_From_Nm( nm, quant );


    //*** Nm_From_Km
    /// <summary>
    /// Nautical Miles from Kilometers
    /// </summary>
    /// <param name="km">Kilometer</param>
    /// <returns>Nautical Miles</returns>
    public static double Nm_From_Km( double km ) => Nm_From_M( km * 1000.0 );
    /// <summary>
    /// Nautical Miles from Kilometers
    /// </summary>
    /// <param name="km">Kilometer</param>
    /// <returns>Nautical Miles</returns>
    public static double AsNm_From_Km( this double km ) => Nm_From_Km( km );
    /// <summary>
    /// Nautical Miles from Kilometers
    /// </summary>
    /// <param name="km">Kilometer</param>
    /// <returns>Nautical Miles</returns>
    public static float AsNm_From_Km( this float km ) => (float)Nm_From_Km( km );

    /// <summary>
    /// Nautical Miles from Kilometers rounding to quantity
    /// </summary>
    /// <param name="km">Kilometer</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Nautical Miles</returns>
    public static int Nm_From_Km( double km, int quant ) => Nm_From_Km( km ).AsRoundInt( quant );
    /// <summary>
    /// Nautical Miles from Kilometers rounding to quantity
    /// </summary>
    /// <param name="km">Kilometer</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Nautical Miles</returns>
    public static int AsNm_From_Km( this double km, int quant ) => Nm_From_Km( km, quant );


    //*** Km_From_Nm
    /// <summary>
    /// Kilometers from Nautical Miles
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <returns>Kilometer</returns>
    public static double Km_From_Nm( double nm ) => (M_From_Nm( nm ) / 1000.0);
    /// <summary>
    /// Kilometers from Nautical Miles
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <returns>Kilometer</returns>
    public static double AsKm_From_Nm( this double nm ) => Km_From_Nm( nm );
    /// <summary>
    /// Kilometers from Nautical Miles
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <returns>Kilometer</returns>
    public static float AsKm_From_Nm( this float nm ) => (float)Km_From_Nm( nm );

    /// <summary>
    /// Kilometers from Nautical Miles rounding to quantity
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Kilometer</returns>
    public static int Km_From_Nm( double nm, int quant ) => Km_From_Nm( nm ).AsRoundInt( quant );
    /// <summary>
    /// Kilometers from Nautical Miles rounding to quantity
    /// </summary>
    /// <param name="nm">Nautical Miles</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Kilometer</returns>
    public static int AsKm_From_Nm( this double nm, int quant ) => Km_From_Nm( nm, quant );


    //*** M_From_Ft
    /// <summary>
    /// Meter from Foot
    /// </summary>
    /// <param name="ft">Foot</param>
    /// <returns>Meter</returns>
    public static double M_From_Ft( double ft ) => (ft * c_mPFt);
    /// <summary>
    /// Meter from Foot
    /// </summary>
    /// <param name="ft">Foot</param>
    /// <returns>Meter</returns>
    public static double AsM_From_Ft( this double ft ) => M_From_Ft( ft );
    /// <summary>
    /// Meter from Foot
    /// </summary>
    /// <param name="ft">Foot</param>
    /// <returns>Meter</returns>
    public static float AsM_From_Ft( this float ft ) => (float)M_From_Ft( ft );

    /// <summary>
    /// Meter from Foot rounding to the given quant
    /// </summary>
    /// <param name="ft">Foot</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Meter</returns>
    public static int M_From_Ft( double ft, int quant ) => M_From_Ft( ft ).AsRoundInt( quant );
    /// <summary>
    /// Meter from Foot rounding to the given quant
    /// </summary>
    /// <param name="ft">Foot</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Meter</returns>
    public static int AsM_From_Ft( this int ft, int quant ) => M_From_Ft( ft, quant );

    //*** Ft_From_M
    /// <summary>
    /// Foot from Meters
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <returns>Foot</returns>
    public static double Ft_From_M( double meter ) => (meter * c_ftPm);
    /// <summary>
    /// Foot from Meters
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <returns>Foot</returns>
    public static double AsFt_From_M( this double meter ) => Ft_From_M( meter );
    /// <summary>
    /// Foot from Meters
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <returns>Foot</returns>
    public static float AsFt_From_M( this float meter ) => (float)Ft_From_M( meter );

    /// <summary>
    /// Foot from Meters rounding to the given quantity
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Foot</returns>
    public static int Ft_From_M( double meter, int quant ) => Ft_From_M( meter ).AsRoundInt( quant );
    /// <summary>
    /// Foot from Meters rounding to the given quantity
    /// </summary>
    /// <param name="meter">Meter</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Foot</returns>
    public static int AsFt_From_M( this int meter, int quant ) => Ft_From_M( meter, quant );


    //*** M2_From_Ft2
    /// <summary>
    /// Meter^2 from Foot^2
    /// </summary>
    /// <param name="ft2">Foot ^2</param>
    /// <returns>Meter</returns>
    public static double M2_From_Ft2( double ft2 ) => (ft2 * c_m2PFt2);
    /// <summary>
    /// Meter^2 from Foot^2
    /// </summary>
    /// <param name="ft2">Foot ^2</param>
    /// <returns>Meter</returns>
    public static double AsM2_From_Ft2( this double ft2 ) => M2_From_Ft2( ft2 );
    /// <summary>
    /// Meter^2 from Foot^2
    /// </summary>
    /// <param name="ft2">Foot ^2</param>
    /// <returns>Meter</returns>
    public static float AsM2_From_Ft2( this float ft2 ) => (float)M2_From_Ft2( ft2 );


    //*** Ft2_From_M2
    /// <summary>
    /// Foot^2 from Meters^2
    /// </summary>
    /// <param name="meter2">Meter ^2</param>
    /// <returns>Foot ^2</returns>
    public static double Ft2_From_M2( double meter2 ) => (meter2 * c_ft2Pm2);
    /// <summary>
    /// Foot^2 from Meters^2
    /// </summary>
    /// <param name="meter2">Meter ^2</param>
    /// <returns>Foot ^2</returns>
    public static double AsFt2_From_M2( this double meter2 ) => Ft2_From_M2( meter2 );
    /// <summary>
    /// Foot^2 from Meters^2
    /// </summary>
    /// <param name="meter2">Meter ^2</param>
    /// <returns>Foot ^2</returns>
    public static float AsFt2_From_M2( this float meter2 ) => (float)Ft2_From_M2( meter2 );



    //*** Mps_From_Ftpm
    /// <summary>
    /// Returns Meter/Sec from Foot/Min
    /// </summary>
    /// <param name="fpm">A foot/minute value</param>
    /// <returns>The meter/second value</returns>
    public static double Mps_From_Ftpm( double fpm ) => (fpm * c_mPFt / 60.0);
    /// <summary>
    /// Returns Meter/Sec from Foot/Min
    /// </summary>
    /// <param name="fpm">A foot/minute value</param>
    /// <returns>The meter/second value</returns>
    public static double AsMps_From_Ftpm( this double fpm ) => Mps_From_Ftpm( fpm );
    /// <summary>
    /// Returns Meter/Sec from Foot/Min
    /// </summary>
    /// <param name="fpm">A foot/minute value</param>
    /// <returns>The meter/second value</returns>
    public static float AsMps_From_Ftpm( this float fpm ) => (float)Mps_From_Ftpm( fpm );

    /// <summary>
    /// Returns Meter/Sec from Foot/Min rounding to quantity
    /// </summary>
    /// <param name="fpm">A foot/minute value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>The meter/second value</returns>
    public static int Mps_From_Ftpm( double fpm, int quant ) => Mps_From_Ftpm( fpm ).AsRoundInt( quant );
    /// <summary>
    /// Returns Meter/Sec from Foot/Min rounding to quantity
    /// </summary>
    /// <param name="fpm">A foot/minute value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>The meter/second value</returns>
    public static int AsMps_From_Ftpm( this double fpm, int quant ) => Mps_From_Ftpm( fpm, quant );


    //*** Ftpm_From_Mps
    /// <summary>
    /// Returns Foot/Min from Meter/Sec
    /// </summary>
    /// <param name="mps">A foot/minute value</param>
    /// <returns>The foot/Min value</returns>
    public static double Ftpm_From_Mps( double mps ) => (mps * 60.0 * c_ftPm);
    /// <summary>
    /// Returns Foot/Min from Meter/Sec
    /// </summary>
    /// <param name="mps">A foot/minute value</param>
    /// <returns>The foot/Min value</returns>
    public static double AsFtpm_From_Mps( this double mps ) => Ftpm_From_Mps( mps );
    /// <summary>
    /// Returns Foot/Min from Meter/Sec
    /// </summary>
    /// <param name="mps">A foot/minute value</param>
    /// <returns>The foot/Min value</returns>
    public static float AsFtpm_From_Mps( this float mps ) => (float)Ftpm_From_Mps( mps );

    /// <summary>
    /// Returns Foot/Min from Meter/Sec rounding to quantity
    /// </summary>
    /// <param name="mps">A foot/minute value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>The foot/Min value</returns>
    public static int Ftpm_From_Mps( double mps, int quant ) => Ftpm_From_Mps( mps ).AsRoundInt( quant );
    /// <summary>
    /// Returns Foot/Min from Meter/Sec rounding to quantity
    /// </summary>
    /// <param name="mps">A foot/minute value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>The foot/Min value</returns>
    public static int AsFtpm_From_Mps( this double mps, int quant ) => Ftpm_From_Mps( mps, quant );


    //*** Mps_From_Kt
    /// <summary>
    /// Returns m/s from kt
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Meter / second</returns>
    public static double Mps_From_Kt( double kt ) => (kt * c_mPNm / 3600.0);
    /// <summary>
    /// Returns m/s from kt
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Meter / second</returns>
    public static double AsMps_From_Kt( this double kt ) => Mps_From_Kt( kt );
    /// <summary>
    /// Returns m/s from kt
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Meter / second</returns>
    public static float AsMps_From_Kt( this float kt ) => (float)Mps_From_Kt( kt );

    /// <summary>
    /// Returns m/s from kt rounding to quantity
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Meter / second</returns>
    public static int Mps_From_Kt( double kt, int quant ) => Mps_From_Kt( kt ).AsRoundInt( quant );
    /// <summary>
    /// Returns m/s from kt rounding to quantity
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Meter / second</returns>
    public static int AsMps_From_Kt( this double kt, int quant ) => Mps_From_Kt( kt, quant );


    //*** Kt_From_Mps
    /// <summary>
    /// Returns kts from m/sec
    /// </summary>
    /// <param name="mps">Meter per sec value</param>
    /// <returns>Converted Knots value</returns>
    public static double Kt_From_Mps( double mps ) => (mps * 3600.0 * c_nmPm);
    /// <summary>
    /// Returns kts from m/sec
    /// </summary>
    /// <param name="mps">Meter per sec value</param>
    /// <returns>Converted Knots value</returns>
    public static double AsKt_From_Mps( this double mps ) => Kt_From_Mps( mps );
    /// <summary>
    /// Returns kts from m/sec
    /// </summary>
    /// <param name="mps">Meter per sec value</param>
    /// <returns>Converted Knots value</returns>
    public static float AsKt_From_Mps( this float mps ) => (float)Kt_From_Mps( mps );

    /// <summary>
    /// Returns kts from m/sec rounding to quantity
    /// </summary>
    /// <param name="mps">Meter per sec value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Converted Knots value</returns>
    public static int Kt_From_Mps( double mps, int quant ) => Kt_From_Mps( mps ).AsRoundInt( quant );
    /// <summary>
    /// Returns kts from m/sec rounding to quantity
    /// </summary>
    /// <param name="mps">Meter per sec value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Converted Knots value</returns>
    public static int AsKt_From_Mps( this double mps, int quant ) => Kt_From_Mps( mps, quant );


    //*** Kmh_From_Kt
    /// <summary>
    /// Returns km/h from kt
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static double Kmh_From_Kt( double kt ) => (kt * c_kmhPkt);
    /// <summary>
    /// Returns km/h from kt
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static double AsKmh_From_Kt( this double kt ) => Kmh_From_Kt( kt );
    /// <summary>
    /// Returns km/h from kt
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static float AsKmh_From_Kt( this float kt ) => (float)Kmh_From_Kt( kt );
    /// <summary>
    /// Returns km/h from kt (rounds the conversion to an int)
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static int AsKmh_From_Kt( this int kt ) => (int)Math.Round( Kmh_From_Kt( kt ) );

    /// <summary>
    /// Returns km/h from kt rounding to quantity
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Kilometer per hour</returns>
    public static int Kmh_From_Kt( double kt, int quant ) => Kmh_From_Kt( kt ).AsRoundInt( quant );
    /// <summary>
    /// Returns km/h from kt rounding to quantity
    /// </summary>
    /// <param name="kt">Knots value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Kilometer per hour</returns>
    public static int AsKmh_From_Kt( this double kt, int quant ) => Kmh_From_Kt( kt, quant );


    //*** Kt_From_Kmh
    /// <summary>
    /// Returns kt from km/h
    /// </summary>
    /// <param name="kmh">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static double Kt_From_Kmh( double kmh ) => (kmh * c_ktPkmh);
    /// <summary>
    /// Returns kt from km/h
    /// </summary>
    /// <param name="kmh">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static double AsKt_From_Kmh( this double kmh ) => Kt_From_Kmh( kmh );
    /// <summary>
    /// Returns kt from km/h
    /// </summary>
    /// <param name="kmh">Knots value</param>
    /// <returns>Kilometer per hour</returns>
    public static float AsKt_From_Kmh( this float kmh ) => (float)Kt_From_Kmh( kmh );

    /// <summary>
    /// Returns kt from km/h rounding to quantity
    /// </summary>
    /// <param name="kmh">Knots value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Kilometer per hour</returns>
    public static int Kt_From_Kmh( double kmh, int quant ) => Kt_From_Kmh( kmh ).AsRoundInt( quant );
    /// <summary>
    /// Returns kt from km/h rounding to quantity
    /// </summary>
    /// <param name="kmh">Knots value</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Kilometer per hour</returns>
    public static int AsKt_From_Kmh( this double kmh, int quant ) => Kt_From_Kmh( kmh, quant );


    //*** Fpm_From_Fps
    /// <summary>
    /// Returns feet per Minute from Feet per Second
    /// </summary>
    /// <param name="fps">Feet per Second</param>
    /// <returns>Feet per Minute</returns>
    public static double Fpm_From_Fps( double fps ) => (fps * 60.0);
    /// <summary>
    /// Returns feet per Minute from Feet per Second
    /// </summary>
    /// <param name="fps">Feet per Second</param>
    /// <returns>Feet per Minute</returns>
    public static double AsFpm_From_Fps( this double fps ) => Fpm_From_Fps( fps );
    /// <summary>
    /// Returns feet per Minute from Feet per Second
    /// </summary>
    /// <param name="fps">Feet per Second</param>
    /// <returns>Feet per Minute</returns>
    public static float AsFpm_From_Fps( this float fps ) => (float)Fpm_From_Fps( fps );

    /// <summary>
    /// Returns feet per Minute from Feet per Second rounding to quantity
    /// </summary>
    /// <param name="fps">Feet per Second</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Feet per Minute</returns>
    public static int Fpm_From_Fps( double fps, int quant ) => Fpm_From_Fps( fps ).AsRoundInt( quant );
    /// <summary>
    /// Returns feet per Minute from Feet per Second rounding to quantity
    /// </summary>
    /// <param name="fps">Feet per Second</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Feet per Minute</returns>
    public static int AsFpm_From_Fps( this double fps, int quant ) => Fpm_From_Fps( fps, quant );


    //*** Fps_From_Fpm
    /// <summary>
    /// Returns feet per Second from Feet per Minute
    /// </summary>
    /// <param name="fpm">Feet per Minute</param>
    /// <returns>Feet per Second</returns>
    public static double Fps_From_Fpm( double fpm ) => fpm / 60.0;
    /// <summary>
    /// Returns feet per Second from Feet per Minute
    /// </summary>
    /// <param name="fpm">Feet per Minute</param>
    /// <returns>Feet per Second</returns>
    public static double AsFps_From_Fpm( this double fpm ) => Fps_From_Fpm( fpm );
    /// <summary>
    /// Returns feet per Second from Feet per Minute
    /// </summary>
    /// <param name="fpm">Feet per Minute</param>
    /// <returns>Feet per Second</returns>
    public static float AsFps_From_Fpm( this float fpm ) => (float)Fps_From_Fpm( fpm );

    /// <summary>
    /// Returns feet per Second from Feet per Minute rounding to quantity
    /// </summary>
    /// <param name="fpm">Feet per Minute</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Feet per Second</returns>
    public static int Fps_From_Fpm( double fpm, int quant ) => Fps_From_Fpm( fpm ).AsRoundInt( quant );
    /// <summary>
    /// Returns feet per Second from Feet per Minute rounding to quantity
    /// </summary>
    /// <param name="fpm">Feet per Minute</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Feet per Second</returns>
    public static int AsFps_From_Fpm( this double fpm, int quant ) => Fps_From_Fpm( fpm, quant );


    //*** Kmh_From_Mps
    /// <summary>
    /// Returns km/h from m/sec
    /// </summary>
    /// <param name="kmh">m/sec</param>
    /// <returns>Kilometer per hour</returns>
    public static double Kmh_From_Mps( double kmh ) => kmh * 3600.0 / 1000.0;
    /// <summary>
    /// Returns km/h from m/sec
    /// </summary>
    /// <param name="kmh">m/sec</param>
    /// <returns>Kilometer per hour</returns>
    public static double AsKmh_From_Mps( this double kmh ) => Kmh_From_Mps( kmh );
    /// <summary>
    /// Returns km/h from m/sec
    /// </summary>
    /// <param name="kmh">m/sec</param>
    /// <returns>Kilometer per hour</returns>
    public static float AsKmh_From_Mps( this float kmh ) => (float)Kmh_From_Mps( kmh );

    /// <summary>
    /// Returns km/h from m/sec rounding to quantity
    /// </summary>
    /// <param name="kmh">m/sec</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Kilometer per hour</returns>
    public static int Kmh_From_Mps( double kmh, int quant ) => Kmh_From_Mps( kmh ).AsRoundInt( quant );
    /// <summary>
    /// Returns km/h from m/sec rounding to quantity
    /// </summary>
    /// <param name="kmh">m/sec</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Kilometer per hour</returns>
    public static int AsKmh_From_Mps( this double kmh, int quant ) => Kmh_From_Mps( kmh, quant );


    //*** Mps_From_Kmh
    /// <summary>
    /// Returns m/sec from km/h
    /// </summary>
    /// <param name="kmh">km/h</param>
    /// <returns>Meter per sec</returns>
    public static double Mps_From_Kmh( double kmh ) => kmh * 1000.0 / 3600.0;
    /// <summary>
    /// Returns m/sec from km/h
    /// </summary>
    /// <param name="kmh">km/h</param>
    /// <returns>Meter per sec</returns>
    public static double AsMps_From_Kmh( this double kmh ) => Mps_From_Kmh( kmh );
    /// <summary>
    /// Returns m/sec from km/h
    /// </summary>
    /// <param name="kmh">km/h</param>
    /// <returns>Meter per sec</returns>
    public static float AsMps_From_Kmh( this float kmh ) => (float)Mps_From_Kmh( kmh );

    /// <summary>
    /// Returns m/sec from km/h rounding to quantity
    /// </summary>
    /// <param name="kmh">km/h</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Meter per sec</returns>
    public static int Mps_From_Kmh( double kmh, int quant ) => Mps_From_Kmh( kmh ).AsRoundInt( quant );
    /// <summary>
    /// Returns m/sec from km/h rounding to quantity
    /// </summary>
    /// <param name="kmh">km/h</param>
    /// <param name="quant">Min quantity (for rounding)</param>
    /// <returns>Meter per sec</returns>
    public static int AsMps_From_Kmh( this double kmh, int quant ) => Mps_From_Kmh( kmh, quant );


    //*** DegC_From_K
    /// <summary>
    /// Returns DegC from K
    /// </summary>
    /// <param name="degK">Temp K</param>
    /// <returns>Temp in deg C</returns>
    public static double DegC_From_K( double degK ) => degK - K0deg;
    /// <summary>
    /// Returns DegC from K
    /// </summary>
    /// <param name="degK">Temp K</param>
    /// <returns>Temp in deg C</returns>
    public static double AsDegC_From_K( this double degK ) => DegC_From_K( degK );
    /// <summary>
    /// Returns DegC from K
    /// </summary>
    /// <param name="degK">Temp K</param>
    /// <returns>Temp in deg C</returns>
    public static float AsDegC_From_K( this float degK ) => (float)DegC_From_K( degK );

    //*** DegF_From_DegC
    /// <summary>
    /// Returns DegF from DegC ((DEG°C * 9/5) + 32)
    /// </summary>
    /// <param name="degC">Temp deg C</param>
    /// <returns>Temp in deg F</returns>
    public static double DegF_From_DegC( double degC ) => (degC * c_degF) + 32.0;
    /// <summary>
    /// Returns DegF from DegC ((DEG°C * 9/5) + 32)
    /// </summary>
    /// <param name="degC">Temp deg C</param>
    /// <returns>Temp in deg F</returns>
    public static double AsDegF_From_DegC( this double degC ) => DegF_From_DegC( degC );
    /// <summary>
    /// Returns DegF from DegC ((DEG°C * 9/5) + 32)
    /// </summary>
    /// <param name="degC">Temp deg C</param>
    /// <returns>Temp in deg F</returns>
    public static float AsDegF_From_DegC( this float degC ) => (float)DegF_From_DegC( degC );


    //*** DegC_From_DegF
    /// <summary>
    /// Returns DegC from DegF  ((DegF-32) / (9/5))
    /// </summary>
    /// <param name="degF">Temp deg C</param>
    /// <returns>Temp in deg F</returns>
    public static double DegC_From_DegF( double degF ) => ((degF - 32.0) / c_degF);
    /// <summary>
    /// Returns DegC from DegF  ((DegF-32) / (9/5))
    /// </summary>
    /// <param name="degF">Temp deg C</param>
    /// <returns>Temp in deg F</returns>
    public static double AsDegC_From_DegF( this double degF ) => DegC_From_DegF( degF );
    /// <summary>
    /// Returns DegC from DegF  ((DegF-32) / (9/5))
    /// </summary>
    /// <param name="degF">Temp deg C</param>
    /// <returns>Temp in deg F</returns>
    public static float AsDegC_From_DegF( this float degF ) => (float)DegC_From_DegF( degF );

    // ** Pressure Altitude
    /// <summary>
    /// Pressure Altitude Formula from NOAA
    /// https://en.wikipedia.org/wiki/Pressure_altitude
    /// </summary>
    /// <param name="pressure_pa">Isobare pressure Pa</param>
    /// <returns>An altitude in m</returns>
    public static int PressureAltitude_m( double pressure_pa )
    {
      return (int)M_From_Ft( PressureAltitude_ft( pressure_pa ) );
    }

    /// <summary>
    /// Pressure Altitude Formula from NOAA
    /// https://en.wikipedia.org/wiki/Pressure_altitude
    /// </summary>
    /// <param name="pressure_pa">Isobare pressure Pa</param>
    /// <returns>An altitude in ft</returns>
    public static int PressureAltitude_ft( double pressure_pa )
    {
      // Resulting Alt in ft, pressure in mBar; hence div 100.0
      return (int)(145366.45 * (1.0 - Math.Pow( (pressure_pa / 100.0) / 1013.25, 0.190284 )));
    }

    #region TAS, CAS conversions

    /*
     Credit: http://walter.bislins.ch/fsim/index.asp?page=Fluggeschwindigkeiten

     */
    #region Altitude Atm Model 

    // Const helper for the conversions below
    // instantiate the class with a specific altitude [ft]
    private class SConst
    {
      // http://walter.bislins.ch/blog/index.asp?page=Javascript%3A+Umrechnen+von+Fluggeschwindigkeiten
      // Calculations are using SI units !!!

      public readonly double kappa = 1.4; //1.4 = Adiabatenexponent (kappa) für Luft
      public readonly double g = 9.80665;
      public readonly double RS = 287.058;
      public readonly double R = 8.31446;
      public readonly double M = 28.9644;
      public readonly double T0 = 288.15;
      public readonly double p0 = 101_325;
      public readonly double rho0 = 1.225;
      public readonly double a0 = 340.294;

      public double hRef = 0;
      public double TRef = 0;
      public double pRef = 0;
      public double rhoRef = 0;
      public double alpha = 0;

      public double h_m = 0;

      // air level data of standard atmosphere model
      private readonly double[] hLimitTab_m = new double[] { 11000, 20000, 32000, 47000, 51000, 71000, 84852, 0 };
      private readonly double[] hRefTab_m = new double[] { 0, 11000, 20000, 32000, 47000, 51000, 71000, double.NaN };
      private readonly double[] alphaTab = new double[] { -0.0065, 0, 0.001, 0.0028, 0, -0.0028, -0.002, double.NaN };
      private readonly double[] TRefTab_K = new double[] { 288.15, 216.65, 216.65, 228.65, 270.65, 270.65, 214.65, double.NaN };
      private readonly double[] rhoRefTab = new double[] { 1.225, 0.363918, 0.0880348, 0.013225, 0.00142753, 0.000861605, 0.000064211, double.NaN };
      private readonly double[] pRefTab_Pa = new double[] { 101325, 22632.1, 5474.89, 868.019, 110.906, 66.9389, 3.95642, double.NaN };

      /// <summary>
      /// cTor: Init values for an altitude
      /// </summary>
      /// <param name="alt_ft">Set Altitude in ft MSL</param>
      public SConst( double alt_ft )
      {
        h_m = M_From_Ft( alt_ft ); // using m internally

        // select Baro Height model data
        int hIx = hLimitTab_m.Length - 1;
        for (var i = 0; i < hLimitTab_m.Length; i++) {
          if (h_m <= hLimitTab_m[i]) {
            hIx = i;
            break;
          }
        }

        // set consts for given h_m
        hRef = hRefTab_m[hIx];
        alpha = alphaTab[hIx];
        TRef = TRefTab_K[hIx];
        rhoRef = rhoRefTab[hIx];
        pRef = pRefTab_Pa[hIx];
      }

      /// <summary>
      /// Temp of Altitude (std model) [K]
      /// </summary>
      public double TempOf => TRef + alpha * (h_m - hRef);
      /// <summary>
      /// Pressure of Altitude (std model) [Pa]
      /// </summary>
      public double PressureOf {
        get {
          if (alpha == 0) {
            // isoterm
            var hs = RS * TRef / g;
            var p = pRef * Math.Exp( -(h_m - hRef) / hs );
            return p;
          }
          else {
            var beta = g / RS / alpha;
            var p = pRef * Math.Pow( 1 + alpha * (h_m - hRef) / TRef, -beta );
            return p;
          }
        }
      }
      /// <summary>
      /// Air Density of Altitude (std model) [kg/m3]
      /// </summary>
      public double DensityOf {
        get {
          if (alpha == 0) {
            // isoterm
            var hs = RS * TRef / g;
            var r = rhoRef * Math.Exp( -(h_m - hRef) / hs );
            return r;
          }
          else {
            var beta = g / RS / alpha;
            var r = rhoRef * Math.Pow( 1 + alpha * (h_m - hRef) / TRef, -beta - 1 );
            return r;
          }
        }
      }
      /// <summary>
      /// Speed of Sound of Altitude (std model) [m/s]
      /// </summary>
      public double Aof {
        get {
          return Math.Sqrt( kappa * RS * TempOf );
        }
      }

    }

    #endregion


    /// <summary>
    /// Returns CAS (Calibrated Airspeed) from TAS (True Airspeed) in [kt]
    /// </summary>
    /// <param name="tas_kt">TAS [kt]</param>
    /// <param name="alt_ft">Altitude [ft]</param>
    /// <returns>CAS [kt]</returns>
    public static double CAS_From_TAS( double tas_kt, double alt_ft )
    {
      //http://walter.bislins.ch/blog/index.asp?page=Fluggeschwindigkeiten%2C+IAS%2C+TAS%2C+EAS%2C+CAS%2C+Mach#H_Calibrated_Airspeed_.28CAS.29
      /*
      BasicOfH: function() {
        BaroConst.SetAltRange( this.h_m );
        this.TRef = this.TempOfH( this.h_m );
        this.p = this.PressureOfH( this.h_m );
        this.rhoRef = this.DensityOfH( this.h_m );
        this.a = this.AofH( this.h_m );
      },

       * 
      AllOfTas: function() {
        this.mach = this.tas / this.a;
        this.eas = Math.sqrt( this.rho / BaroConst.rho0 ) * this.tas;
        var k1 = (BaroConst.kappa - 1) / 2;
        var k2 = BaroConst.kappa / (BaroConst.kappa - 1);
        var v = this.tas / this.a;
        this.qc = this.p * ( Math.pow( k1 * v * v + 1 , k2 ) - 1 );
        this.cas = BaroConst.a0 * Math.sqrt( (1/k1) * ( Math.pow( this.qc / BaroConst.p0 + 1 , 1/k2 ) - 1 ) );
        this.q = 0.5 * this.rho * this.tas * this.tas;
      },   
       */

      var tas = Mps_From_Kt( tas_kt );

      // create a const model for an altitude (BasicOfH start i.e. SetAltRange)
      var c = new SConst( alt_ft );
      // get derived values  (rest of BasicOfH)
      //double TRef = c.TempOf;
      double p = c.PressureOf;
      //double rhoRef = c.DensityOf;
      double a = c.Aof;
      // calc (AllOfTas)
      //double mach = tas / a;
      //double eas = Math.Sqrt( c.rhoRef / c.rho0 ) * tas;
      double k1 = (c.kappa - 1) / 2;
      double k2 = c.kappa / (c.kappa - 1);
      double v = tas / a;
      double qc = p * (Math.Pow( k1 * v * v + 1, k2 ) - 1);
      double cas = c.a0 * Math.Sqrt( (1 / k1) * (Math.Pow( qc / c.p0 + 1, 1 / k2 ) - 1) );
      //double q = 0.5 * rhoRef * tas_kt * tas_kt;

      return Kt_From_Mps( cas );
    }

    /// <summary>
    /// Returns TAS (True Airspeed) from CAS (Calibrated Airspeed) in [kt]
    /// </summary>
    /// <param name="cas_kt">CAS [kt]</param>
    /// <param name="alt_ft">Altitude [ft]</param>
    /// <returns>TAS [kt]</returns>
    public static double TAS_From_CAS( double cas_kt, double alt_ft )
    {
      //http://walter.bislins.ch/blog/index.asp?page=Fluggeschwindigkeiten%2C+IAS%2C+TAS%2C+EAS%2C+CAS%2C+Mach#H_Calibrated_Airspeed_.28CAS.29
      /*
      BasicOfH: function() {
        BaroConst.SetAltRange( this.h_m );
        this.TRef = this.TempOfH( this.h_m );
        this.p = this.PressureOfH( this.h_m );
        this.rhoRef = this.DensityOfH( this.h_m );
        this.a = this.AofH( this.h_m );
      },

       * 
      AllOfCas: function() {
        var k1 = (BaroConst.kappa - 1) / 2;
        var k2 = BaroConst.kappa / (BaroConst.kappa - 1);
        var v = this.cas / BaroConst.a0;
        this.qc = BaroConst.p0 * ( Math.pow( k1 * v * v + 1, k2 ) - 1 );
        this.tas = this.a * Math.sqrt( (1/k1) * ( Math.pow( this.qc / this.p + 1 , 1/k2 ) - 1 ) );
        this.AllOfTas();
      },     
       */

      var cas = Mps_From_Kt( cas_kt );

      // create a const model for an altitude (BasicOfH start i.e. SetAltRange)
      var c = new SConst( alt_ft );
      // get derived values  (rest of BasicOfH)
      //double TRef = c.TempOf;
      double p = c.PressureOf;
      //double rhoRef = c.DensityOf;
      double a = c.Aof;
      // calc (AllOfCas)
      double k1 = (c.kappa - 1) / 2;
      double k2 = c.kappa / (c.kappa - 1);
      double v = cas / c.a0;
      double qc = c.p0 * (Math.Pow( k1 * v * v + 1, k2 ) - 1);
      double tas = a * Math.Sqrt( (1 / k1) * (Math.Pow( qc / p + 1, 1 / k2 ) - 1) );

      return Kt_From_Mps( tas );
    }

    // * Mach Kt conversion
    // mach tables for conversion
    private static readonly double[] c_AltAbove_ft = new double[] { 0, 10_000, 15_000, 20_000, 25_000, 30_000, 35_000, 40_000 };
    private static readonly double[] c_Mach2Kt = new double[] { 660.0, 638.0, 627.0, 614.0, 602.0, 590.0, 577.0, 573.0 };

    /// <summary>
    /// Convert from Kt to Mach at Altitude [ft] defaults to 30_000 ft
    /// </summary>
    /// <param name="tas_kt">TAS Speed [kt]</param>
    /// <param name="alt_ft">Designated altitude [ft]</param>
    /// <returns>A Mach Number</returns>
    public static double Mach_From_TAS_Kt( double tas_kt, double alt_ft = 30_000 )
    {
      var tas = Mps_From_Kt( tas_kt );
      var c = new SConst( alt_ft );
      return (tas / c.Aof);

      /* Table Lookup Code
            double ret = kt / 660; // default for below 0 alt
            for (int i = 0; i < c_AltAbove_ft.Length; i++) {
              if (alt_ft >= c_AltAbove_ft[i]) {
                ret = kt / c_Mach2Kt[i]; // recalc while going up - well could be done inversely but then...
              }
            }
            return ret;
      */
    }

    /// <summary>
    /// Convert from Mach to Kt at Altitude [ft] defaults to 30_000 ft
    /// </summary>
    /// <param name="mach">A Mach number</param>
    /// <param name="alt_ft">Designated altitude [ft]</param>
    /// <returns>TAS Speed [kt]</returns>
    public static double TAS_Kt_From_Mach( double mach, double alt_ft = 30_000 )
    {
      var c = new SConst( alt_ft );
      return (mach * c.Aof).AsKt_From_Mps( );

      /* Table Lookup Code
      double ret = mach * 660; // default for below 0 alt
      for (int i = 0; i < c_AltAbove_ft.Length; i++) {
        if (alt_ft >= c_AltAbove_ft[i]) {
          ret = mach * c_Mach2Kt[i]; // recalc while going up - well could be done inversely but then...
        }
      }
      return ret;
      */
    }

    /// <summary>
    /// Convert from Kt to Mach at Altitude [ft] defaults to 30_000 ft
    /// </summary>
    /// <param name="cas_kt">CAS Speed [kt]</param>
    /// <param name="alt_ft">Designated altitude [ft]</param>
    /// <returns>A Mach Number</returns>
    public static double Mach_From_CAS_Kt( double cas_kt, double alt_ft = 30_000 )
    {
      var tas_kt = TAS_From_CAS( cas_kt, alt_ft );
      return Mach_From_TAS_Kt( tas_kt, alt_ft );
    }

    /// <summary>
    /// Convert from Mach to Kt at Altitude [ft] defaults to 30_000 ft
    /// </summary>
    /// <param name="mach">A Mach number</param>
    /// <param name="alt_ft">Designated altitude [ft]</param>
    /// <returns>CAS Speed [kt]</returns>
    public static double CAS_Kt_From_Mach( double mach, double alt_ft = 30_000 )
    {
      var tas_kt = TAS_Kt_From_Mach( mach, alt_ft );
      return CAS_From_TAS( tas_kt, alt_ft );
    }

    /// <summary>
    /// Convert from Mach at altitude to Groundspeed (kt)
    /// </summary>
    /// <param name="mach">Mach no</param>
    /// <param name="alt_ft">Altitude [ft]</param>
    /// <returns>GS Speed [kt]</returns>
    public static double GS_From_Mach( double mach, double alt_ft = 30_000 )
    {
      // create a const model for an altitude (BasicOfH start i.e. SetAltRange)
      var c = new SConst( alt_ft );
      double a = c.Aof;

      // GS = M * a where GS=GroundSpeed, M=machNo and S=SpeedOfSound at altitude

      var gs_mps = mach * a;
      return Kt_From_Mps( gs_mps );
    }

    /// <summary>
    /// Convert from Mach at altitude to Groundspeed (kt)
    /// </summary>
    /// <param name="gs_kt">GS Speed [kt]</param>
    /// <param name="alt_ft">Altitude [ft]</param>
    /// <returns>Mach No (at altitude)</returns>
    public static double Mach_From_GS( double gs_kt, double alt_ft = 30_000 )
    {
      // create a const model for an altitude (BasicOfH start i.e. SetAltRange)
      var c = new SConst( alt_ft );
      double a = c.Aof;
      if (a == 0) { return double.NaN; } // sanity exit

      var gs_mps = Mps_From_Kt( gs_kt );

      // M = GS/ a where GS=GroundSpeed, M=machNo and S=SpeedOfSound at altitude

      return gs_mps / a;
    }

    #endregion

  }
}
