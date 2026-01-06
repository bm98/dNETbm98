using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

using System;

using dNetBm98;
using dNetBm98.CsvLib;

namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_CsvLib
  {
    private string _testFile = @".\UNITTEST.csv";

    private List<string> csv1 = new List<string>( ) {
      "C1;C2;C3",
      "1;2;3",
      "11;12;13",
    };
    // separator in quoted content
    private List<string> csv2 = new List<string>( ) {
      "C1;C2;C3",
      "\"1;1\";\"1;2\";\"1;3\"",
    };
    // custom separator |
    private List<string> csv3 = new List<string>( ) {
      "C1|C2|C3",
      "1|2|3",
    };


    private void CreateTestfile( List<string> csv )
    {
      DeleteTestfile( );
      using (var sw = new StreamWriter( File.OpenWrite( _testFile ) )) {
        foreach (var item in csv) sw.WriteLine( item );
      }
    }
    private void DeleteTestfile( )
    {
      if (File.Exists( _testFile )) File.Delete( _testFile );
    }

    [TestMethod]
    public void BasicTests( )
    {
      // plain csv with locale separator (CH=;)
      CreateTestfile( csv1 );

      var cx = new CsvFile( _testFile );
      Assert.IsNotNull( cx );
      Assert.HasCount( csv1.Count, cx.CsvContainer );
      Assert.AreEqual( 3, cx.CsvContainer.NumColumns );

      Assert.AreEqual( csv1[0], cx.CsvContainer[0].Line );
      Assert.AreEqual( csv1[1], cx.CsvContainer[1].Line );
      Assert.AreEqual( csv1[2], cx.CsvContainer[2].Line );

      Assert.AreEqual( "C1", cx.CsvContainer[0][0] );
      Assert.AreEqual( "C2", cx.CsvContainer[0][1] );
      Assert.AreEqual( "C3", cx.CsvContainer[0][2] );

      Assert.AreEqual( "1", cx.CsvContainer[1][0] );
      Assert.AreEqual( "2", cx.CsvContainer[1][1] );
      Assert.AreEqual( "3", cx.CsvContainer[1][2] );

      Assert.AreEqual( "11", cx.CsvContainer[2][0] );
      Assert.AreEqual( "12", cx.CsvContainer[2][1] );
      Assert.AreEqual( "13", cx.CsvContainer[2][2] );

      DeleteTestfile( );
    }

    [TestMethod]
    public void QuotedSeparatorTests( )
    {
      // having a separator in quoted content
      CreateTestfile( csv2 );

      var cx = new CsvFile( _testFile );
      Assert.IsNotNull( cx );
      Assert.HasCount( csv2.Count, cx.CsvContainer );
      Assert.AreEqual( 3, cx.CsvContainer.NumColumns );

      Assert.AreEqual( csv2[0], cx.CsvContainer[0].Line );
      Assert.AreEqual( csv2[1], cx.CsvContainer[1].Line );

      Assert.AreEqual( "C1", cx.CsvContainer[0][0] );
      Assert.AreEqual( "C2", cx.CsvContainer[0][1] );
      Assert.AreEqual( "C3", cx.CsvContainer[0][2] );

      Assert.AreEqual( "\"1;1\"", cx.CsvContainer[1][0] );
      Assert.AreEqual( "\"1;2\"", cx.CsvContainer[1][1] );
      Assert.AreEqual( "\"1;3\"", cx.CsvContainer[1][2] );

      DeleteTestfile( );
    }

    [TestMethod]
    public void SeparatorTests( )
    {
      // use a custom separator
      CreateTestfile( csv3 );

      var cx = new CsvFile( _testFile, separator: '|' );
      Assert.IsNotNull( cx );
      Assert.HasCount( csv3.Count, cx.CsvContainer );
      Assert.AreEqual( 3, cx.CsvContainer.NumColumns );

      Assert.AreEqual( csv3[0], cx.CsvContainer[0].Line );
      Assert.AreEqual( csv3[1], cx.CsvContainer[1].Line );

      Assert.AreEqual( "C1", cx.CsvContainer[0][0] );
      Assert.AreEqual( "C2", cx.CsvContainer[0][1] );
      Assert.AreEqual( "C3", cx.CsvContainer[0][2] );

      Assert.AreEqual( "1", cx.CsvContainer[1][0] );
      Assert.AreEqual( "2", cx.CsvContainer[1][1] );
      Assert.AreEqual( "3", cx.CsvContainer[1][2] );

      DeleteTestfile( );
    }

  }
}
