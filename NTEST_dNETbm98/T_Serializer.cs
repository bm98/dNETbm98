using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Drawing;

using dNetBm98;
using System.Windows.Forms;
using System.Globalization;

namespace NTEST_dNETbm98
{
  /// <summary>
  /// Basic Test Serializers
  /// 
  /// </summary>
  [TestClass]
  public class T_Serializer
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

    [TestMethod]
    public void Test_XPoint( )
    {
      Point p = new Point( );
      Point px = new Point( );
      string str = "";

      p = new Point( 1, 2 );
      str = p.AsSerString( );
      Assert.AreEqual( "{X=1,Y=2}", str );
      px = XPoint.PointFromSerString( str );
      Assert.AreEqual( p, px );

      p = new Point( -1, -2 );
      str = p.AsSerString( );
      Assert.AreEqual( "{X=-1,Y=-2}", str );
      px = XPoint.PointFromSerString( str );
      Assert.AreEqual( p, px );
    }


    [TestMethod]
    public void Test_XPointF( )
    {
      PointF p = new PointF( );
      PointF px = new PointF( );
      string str = "";

      p = new PointF( 1.125f, 2.874f );
      str = p.AsSerString( );
      Assert.AreEqual( "{X=1.125,Y=2.874}", str );
      px = XPoint.PointFFromSerString( str );
      Assert.AreEqual( p, px );

      p = new PointF( -1.125f, -2.874f );
      str = p.AsSerString( );
      Assert.AreEqual( "{X=-1.125,Y=-2.874}", str );
      px = XPoint.PointFFromSerString( str );
      Assert.AreEqual( p, px );

      p = new PointF( 1.125E12f, -2.874E-12f );
      str = p.AsSerString( );
      Assert.AreEqual( "{X=1.125E+12,Y=-2.874E-12}", str );
      px = XPoint.PointFFromSerString( str );
      Assert.AreEqual( p, px );

    }


    [TestMethod]
    public void Test_XSize( )
    {
      Size s = new Size( );
      Size sx = new Size( );
      string str = "";

      s = new Size( 1, 2 );
      str = s.AsSerString( );
      Assert.AreEqual( "{W=1,H=2}", str );
      sx = XSize.SizeFromSerString( str );
      Assert.AreEqual( s, sx );

      s = new Size( -1, -2 );
      str = s.AsSerString( );
      Assert.AreEqual( "{W=-1,H=-2}", str );
      sx = XSize.SizeFromSerString( str );
      Assert.AreEqual( s, sx );
    }


    [TestMethod]
    public void Test_XSizeF( )
    {
      SizeF s = new SizeF( );
      SizeF sx = new SizeF( );
      string str = "";

      s = new SizeF( 1.125f, 2.874f );
      str = s.AsSerString( );
      Assert.AreEqual( "{W=1.125,H=2.874}", str );
      sx = XSize.SizeFFromSerString( str );
      Assert.AreEqual( s, sx );

      s = new SizeF( -1.125f, -2.874f );
      str = s.AsSerString( );
      Assert.AreEqual( "{W=-1.125,H=-2.874}", str );
      sx = XSize.SizeFFromSerString( str );
      Assert.AreEqual( s, sx );

      s = new SizeF( -1.125E12f, -2.874E-12f );
      str = s.AsSerString( );
      Assert.AreEqual( "{W=-1.125E+12,H=-2.874E-12}", str );
      sx = XSize.SizeFFromSerString( str );
      Assert.AreEqual( s, sx );
    }

    [TestMethod]
    public void Test_XRect( )
    {
      Rectangle r = new Rectangle( );
      Rectangle rx = new Rectangle( );
      string str = "";

      r = new Rectangle( 1, 2, 10, 20 );
      str = r.AsSerString( );
      Assert.AreEqual( "{X=1,Y=2,W=10,H=20}", str );
      rx = XRect.RectangleFromSerString( str );
      Assert.AreEqual( r, rx );

      r = new Rectangle( -1, -2, 10, 20 );
      str = r.AsSerString( );
      Assert.AreEqual( "{X=-1,Y=-2,W=10,H=20}", str );
      rx = XRect.RectangleFromSerString( str );
      Assert.AreEqual( r, rx );
    }


    [TestMethod]
    public void Test_XRectF( )
    {
      RectangleF r = new RectangleF( );
      RectangleF rx = new RectangleF( );
      string str = "";

      r = new RectangleF( 1.125f, 2.874f, 10.9f, 20.2f );
      str = r.AsSerString( );
      Assert.AreEqual( "{X=1.125,Y=2.874,W=10.9,H=20.2}", str );
      rx = XRect.RectangleFFromSerString( str );
      Assert.AreEqual( r, rx );

      r = new RectangleF( -1.125f, -2.874f, 10.9f, 20.2f );
      str = r.AsSerString( );
      Assert.AreEqual( "{X=-1.125,Y=-2.874,W=10.9,H=20.2}", str );
      rx = XRect.RectangleFFromSerString( str );
      Assert.AreEqual( r, rx );

      r = new RectangleF( -1.125E12f, -2.874E-12f, 10.9f, 20.2f );
      str = r.AsSerString( );
      Assert.AreEqual( "{X=-1.125E+12,Y=-2.874E-12,W=10.9,H=20.2}", str );
      rx = XRect.RectangleFFromSerString( str );
      Assert.AreEqual( r, rx );
    }

    [TestMethod]
    public void Test_Padding( )
    {
      Padding r = new Padding( );
      Padding rx = new Padding( );
      string str = "";

      r = new Padding( 1, 2, 10, 20 );
      str = r.AsSerString( );
      Assert.AreEqual( "{L=1,T=2,R=10,B=20}", str );
      rx = Utilities.PaddingFromSerString( str );
      Assert.AreEqual( r, rx );

      r = new Padding( -1, -2, -10, -20 );
      str = r.AsSerString( );
      Assert.AreEqual( "{L=-1,T=-2,R=-10,B=-20}", str );
      rx = Utilities.PaddingFromSerString( str );
      Assert.AreEqual( r, rx );
    }


    [TestMethod]
    public void Test_Errors( )
    {
      Assert.AreEqual( new Point( ), XPoint.PointFromSerString( "{Z=1,Y=2}" ) );// invalid Z must return Point(0,0)
      Assert.AreEqual( new Point( ), XPoint.PointFromSerString( "{X=1;Y=2}" ) );// invalid divider must return Point(0,0)
      Assert.AreEqual( new Point( ), XPoint.PointFromSerString( "{X=1,Y=2" ) );// invalid end must return Point(0,0)
      Assert.AreEqual( new PointF( ), XPoint.PointFFromSerString( "{X=1,1,Y=2}" ) );// invalid decimal must return PointF(0,0)
      Assert.AreEqual( new PointF( ), XPoint.PointFFromSerString( "{X=- 1.1,Y=2}" ) );// invalid negative must return PointF(0,0)

      Assert.AreEqual( new Size( ), XSize.SizeFromSerString( "{X=1,H=2}" ) );// invalid X must return Size(0,0)
      Assert.AreEqual( new Size( ), XSize.SizeFromSerString( "{W=1;H=2}" ) );// invalid divider must return Size(0,0)
      Assert.AreEqual( new Size( ), XSize.SizeFromSerString( "{W=1,H=2" ) );// invalid end must return Size(0,0)
      Assert.AreEqual( new SizeF( ), XSize.SizeFFromSerString( "{W=1,1,H=2}" ) );// invalid decimal must return SizeF(0,0)
      Assert.AreEqual( new SizeF( ), XSize.SizeFFromSerString( "{W=- 1.1,H=2}" ) );// invalid negative must return SizeF(0,0)

      Assert.AreEqual( new Rectangle( ), XRect.RectangleFromSerString( "{X=1,Y=2,B=3,H=4}" ) );// invalid B must return Rectangle(0,0,0,0)
      Assert.AreEqual( new Rectangle( ), XRect.RectangleFromSerString( "{X=1,Y=2,W=1;H=2}" ) );// invalid divider must return Rectangle(0,0,0,0)
      Assert.AreEqual( new Rectangle( ), XRect.RectangleFromSerString( "{X=1,Y=2,W=1,H=2" ) );// invalid end must return Rectangle(0,0,0,0)
      Assert.AreEqual( new RectangleF( ), XRect.RectangleFFromSerString( "{X=1,Y=2,W=1,1,H=2}" ) );// invalid decimal must return RectangleF(0,0,0,0)
      Assert.AreEqual( new RectangleF( ), XRect.RectangleFFromSerString( "{X=1,Y=2,W=- 1.1,H=2}" ) );// invalid negative must return RectangleF(0,0,0,0)

      Assert.AreEqual( new Padding( ), Utilities.PaddingFromSerString( "{X=1,T=2,R=3,B=4}" ) );// invalid X must return Padding(0,0,0,0)
      Assert.AreEqual( new Padding( ), Utilities.PaddingFromSerString( "{L=1,I=2,R=1;B=2}" ) );// invalid divider must return Padding(0,0,0,0)
      Assert.AreEqual( new Padding( ), Utilities.PaddingFromSerString( "{L=1,T=2,R=1,B=2" ) );// invalid end must return Padding(0,0,0,0)
      Assert.AreEqual( new Padding( ), Utilities.PaddingFromSerString( "{L=1,T=2,R=1.1,B=2}" ) );// invalid decimal must return Padding(0,0,0,0)
      Assert.AreEqual( new Padding( ), Utilities.PaddingFromSerString( "{L=1,T=2,R=- 1,B=2}" ) );// invalid negative must return Padding(0,0,0,0)
    }

    /// <summary>
    /// Test if it works with a culture using , as decimal separator
    /// </summary>
    [TestMethod]
    public void Test_Global( )
    {
      T_GlobalTools.SetTestCulture( );

      PointF p = new PointF( );
      PointF px = new PointF( );
      string str = "";

      p = new PointF( 1.125f, 2.874f );
      str = p.AsSerString( );
      Assert.AreEqual( "{X=1.125,Y=2.874}", str );
      px = XPoint.PointFFromSerString( str );
      Assert.AreEqual( p, px );

      SizeF s = new SizeF( );
      SizeF sx = new SizeF( );

      s = new SizeF( 1.125f, 2.874f );
      str = s.AsSerString( );
      Assert.AreEqual( "{W=1.125,H=2.874}", str );
      sx = XSize.SizeFFromSerString( str );
      Assert.AreEqual( s, sx );


      RectangleF r = new RectangleF( );
      RectangleF rx = new RectangleF( );

      r = new RectangleF( 1.125f, 2.874f, 10.9f, 20.2f );
      str = r.AsSerString( );
      Assert.AreEqual( "{X=1.125,Y=2.874,W=10.9,H=20.2}", str );
      rx = XRect.RectangleFFromSerString( str );
      Assert.AreEqual( r, rx );


      T_GlobalTools.ResetCulture( );
    }
  }
}
