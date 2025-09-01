using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Globalization;

using dNetBm98;
using static dNetBm98.Utilities;
using static dNetBm98.XIO;

namespace NTEST_dNETbm98
{
  public static class T_GlobalTools
  {
    // set on init
    private static CultureInfo _localCulture = CultureInfo.CurrentCulture;
    // test culture which does have a number format which is not using decimal points
    public static void SetTestCulture( )
    {
      CultureInfo.CurrentCulture = new CultureInfo( "it_it" ); // uses comma as decimal point
    }

    // test culture which does have a number format which is not using decimal points
    public static void ResetCulture( )
    {
      CultureInfo.CurrentCulture = new CultureInfo( _localCulture.LCID ); // uses comma as decimal point
    }
  }

  /// <summary>
  /// Test Globalization (invariant culture) support
  /// </summary>
  [TestClass]
  public class T_Global
  {

    [TestMethod]
    public void Test_CultureSetup( )
    {
      T_GlobalTools.SetTestCulture( );

      double d = 1.1;

      // check if the culture set is really de_de
      Assert.AreEqual( "€", CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol );
      Assert.AreEqual( ",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator );
      Assert.AreEqual( ";", CultureInfo.CurrentCulture.TextInfo.ListSeparator );
      Assert.AreEqual( "1,1", d.ToString( ) ); // test if comma is used by .net

      // set to uniform number format
      SetThreadUniformat( CultureInfo.CurrentCulture );

      Assert.AreEqual( "€", CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol );
      Assert.AreEqual( ";", CultureInfo.CurrentCulture.TextInfo.ListSeparator );
      Assert.AreEqual( ".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator );
      Assert.AreEqual( "1.1", d.ToString( ) ); // test if dot is used by .net


      T_GlobalTools.ResetCulture( );
    }

    /// <summary>
    /// Test XIO number formatting and parsing using Invariant decimal signatures
    /// </summary>
    [TestMethod]
    public void Test_BasicNumbers( )
    {
      T_GlobalTools.SetTestCulture( ); // change to non decimal point culture

      Assert.AreEqual( "1,1", (1.1).ToString( ) ); // test if comma is used by .net default formatters

      decimal m = 1.123m;
      double d = 1.123;
      float f = 1.12f; // can be represented exactly with a float
      sbyte sb = 123;
      short s = 1123;
      int i = 1123;
      long l = 1123;

      byte b = 123;
      ushort us = 1123;
      uint ui = 1123;
      ulong ul = 1123;

      // stringify number types
      Assert.AreEqual( "1.123", m.ToStringX( ) );
      Assert.AreEqual( "1.123", d.ToStringX( ) );
      Assert.AreEqual( "1.12", f.ToStringX( ) );

      Assert.AreEqual( "123", sb.ToStringX( ) );
      Assert.AreEqual( "1123", s.ToStringX( ) );
      Assert.AreEqual( "1123", i.ToStringX( ) );
      Assert.AreEqual( "1123", l.ToStringX( ) );

      Assert.AreEqual( "123", b.ToStringX( ) );
      Assert.AreEqual( "1123", us.ToStringX( ) );
      Assert.AreEqual( "1123", ui.ToStringX( ) );
      Assert.AreEqual( "1123", ul.ToStringX( ) );

      m = -m; d = -d; f = -f; sb = (sbyte)-sb; s = (short)-s; i = -i; l = -l;
      Assert.AreEqual( "-1.123", m.ToStringX( ) );
      Assert.AreEqual( "-1.123", d.ToStringX( ) );
      Assert.AreEqual( "-1.12", f.ToStringX( ) );

      Assert.AreEqual( "-123", sb.ToStringX( ) );
      Assert.AreEqual( "-1123", s.ToStringX( ) );
      Assert.AreEqual( "-1123", i.ToStringX( ) );
      Assert.AreEqual( "-1123", l.ToStringX( ) );

      m = 1.1234567890123456m; // roundtrip number sizes
      d = 1.1234567890123399;
      f = 1.12345672f;
      Assert.AreEqual( "1.1234567890123456", m.ToStringX( ) );
      Assert.AreEqual( "1.1234567890123399", d.ToStringX( ) );
      Assert.AreEqual( "1.12345672", f.ToStringX( ) );

      // Parse number types
      Assert.AreEqual( 1.123m, ParseXm( "1.123" ) );
      Assert.AreEqual( 1.123, ParseXd( "1.123" ) );
      Assert.AreEqual( 1.12f, ParseXf( "1.12" ) );

      Assert.AreEqual( (sbyte)123, ParseXsb( "123" ) );
      Assert.AreEqual( (short)1123, ParseXs( "1123" ) );
      Assert.AreEqual( (int)1123, ParseXi( "1123" ) );
      Assert.AreEqual( (long)1123, ParseXl( "1123" ) );

      Assert.AreEqual( (byte)123, ParseXb( "123" ) );
      Assert.AreEqual( (ushort)1123, ParseXus( "1123" ) );
      Assert.AreEqual( (uint)1123, ParseXui( "1123" ) );
      Assert.AreEqual( (ulong)1123, ParseXul( "1123" ) );

      Assert.AreEqual( (sbyte)-123, ParseXsb( "-123" ) );
      Assert.AreEqual( (short)-1123, ParseXs( "-1123" ) );
      Assert.AreEqual( (int)-1123, ParseXi( "-1123" ) );
      Assert.AreEqual( (long)-1123, ParseXl( "-1123" ) );

      Assert.AreEqual( (byte)123, ParseXb( "123" ) );
      Assert.AreEqual( (ushort)1123, ParseXus( "1123" ) );
      Assert.AreEqual( (uint)1123, ParseXui( "1123" ) );
      Assert.AreEqual( (ulong)1123, ParseXul( "1123" ) );

      // Try Parse number types
      Assert.AreEqual( true, TryParseX( "1.123", out m ) ); Assert.AreEqual( 1.123m, m );
      Assert.AreEqual( true, TryParseX( "1.123", out d ) ); Assert.AreEqual( 1.123d, d );
      Assert.AreEqual( true, TryParseX( "1.12", out f ) ); Assert.AreEqual( 1.12f, f );

      Assert.AreEqual( true, TryParseX( "123", out sb ) ); Assert.AreEqual( (sbyte)123, sb );
      Assert.AreEqual( true, TryParseX( "1123", out s ) ); Assert.AreEqual( (short)1123, s );
      Assert.AreEqual( true, TryParseX( "1123", out i ) ); Assert.AreEqual( (int)1123, i );
      Assert.AreEqual( true, TryParseX( "1123", out l ) ); Assert.AreEqual( (long)1123, l );

      Assert.AreEqual( true, TryParseX( "123", out b ) ); Assert.AreEqual( (byte)123, b );
      Assert.AreEqual( true, TryParseX( "1123", out us ) ); Assert.AreEqual( (ushort)1123, us );
      Assert.AreEqual( true, TryParseX( "1123", out ui ) ); Assert.AreEqual( (uint)1123, ui );
      Assert.AreEqual( true, TryParseX( "1123", out ul ) ); Assert.AreEqual( (ulong)1123, ul );

      Assert.AreEqual( true, TryParseX( "-1.123", out m ) ); Assert.AreEqual( -1.123m, m );
      Assert.AreEqual( true, TryParseX( "-1.123", out d ) ); Assert.AreEqual( -1.123d, d );
      Assert.AreEqual( true, TryParseX( "-1.12", out f ) ); Assert.AreEqual( -1.12f, f );

      Assert.AreEqual( true, TryParseX( "-123", out sb ) ); Assert.AreEqual( (sbyte)-123, sb );
      Assert.AreEqual( true, TryParseX( "-1123", out s ) ); Assert.AreEqual( (short)-1123, s );
      Assert.AreEqual( true, TryParseX( "-1123", out i ) ); Assert.AreEqual( (int)-1123, i );
      Assert.AreEqual( true, TryParseX( "-1123", out l ) ); Assert.AreEqual( (long)-1123, l );


      T_GlobalTools.ResetCulture( );
    }




  }
}
