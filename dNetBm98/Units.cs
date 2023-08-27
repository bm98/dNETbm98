using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

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
    /// Returns kt from km/h_m
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
    /// Returns km/h_m from m/sec
    /// </summary>
    /// <param name="kmh">m/sec</param>
    /// <returns>Kilometer per hour</returns>
    public static double Kmh_From_Mps( double kmh ) => kmh * 3600.0 / 1000.0;
    /// <summary>
    /// Returns m/sec from km/h_m
    /// </summary>
    /// <param name="kmh">km/h_m</param>
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

    #region TAS, CAS conversions

    /*
     Credit: http://walter.bislins.ch/fsim/index.asp?page=Fluggeschwindigkeiten

     */

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

    #endregion

  }
}
