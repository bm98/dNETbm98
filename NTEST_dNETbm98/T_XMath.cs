using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using dNetBm98;

[assembly: Parallelize]
namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_XMath
  {
    [TestMethod]
    public void Test_AboutEqual( )
    {
      // trivial x=y
      Assert.IsTrue(  XMath.AboutEqual( 0.0, 0.0, 0.0000001 ) );
      Assert.IsTrue(  XMath.AboutEqual( 0.0f, 0.0f, 0.0000001f ) );
      Assert.IsTrue(  XMath.AboutEqual( 0L, 0L, 1L ) );
      Assert.IsTrue(  XMath.AboutEqual( 0, 0, 1 ) );

      Assert.IsTrue(  XMath.AboutEqual( 100.0, 100.0, 0.0000001 ) );
      Assert.IsTrue(  XMath.AboutEqual( 100.0f, 100.0f, 0.0000001f ) );
      Assert.IsTrue(  XMath.AboutEqual( 100L, 100L, 1L ) );
      Assert.IsTrue(  XMath.AboutEqual( 100, 100, 1 ) );

      Assert.IsTrue(  XMath.AboutEqual( -100.0, -100.0, 0.0000001 ) );
      Assert.IsTrue(  XMath.AboutEqual( -100.0f, -100.0f, 0.0000001f ) );
      Assert.IsTrue(  XMath.AboutEqual( -100L, -100L, 1L ) );
      Assert.IsTrue(  XMath.AboutEqual( -100, -100, 1 ) );

      // some other tests for basic functionality

      // equal outcomes
      Assert.IsTrue(  XMath.AboutEqual( 0.001, 0.0, 0.01 ) );
      Assert.IsTrue(  XMath.AboutEqual( 0.001f, 0.0f, 0.01f ) );

      Assert.IsTrue(  XMath.AboutEqual( 1000.0, 1000.001, 0.01 ) );
      Assert.IsTrue(  XMath.AboutEqual( 1000.0f, 1000.001f, 0.01f ) );

      Assert.IsTrue(  XMath.AboutEqual( -1000.0, -1000.001, 0.01 ) );
      Assert.IsTrue(  XMath.AboutEqual( -1000.0f, -1000.001f, 0.01f ) );

      Assert.IsTrue(  XMath.AboutEqual( 1000L, 1001L, 2L ) );
      Assert.IsTrue(  XMath.AboutEqual( 1000, 1001, 2 ) );

      Assert.IsTrue(  XMath.AboutEqual( -1000L, -1001L, 2L ) );
      Assert.IsTrue(  XMath.AboutEqual( -1000, -1001, 2 ) );

      Assert.IsTrue(  XMath.AboutEqual( -0.001, 0.001, 0.01 ) );
      Assert.IsTrue(  XMath.AboutEqual( -0.001f, 0.001f, 0.01f ) );

      Assert.IsTrue(  XMath.AboutEqual( -1L, 1L, 3L ) );
      Assert.IsTrue(  XMath.AboutEqual( -1, 1, 3 ) );


      // not equal outcomes
      Assert.IsFalse( XMath.AboutEqual( 0.001, 0.0, 0.001 ) );
      Assert.IsFalse( XMath.AboutEqual( 0.001f, 0.0f, 0.001f ) );

      Assert.IsFalse( XMath.AboutEqual( -0.001, -0.0, 0.001 ) );
      Assert.IsFalse( XMath.AboutEqual( -0.001f, -0.0f, 0.001f ) );

      Assert.IsFalse( XMath.AboutEqual( 1000L, 1001L, 1L ) );
      Assert.IsFalse( XMath.AboutEqual( 1000, 1001, 1 ) );

      Assert.IsFalse( XMath.AboutEqual( -1000L, -1001L, 1L ) );
      Assert.IsFalse( XMath.AboutEqual( -1000, -1001, 1 ) );

      Assert.IsFalse( XMath.AboutEqual( -1L, 1L, 2L ) );


      // Throwing Epsilon Exception
      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0, -1000.001, double.Epsilon ); } );
      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0, -1000.001, 0 ); } );
      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0, -1000.001, -1 ); } );

      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0f, -1000.001f, float.Epsilon ); } );
      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0f, -1000.001f, 0f ); } );
      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000.0f, -1000.001f, -1f ); } );

      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000, -1000, 0 ); } );
      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000, -1000, -1 ); } );

      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000L, -1000L, 0L ); } );
      Assert.ThrowsExactly<ArgumentException>( ( ) => { XMath.AboutEqual( -1000L, -1000L, -1L ); } );

    }
  }
}
