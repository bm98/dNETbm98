using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using dNetBm98.Metrics;
using System.Diagnostics;

namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_Metrics
  {

    // 100 values where 17 should win (twice)
    private void LoadHistogram_100_17( Histogram<int> histogram )
    {
      for (int i = 0; i < 100; i++) {
        histogram.Add( i );
      }
      histogram.Add( 17 );
    }

    // loading with random Ints and add a lot of 17 to win
    private void LoadHistogram_rnd_17( Histogram<int> histogram )
    {
      var rnd = new Random( 133 );

      for (int i = 0; i < 100; i++) {
        histogram.Add( rnd.Next( 0, 100 ) ); // add ints
      }
      // add 20 more ints to win
      for (int i = 0; i < 20; i++) {
        histogram.Add( 17 );
      }
    }

    // loading with many random Ints and add a lot of 17 to win
    private void LoadHistogramBig_rnd_17( Histogram<int> histogram )
    {
      var rnd = new Random( 133 );

      for (int i = 0; i < 100000; i++) {
        histogram.Add( rnd.Next( 0, 100 ) ); // add ints
      }
      // add 100 more ints
      for (int i = 0; i < 100; i++) {
        histogram.Add( 33 );
      }
      // add 100 more ints
      for (int i = 0; i < 100; i++) {
        histogram.Add( 54 );
      }
      // add 100 more ints
      for (int i = 0; i < 100; i++) {
        histogram.Add( 1 );
      }
      // add 100 more ints
      for (int i = 0; i < 100; i++) {
        histogram.Add( 99 );
      }

      // add 2000 more ints to win
      for (int i = 0; i < 20000; i++) {
        histogram.Add( 17 );
      }
    }



    [TestMethod]
    public void Test_Histogram_noLL( )
    {
      var h = new Histogram<int>( false ); // no linked list
      LoadHistogram_100_17( h );
      Assert.AreEqual( 17, h.Max( ) );

      h.Reset( );
      LoadHistogram_rnd_17( h );
      Assert.AreEqual( 17, h.Max( ) );

      h.Reset( );
      LoadHistogramBig_rnd_17( h );
      Assert.AreEqual( 17, h.Max( ) );


    }

    [TestMethod]
    public void Test_Histogram_LL( )
    {
      var hLL = new Histogram<int>( true ); // linked list
      LoadHistogram_100_17( hLL );
      Assert.AreEqual( 17, hLL.Max( ) );

      hLL.Reset( );
      LoadHistogram_rnd_17( hLL );
      Assert.AreEqual( 17, hLL.Max( ) );

      hLL.Reset( );
      LoadHistogramBig_rnd_17( hLL );
      Assert.AreEqual( 17, hLL.Max( ) );

    }

    [TestMethod]
    public void Test_Histogram_Timing( )
    {
      var timer = new Stopwatch( );

      var h = new Histogram<int>( false ); // no linked list
      timer.Start( );
      LoadHistogramBig_rnd_17( h );
      Assert.AreEqual( 17, h.Max( ) );
      timer.Stop( );
      Debug.WriteLine( $"NO LL: {timer.ElapsedMilliseconds:0.000} ms" );

      var hLL = new Histogram<int>( true ); // linked list
      timer.Start( );
      LoadHistogramBig_rnd_17( hLL );
      Assert.AreEqual( 17, hLL.Max( ) );
      timer.Stop( );
      Debug.WriteLine( $"   LL: {timer.ElapsedMilliseconds:0.000} ms" );


      // test with many readouts
      h.Reset( );
      timer.Start( );
      LoadHistogramBig_rnd_17( h );
      for ( int i = 0; i < 10000; i++) {
        Assert.AreEqual( 17, h.Max( ) );
      }
      timer.Stop( );
      Debug.WriteLine( $"NO LL maxRead: {timer.ElapsedMilliseconds:0.000} ms" );

      hLL.Reset( );
      timer.Start( );
      LoadHistogramBig_rnd_17( hLL );
      for (int i = 0; i < 10000; i++) {
        Assert.AreEqual( 17, hLL.Max( ) );
      }
      timer.Stop( );
      Debug.WriteLine( $"   LL maxRead: {timer.ElapsedMilliseconds:0.000} ms" );

    }


  }
}
