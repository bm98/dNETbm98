using dNetBm98;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics;
using System.Globalization;

namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_XIO
  {
    // set on init
    private static CultureInfo _localCulture = CultureInfo.CurrentCulture;
    // test culture which does have a number format which is not using decimal points
    private void SetTestCulture( )
    {
      CultureInfo.CurrentCulture = new CultureInfo( "it_it" ); // uses comma as decimal point
    }

    // test culture which does have a number format which is not using decimal points
    private void ResetCulture( )
    {
      CultureInfo.CurrentCulture = new CultureInfo( _localCulture.LCID ); // uses comma as decimal point
    }

    /// <summary>
    /// Basic Test Serializers
    /// 
    /// </summary>
    [TestMethod]
    public void Test_XRealNumbers( )
    {
      string str = "";

      float f;
      float fx;
      f = 1.25f; // when choosing a number take care of representation limits in binary format...
      str = f.ToStringX( );
      Assert.AreEqual( "1.25", str );
      fx = XIO.ParseXf( str );
      Assert.AreEqual( f, fx );

      f = -1.25f;
      str = f.ToStringX( );
      Assert.AreEqual( "-1.25", str );
      fx = XIO.ParseXf( str );
      Assert.AreEqual( f, fx );

      double d;
      double dx;
      d = 1.25d; // when choosing a number take care of representation limits in binary format...
      str = d.ToStringX( );
      Assert.AreEqual( "1.25", str );
      dx = XIO.ParseXd( str );
      Assert.AreEqual( d, dx );

      d = -1.25d;
      str = d.ToStringX( );
      Assert.AreEqual( "-1.25", str );
      dx = XIO.ParseXf( str );
      Assert.AreEqual( d, dx );

      decimal m;
      decimal mx;
      m = 1.25m; // when choosing a number take care of representation limits in binary format...
      str = m.ToStringX( );
      Assert.AreEqual( "1.25", str );
      mx = XIO.ParseXm( str );
      Assert.AreEqual( m, mx );

      m = -1.25m;
      str = m.ToStringX( );
      Assert.AreEqual( "-1.25", str );
      mx = XIO.ParseXm( str );
      Assert.AreEqual( m, mx );

    }

    /// <summary>
    /// Formatters
    /// 
    /// </summary>
    [TestMethod]
    public void Test_XRealFormatter( )
    {
      string str = "";

      float f;
      float fx;
      f = 1.25f;
      str = XIO.FormatX(f,"{0:0.000}" );
      Assert.AreEqual( "1.250", str );
      fx = XIO.ParseXf( str );
      Assert.AreEqual( f, fx );

      f = -1.25f;
      str = XIO.FormatX( f, "{0:0.000}" );
      Assert.AreEqual( "-1.250", str );
      fx = XIO.ParseXf( str );
      Assert.AreEqual( f, fx );

      double d;
      double dx;
      d = 1.25d;
      str = XIO.FormatX( d, "{0:0.000}" );
      Assert.AreEqual( "1.250", str );
      dx = XIO.ParseXd( str );
      Assert.AreEqual( d, dx );

      d = -1.25d;
      str = XIO.FormatX( d, "{0:0.000}" );
      Assert.AreEqual( "-1.250", str );
      dx = XIO.ParseXf( str );
      Assert.AreEqual( d, dx );

      decimal m;
      decimal mx;
      m = 1.25m;
      str = XIO.FormatX( m, "{0:0.000}" );
      Assert.AreEqual( "1.250", str );
      mx = XIO.ParseXm( str );
      Assert.AreEqual( m, mx );

      m = -1.25m;
      str = XIO.FormatX( m, "{0:0.000}" );
      Assert.AreEqual( "-1.250", str );
      mx = XIO.ParseXm( str );
      Assert.AreEqual( m, mx );

    }


    /// <summary>
    /// Test if it works with a culture using , as decimal separator
    /// </summary>
    [TestMethod]
    public void Test_Global( )
    {
      T_GlobalTools.SetTestCulture( );

      string str = "";

      // Serialize and Parse 

      float f;
      float fx;
      f = 1.25f; // when choosing a number take care of representation limits in binary format...
      str = f.ToStringX( );
      Debug.WriteLine( $"f.ToStringX NATIVE: <{f}>   XIO: {str}" );

      Assert.AreEqual( "1.25", str );
      fx = XIO.ParseXf( str );
      Assert.AreEqual( f, fx );

      f = -1.25f;
      str = f.ToStringX( );
      Assert.AreEqual( "-1.25", str );
      fx = XIO.ParseXf( str );
      Assert.AreEqual( f, fx );

      double d;
      double dx;
      d = 1.25d; // when choosing a number take care of representation limits in binary format...
      str = d.ToStringX( );
      Debug.WriteLine( $"d.ToStringX NATIVE: <{d}>   XIO: {str}" );
      Assert.AreEqual( "1.25", str );
      dx = XIO.ParseXd( str );
      Assert.AreEqual( d, dx );

      d = -1.25d;
      str = d.ToStringX( );
      Assert.AreEqual( "-1.25", str );
      dx = XIO.ParseXf( str );
      Assert.AreEqual( d, dx );

      decimal m;
      decimal mx;
      m = 1.25m; // when choosing a number take care of representation limits in binary format...
      str = m.ToStringX( );
      Debug.WriteLine( $"m.ToStringX NATIVE: <{m}>   XIO: {str}" );
      Assert.AreEqual( "1.25", str );
      mx = XIO.ParseXm( str );
      Assert.AreEqual( m, mx );

      m = -1.25m;
      str = m.ToStringX( );
      Assert.AreEqual( "-1.25", str );
      mx = XIO.ParseXm( str );
      Assert.AreEqual( m, mx );

      // Formatter
      f = 1.25f;
      str = XIO.FormatX( f, "{0:0.000}" );
      Debug.WriteLine( $"XIO.FormatX( f ) NATIVE: <{f}>   XIO: {str}" );
      Assert.AreEqual( "1.250", str );
      fx = XIO.ParseXf( str );
      Assert.AreEqual( f, fx );

      f = -1.25f;
      str = XIO.FormatX( f, "{0:0.000}" );
      Assert.AreEqual( "-1.250", str );
      fx = XIO.ParseXf( str );
      Assert.AreEqual( f, fx );

      d = 1.25d;
      str = XIO.FormatX( d, "{0:0.000}" );
      Debug.WriteLine( $"XIO.FormatX( d ) NATIVE: <{d}>   XIO: {str}" );
      Assert.AreEqual( "1.250", str );
      dx = XIO.ParseXd( str );
      Assert.AreEqual( d, dx );

      d = -1.25d;
      str = XIO.FormatX( d, "{0:0.000}" );
      Assert.AreEqual( "-1.250", str );
      dx = XIO.ParseXf( str );
      Assert.AreEqual( d, dx );

      m = 1.25m;
      str = XIO.FormatX( m, "{0:0.000}" );
      Debug.WriteLine( $"XIO.FormatX( m ) NATIVE: <{m}>   XIO: {str}" );
      Assert.AreEqual( "1.250", str );
      mx = XIO.ParseXm( str );
      Assert.AreEqual( m, mx );

      m = -1.25m;
      str = XIO.FormatX( m, "{0:0.000}" );
      Assert.AreEqual( "-1.250", str );
      mx = XIO.ParseXm( str );
      Assert.AreEqual( m, mx );

      T_GlobalTools.ResetCulture( );
    }



  }
}
