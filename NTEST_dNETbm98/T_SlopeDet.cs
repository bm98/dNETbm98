using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using dNetBm98;

namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_SlopeDet
  {
    [TestMethod]
    public void TestFloat( )
    {
      SlopeDetector<float> _sDet = new SlopeDetector<float>( Slope.BiDirectional, 100f, 0, null );

      // Bidirectional
      _sDet.Update( 10 );
      Assert.AreEqual( false, _sDet.Read( ) );
      _sDet.Update( 99.9999f );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from below
      _sDet.Update( 100f );
      Assert.AreEqual( true, _sDet.Read( ) );
      Assert.AreEqual( false, _sDet.Read( ) ); // must be reset now

      _sDet.Update( 101f );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from above
      _sDet.Update( 100f );
      Assert.AreEqual( true, _sDet.Read( ) );
      Assert.AreEqual( false, _sDet.Read( ) ); // must be reset now

      // From Above
      _sDet.SetSlope( Slope.FromAbove );
      _sDet.OverrideValue( 0 );

      _sDet.Update( 10 );
      Assert.AreEqual( false, _sDet.Read( ) );
      _sDet.Update( 99.9999f );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from below
      _sDet.Update( 100f );
      Assert.AreEqual( false, _sDet.Read( ) );

      _sDet.Update( 101f );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from above
      _sDet.Update( 100f );
      Assert.AreEqual( true, _sDet.Read( ) );
      Assert.AreEqual( false, _sDet.Read( ) ); // must be reset now

      // From Below
      _sDet.SetSlope( Slope.FromBelow );
      _sDet.OverrideValue( 0 );

      _sDet.Update( 10 );
      Assert.AreEqual( false, _sDet.Read( ) );
      _sDet.Update( 99.9999f );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from below
      _sDet.Update( 100f );
      Assert.AreEqual( true, _sDet.Read( ) );
      Assert.AreEqual( false, _sDet.Read( ) ); // must be reset now

      _sDet.Update( 101f );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from above
      _sDet.Update( 100f );
      Assert.AreEqual( false, _sDet.Read( ) );



    }

    [TestMethod]
    public void TestInt( )
    {
      SlopeDetector<int> _sDet = new SlopeDetector<int>( Slope.BiDirectional, 100, 0, null );

      // Bidirectional
      _sDet.Update( 10 );
      Assert.AreEqual( false, _sDet.Read( ) );
      _sDet.Update( 99 );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from below
      _sDet.Update( 100 );
      Assert.AreEqual( true, _sDet.Read( ) );
      Assert.AreEqual( false, _sDet.Read( ) ); // must be reset now

      _sDet.Update( 101 );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from above
      _sDet.Update( 100 );
      Assert.AreEqual( true, _sDet.Read( ) );
      Assert.AreEqual( false, _sDet.Read( ) ); // must be reset now

      // From Above
      _sDet.SetSlope( Slope.FromAbove );
      _sDet.OverrideValue( 0 );

      _sDet.Update( 10 );
      Assert.AreEqual( false, _sDet.Read( ) );
      _sDet.Update( 99 );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from below
      _sDet.Update( 100 );
      Assert.AreEqual( false, _sDet.Read( ) );

      _sDet.Update( 101 );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from above
      _sDet.Update( 100 );
      Assert.AreEqual( true, _sDet.Read( ) );
      Assert.AreEqual( false, _sDet.Read( ) ); // must be reset now

      // From Below
      _sDet.SetSlope( Slope.FromBelow );
      _sDet.OverrideValue( 0 );

      _sDet.Update( 10 );
      Assert.AreEqual( false, _sDet.Read( ) );
      _sDet.Update( 99 );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from below
      _sDet.Update( 100 );
      Assert.AreEqual( true, _sDet.Read( ) );
      Assert.AreEqual( false, _sDet.Read( ) ); // must be reset now

      _sDet.Update( 101 );
      Assert.AreEqual( false, _sDet.Read( ) );
      // from above
      _sDet.Update( 100 );
      Assert.AreEqual( false, _sDet.Read( ) );
    }


  }
}
