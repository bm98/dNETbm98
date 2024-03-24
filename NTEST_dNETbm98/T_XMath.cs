using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using dNetBm98;

namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_XMath
  {
    [TestMethod]
    public void Test_AboutEqual( )
    {
      // trivial x=y
      Assert.AreEqual( true, XMath.AboutEqual( 0.0, 0.0, 0.0000001 ) );
      Assert.AreEqual( true, XMath.AboutEqual( 0.0f, 0.0f, 0.0000001f ) );
      Assert.AreEqual( true, XMath.AboutEqual( 0L, 0L, 1L ) );
      Assert.AreEqual( true, XMath.AboutEqual( 0, 0, 1 ) );

      Assert.AreEqual( true, XMath.AboutEqual( 100.0, 100.0, 0.0000001 ) );
      Assert.AreEqual( true, XMath.AboutEqual( 100.0f, 100.0f, 0.0000001f ) );
      Assert.AreEqual( true, XMath.AboutEqual( 100L, 100L, 1L ) );
      Assert.AreEqual( true, XMath.AboutEqual( 100, 100, 1 ) );

      Assert.AreEqual( true, XMath.AboutEqual( -100.0, -100.0, 0.0000001 ) );
      Assert.AreEqual( true, XMath.AboutEqual( -100.0f, -100.0f, 0.0000001f ) );
      Assert.AreEqual( true, XMath.AboutEqual( -100L, -100L, 1L ) );
      Assert.AreEqual( true, XMath.AboutEqual( -100, -100, 1 ) );

      // some other tests for basic functionality

      // equal outcomes
      Assert.AreEqual( true, XMath.AboutEqual( 0.001, 0.0, 0.01 ) );
      Assert.AreEqual( true, XMath.AboutEqual( 0.001f, 0.0f, 0.01f ) );

      Assert.AreEqual( true, XMath.AboutEqual( 1000.0, 1000.001, 0.01 ) );
      Assert.AreEqual( true, XMath.AboutEqual( 1000.0f, 1000.001f, 0.01f ) );

      Assert.AreEqual( true, XMath.AboutEqual( -1000.0, -1000.001, 0.01 ) );
      Assert.AreEqual( true, XMath.AboutEqual( -1000.0f, -1000.001f, 0.01f ) );

      Assert.AreEqual( true, XMath.AboutEqual( 1000L, 1001L, 2L ) );
      Assert.AreEqual( true, XMath.AboutEqual( 1000, 1001, 2 ) );

      Assert.AreEqual( true, XMath.AboutEqual( -1000L, -1001L, 2L ) );
      Assert.AreEqual( true, XMath.AboutEqual( -1000, -1001, 2 ) );

      Assert.AreEqual( true, XMath.AboutEqual( -0.001, 0.001, 0.01 ) );
      Assert.AreEqual( true, XMath.AboutEqual( -0.001f, 0.001f, 0.01f ) );

      Assert.AreEqual( true, XMath.AboutEqual( -1L, 1L, 3L ) );
      Assert.AreEqual( true, XMath.AboutEqual( -1, 1, 3 ) );


      // not equal outcomes
      Assert.AreEqual( false, XMath.AboutEqual( 0.001, 0.0, 0.001 ) );
      Assert.AreEqual( false, XMath.AboutEqual( 0.001f, 0.0f, 0.001f ) );

      Assert.AreEqual( false, XMath.AboutEqual( -0.001, -0.0, 0.001 ) );
      Assert.AreEqual( false, XMath.AboutEqual( -0.001f, -0.0f, 0.001f ) );

      Assert.AreEqual( false, XMath.AboutEqual( 1000L, 1001L, 1L ) );
      Assert.AreEqual( false, XMath.AboutEqual( 1000, 1001, 1 ) );

      Assert.AreEqual( false, XMath.AboutEqual( -1000L, -1001L, 1L ) );
      Assert.AreEqual( false, XMath.AboutEqual( -1000, -1001, 1 ) );

      Assert.AreEqual( false, XMath.AboutEqual( -1L, 1L, 2L ) );


      // Throwing Epsilon Exception
      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0, -1000.001, double.Epsilon ); } );
      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0, -1000.001, 0 ); } );
      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0, -1000.001, -1 ); } );

      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0f, -1000.001f, float.Epsilon ); } );
      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0f, -1000.001f, 0f ); } );
      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0f, -1000.001f, -1f ); } );

      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000, -1000, 0 ); } );
      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000, -1000, -1 ); } );

      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000L, -1000L, 0L ); } );
      Assert.ThrowsException<ArgumentException>( ( ) => { XMath.AboutEqual( -1000L, -1000L, -1L ); } );

    }
  }
}
