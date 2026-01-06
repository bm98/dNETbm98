using dNetBm98;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Text;

namespace NTEST_dNETbm98
{
  /// <summary>
  /// Testin the Octal class
  /// </summary>
  [TestClass]
  public class T_Octal
  {
    public T_Octal( )
    {
      //
      // TODO: Add constructor logic here
      //
    }

    /*
    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext {
      get {
        return testContextInstance;
      }
      set {
        testContextInstance = value;
      }
    }
    */


    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestMethod]
    public void BasicTests( )
    {
      // create upper lower limits
      Octal octTest = new Octal( 1 );
      octTest = new Octal( 10 );

      // testing with 4 digits now
      octTest = new Octal( 4 );

      // Test Set boundaries Octal
      octTest.SetOct( 0 );
      Assert.AreEqual( 0, octTest.GetOct( ) );
      Assert.AreEqual( "0", octTest.ToString( ) );
      Assert.AreEqual( 0, octTest.GetDec( ) );

      octTest.SetOct( 7777 );
      Assert.AreEqual( 7777, octTest.GetOct( ) );
      Assert.AreEqual( "7777", octTest.ToString( ) );
      Assert.AreEqual( 4095, octTest.GetDec( ) );

      // Test Set any
      octTest.SetOct( 3521 );
      Assert.AreEqual( 3521, octTest.GetOct( ) );
      Assert.AreEqual( "3521", octTest.ToString( ) );
      Assert.AreEqual( 1873, octTest.GetDec( ) );

      // Test Set boundaries Decimal
      octTest.SetDec( 0 );
      Assert.AreEqual( 0, octTest.GetOct( ) );

      octTest.SetDec( 4095 );
      Assert.AreEqual( 7777, octTest.GetOct( ) );

      // Test Set any
      octTest.SetDec( 1873 );
      Assert.AreEqual( 3521, octTest.GetOct( ) );


      // Test IncDec within
      octTest.SetOct( 3521 );
      octTest.Inc( );
      Assert.AreEqual( 3522, octTest.GetOct( ) );
      octTest.Dec( );
      Assert.AreEqual( 3521, octTest.GetOct( ) );

      // Test IncDec at boundaries
      octTest.SetOct( 7777 );
      octTest.Inc( );
      Assert.AreEqual( 7777, octTest.GetOct( ) );

      octTest.SetOct( 0 );
      octTest.Dec( );
      Assert.AreEqual( 0, octTest.GetOct( ) );

      // Test IncDecT at boundaries
      octTest.SetOct( 7777 );
      octTest.IncT( );
      Assert.AreEqual( 0, octTest.GetOct( ) );

      octTest.SetOct( 0 );
      octTest.DecT( );
      Assert.AreEqual( 7777, octTest.GetOct( ) );

      // Test AddDec Oct
      octTest.SetOct( 3521 );
      octTest.AddDec( 8 );
      Assert.AreEqual( 3531, octTest.GetOct( ) );

      octTest.SetOct( 3521 );
      octTest.AddOct( 10 );
      Assert.AreEqual( 3531, octTest.GetOct( ) );

      // Test SubDec Oct
      octTest.SetOct( 3521 );
      octTest.SubtractDec( 8 );
      Assert.AreEqual( 3511, octTest.GetOct( ) );

      octTest.SetOct( 3521 );
      octTest.SubtractOct( 10 );
      Assert.AreEqual( 3511, octTest.GetOct( ) );
    }


    [TestMethod]
    public void BasicErrorTests( )
    {

      Assert.ThrowsExactly<ArgumentException>( ( ) => {
        Octal octTestUnder = new Octal( -1 );
      } );

      Assert.ThrowsExactly<ArgumentException>( ( ) => {
        Octal octTestOver = new Octal( 11 );
      } );

      // testing with 4 digits now
      var octTest = new Octal( 4 );

      Assert.ThrowsExactly<ArgumentException>( ( ) => { octTest.SetOct( -1 ); } );
      Assert.ThrowsExactly<ArgumentException>( ( ) => { octTest.SetOct( 8 ); } );

      Assert.ThrowsExactly<ArgumentException>( ( ) => { octTest.SetOct( 1284 ); } );

      Assert.ThrowsExactly<ArgumentException>( ( ) => { octTest.SetDec( -1 ); } );
      Assert.ThrowsExactly<ArgumentException>( ( ) => { octTest.SetDec( 4096 ); } ); // 1_0000 Oct

    }



  }
}
