using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Threading;

using dNetBm98.Timers;

namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_Timer
  {
    // VERY trivial way to do basic timer testing...
    // but it is good enough to make sure it works as expected


    // allowed delta of the timers in Ticks (100ns/tick)
    const long c_DeltaTicks = 20_000_0; // = 20ms delta seems reasonable for the test durations

    DateTime _expected;
    DateTime _effective;
    bool _ended;

    #region Helpers

    // action on Elapsed
    private void ElapsedAction( )
    {
      _effective = DateTime.Now;
      _ended = true;
    }

    // collect the Elapsed data and allow the test proc to progress
    private void T_Elapsed( object sender, System.Timers.ElapsedEventArgs e )
    {
      _effective = e.SignalTime;
      _ended = true;
    }

    // setup the test - just Start right after
    private void Setup( TimeSpan duration )
    {
      _ended = false;
      _effective = DateTime.Now; // reset
      _expected = DateTime.Now + duration;
    }

    // sleeps at 50ms intervals and checks for _ended
    private bool WaitUntilDone( int timerSec )
    {
      // wait until the event has fired.. or a timeout (5x TestTime) 
      int counter = timerSec * 20 * 5; // timeout
      while ((!_ended) && (counter-- > 0)) {
        Thread.Sleep( 50 );
      }
      return _ended;
    }

    #endregion

    [TestMethod]
    public void TestSimpleTimer( )
    {
      int testTime_sec = 1;
      var t = new SimpleTimer( testTime_sec );
      t.Elapsed += T_Elapsed;

      // setup and start the timer with a duration
      Setup( new TimeSpan( 0, 0, testTime_sec ) );
      t.Reset( );

      WaitUntilDone( testTime_sec );

      // check the outcome
      Assert.AreEqual( true, _ended );
      Assert.AreEqual( _expected.Ticks, _effective.Ticks, c_DeltaTicks );
    }

    [TestMethod]
    public void TestSimpleTimer500ms( )
    {
      double testTime_sec = 0.5;
      var t = new SimpleTimer( testTime_sec );
      t.Elapsed += T_Elapsed;

      // setup and start the timer with a duration
      Setup( new TimeSpan( 0, 0, 0, 0, (int)(testTime_sec * 1000) ) );
      t.Reset( );

      WaitUntilDone( 1 );

      // check the outcome
      Assert.AreEqual( true, _ended );
      Assert.AreEqual( _expected.Ticks, _effective.Ticks, c_DeltaTicks );
    }

    [TestMethod]
    public void TestSimpleTimer_10sec( )
    {
      int testTime_sec = 10;
      var t = new SimpleTimer( testTime_sec );
      t.Elapsed += T_Elapsed;

      // setup and start the timer with a duration
      Setup( new TimeSpan( 0, 0, testTime_sec ) );
      t.Reset( );

      WaitUntilDone( testTime_sec );

      // check the outcome
      Assert.AreEqual( true, _ended );
      Assert.AreEqual( _expected.Ticks, _effective.Ticks, c_DeltaTicks );
    }

    [TestMethod]
    public void TestSimpleTimerWithReset( )
    {
      int testTime_sec = 1;
      var t = new SimpleTimer( testTime_sec );
      t.Elapsed += T_Elapsed;

      // after 2x500 ms reset expect it to end with an additional second
      testTime_sec += 1;

      // setup and start the timer with a final DateTime
      Setup( new TimeSpan( 0, 0, testTime_sec ) );
      t.Reset( );
      Thread.Sleep( 499 ); // adds to imprecise outcomes

      t.Reset( );
      Thread.Sleep( 499 ); // adds to imprecise outcomes

      t.Reset( );
      WaitUntilDone( testTime_sec );
      // check the outcome
      Assert.AreEqual( true, _ended );
      Assert.AreEqual( _expected.Ticks, _effective.Ticks, c_DeltaTicks );
    }

    [TestMethod]
    public void TestActionTimer( )
    {
      int testTime_sec = 1;
      var t = new ActionTimer( testTime_sec, ElapsedAction );
      //t.Elapsed += T_Elapsed; // don't connect the event

      // setup and start the timer with a duration
      Setup( new TimeSpan( 0, 0, testTime_sec ) );
      t.Reset( );

      WaitUntilDone( testTime_sec );
      // check the outcome
      Assert.AreEqual( true, _ended );
      Assert.AreEqual( _expected.Ticks, _effective.Ticks, c_DeltaTicks );
    }

    [TestMethod]
    public void TestClockedTimer( )
    {
      int testTime_sec = 1;
      var t = new ClockedTimer( );
      t.Elapsed += T_Elapsed;

      // setup and start the timer with a final DateTime
      Setup( new TimeSpan( 0, 0, testTime_sec ) );
      t.Reset( _expected );

      WaitUntilDone( testTime_sec );
      // check the outcome
      Assert.AreEqual( true, _ended );
      Assert.AreEqual( _expected.Ticks, _effective.Ticks, c_DeltaTicks );
    }

    [TestMethod]
    public void TestCompareTimerWithReset( )
    {
      int testTime_sec = 1;
      float changeTrigger = 1.0f;

      var t = new CompareTimer<float>( testTime_sec );
      t.Elapsed += T_Elapsed;

      // after 2x500 ms reset expect it to end with an additional second
      testTime_sec += 1;

      // setup and start the timer with a final DateTime
      Setup( new TimeSpan( 0, 0, testTime_sec ) );
      t.Reset( changeTrigger );
      Thread.Sleep( 499 ); // adds to imprecise outcomes

      changeTrigger += 1;
      t.Reset( changeTrigger );
      Thread.Sleep( 499 ); // adds to imprecise outcomes

      changeTrigger -= 1;
      t.Reset( changeTrigger );
      WaitUntilDone( testTime_sec );
      // check the outcome
      Assert.AreEqual( true, _ended );
      Assert.AreEqual( _expected.Ticks, _effective.Ticks, c_DeltaTicks );
    }



  }
}
