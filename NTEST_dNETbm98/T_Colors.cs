using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Drawing;

using dNetBm98;
using dNetBm98.ColorModel;

namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_Colors
  {
    [TestMethod]
    public void Test_ColorH( )
    {
      Assert.AreEqual( "#ffff0000", XColor.ToColorH( Color.FromArgb( 255, 0, 0 ) ) );
      Assert.AreEqual( "#ff00ff00", XColor.ToColorH( Color.FromArgb( 0, 255, 0 ) ) );
      Assert.AreEqual( "#ff0000ff", XColor.ToColorH( Color.FromArgb( 0, 0, 255 ) ) );

      Assert.AreEqual( Color.FromArgb( 255, 0, 0 ), XColor.FromColorH( "#ffff0000" ) );
      Assert.AreEqual( Color.FromArgb( 0, 255, 0 ), XColor.FromColorH( "#ff00ff00" ) );
      Assert.AreEqual( Color.FromArgb( 0, 0, 255 ), XColor.FromColorH( "#ff0000ff" ) );

      // round trip
      Color col = Color.FromArgb( 0, 0, 0 );
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorH( XColor.ToColorH( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.FromArgb( 255, 255, 255 );
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorH( XColor.ToColorH( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.DimGray;
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorH( XColor.ToColorH( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.Pink;
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorH( XColor.ToColorH( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.DarkSlateBlue;
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorH( XColor.ToColorH( col ) ).ToArgb( ) ); // must compare the Value
      // Alpha channel
      col = Color.FromArgb( 0, 0, 0, 0 );
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorH( XColor.ToColorH( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.FromArgb( 128, 255, 255, 255 );
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorH( XColor.ToColorH( col ) ).ToArgb( ) ); // must compare the Value
    }

    [TestMethod]
    public void Test_ColorS( )
    {
      // round trip
      Color col = Color.FromArgb( 0, 0, 0 );
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorS( XColor.ToColorS( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.FromArgb( 255, 255, 255 );
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorS( XColor.ToColorS( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.DimGray;
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorS( XColor.ToColorS( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.Pink;
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorS( XColor.ToColorS( col ) ).ToArgb( ) ); // must compare the Value
      col = Color.DarkSlateBlue;
      Assert.AreEqual( col.ToArgb( ), XColor.FromColorS( XColor.ToColorS( col ) ).ToArgb( ) ); // must compare the Value
    }

    [TestMethod]
    public void Test_ColorComp( )
    {
      // double
      Assert.AreEqual( 0, XColor.ColorComp( 0.0 ) );
      Assert.AreEqual( 0, XColor.ColorComp( -1.0 ) );
      Assert.AreEqual( 15, XColor.ColorComp( 15.0 ) );
      Assert.AreEqual( 255, XColor.ColorComp( 256.0 ) );
      Assert.AreEqual( 255, XColor.ColorComp( 255.0000001 ) );
      // float
      Assert.AreEqual( 0, XColor.ColorComp( 0.0f ) );
      Assert.AreEqual( 0, XColor.ColorComp( -1.0f ) );
      Assert.AreEqual( 15, XColor.ColorComp( 15.0f ) );
      Assert.AreEqual( 255, XColor.ColorComp( 256.0f ) );
      Assert.AreEqual( 255, XColor.ColorComp( 255.0000001f ) );
      // int
      Assert.AreEqual( 0, XColor.ColorComp( 0 ) );
      Assert.AreEqual( 0, XColor.ColorComp( -1 ) );
      Assert.AreEqual( 15, XColor.ColorComp( 15 ) );
      Assert.AreEqual( 255, XColor.ColorComp( 256 ) );
      // long
      Assert.AreEqual( 0, XColor.ColorComp( 0L ) );
      Assert.AreEqual( 0, XColor.ColorComp( -1L ) );
      Assert.AreEqual( 15, XColor.ColorComp( 15L ) );
      Assert.AreEqual( 255, XColor.ColorComp( 256L ) );
      // short
      Assert.AreEqual( 0, XColor.ColorComp( (short)0 ) );
      Assert.AreEqual( 0, XColor.ColorComp( (short)-1 ) );
      Assert.AreEqual( 15, XColor.ColorComp( (short)15 ) );
      Assert.AreEqual( 255, XColor.ColorComp( (short)256 ) );

      // double
      Assert.AreEqual( 0, 0.0.AsColorComp( ) );
      Assert.AreEqual( 0, (-1.0).AsColorComp( ) );
      Assert.AreEqual( 15, 15.0.AsColorComp( ) );
      Assert.AreEqual( 255, 256.0.AsColorComp( ) );
      Assert.AreEqual( 255, 255.0000001.AsColorComp( ) );
      // float
      Assert.AreEqual( 0, 0.0f.AsColorComp( ) );
      Assert.AreEqual( 0, (-1.0f).AsColorComp( ) );
      Assert.AreEqual( 15, 15.0f.AsColorComp( ) );
      Assert.AreEqual( 255, 256.0f.AsColorComp( ) );
      Assert.AreEqual( 255, 255.0000001f.AsColorComp( ) );
      // int
      Assert.AreEqual( 0, 0.AsColorComp( ) );
      Assert.AreEqual( 0, (-1).AsColorComp( ) );
      Assert.AreEqual( 15, 15.AsColorComp( ) );
      Assert.AreEqual( 255, 256.AsColorComp( ) );
      // long
      Assert.AreEqual( 0, 0L.AsColorComp( ) );
      Assert.AreEqual( 0, (-1L).AsColorComp( ) );
      Assert.AreEqual( 15, 15L.AsColorComp( ) );
      Assert.AreEqual( 255, 256L.AsColorComp( ) );
      // short
      Assert.AreEqual( 0, (short)0.AsColorComp( ) );
      Assert.AreEqual( 0, (short)(-1).AsColorComp( ) );
      Assert.AreEqual( 15, (short)15.AsColorComp( ) );
      Assert.AreEqual( 255, (short)256.AsColorComp( ) );
    }

    [TestMethod]
    public void Test_MaxMinComp( )
    {
      Assert.AreEqual( 255, XColor.MaxComp( Color.FromArgb( 255, 128, 0 ) ) );
      Assert.AreEqual( 0, XColor.MinComp( Color.FromArgb( 255, 128, 0 ) ) );

      Assert.AreEqual( 255, Color.FromArgb( 255, 128, 0 ).MaxCompOf( ) );
      Assert.AreEqual( 0, Color.FromArgb( 255, 128, 0 ).MinCompOf( ) );
    }

    [TestMethod]
    public void Test_Test( )
    {
      var hsb = HSB.FromRgb(Color.FromArgb(174,96,172  ) );
    }


    }
  }
