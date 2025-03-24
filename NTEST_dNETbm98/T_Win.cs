using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;
using System.Windows.Forms;

using dNetBm98.Win;
using static dNetBm98.Win.WinKbdSender;
using static dNetBm98.Utilities;


namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_Win
  {
    [TestMethod]
    public void TestWinKbdSender( )
    {
      // Test Serializing of KbdStroke

      var s1 = new KbdStroke( Keys.A, 100 );

      var serialized = s1.ToString( );
      var sRes = new KbdStroke( serialized );

      Assert.AreEqual( s1.Key, sRes.Key );
      Assert.AreEqual( s1.Duration_ms, sRes.Duration_ms );
      Assert.AreEqual( s1.Modifiers.Count( ), sRes.Modifiers.Count( ) );
      int i = 0;
      foreach (var modifier in s1.Modifiers) {
        Assert.AreEqual( modifier, sRes.Modifiers.ElementAt( i++ ) );
      }

      // with modifiers
      s1 = new KbdStroke( Keys.A, 100, new Keys[] { Keys.LMenu, Keys.LShiftKey, Keys.LControlKey } );

      serialized = s1.ToString( );
      sRes = new KbdStroke( serialized );

      Assert.AreEqual( s1.Key, sRes.Key );
      Assert.AreEqual( s1.Duration_ms, sRes.Duration_ms );
      Assert.AreEqual( s1.Modifiers.Count( ), sRes.Modifiers.Count( ) );
      i = 0;
      foreach (var modifier in s1.Modifiers) {
        Assert.AreEqual( modifier, sRes.Modifiers.ElementAt( i++ ) );
      }
    }


    // Valid filenames must return the same as given
    [TestMethod]
    public void Test_MakeFilename( )
    {
      string fn = "Bla.bla";
      Assert.IsTrue( IsValidFileName( fn ) );
      Assert.AreEqual( fn, MakeValidFileName( fn ) );

      fn = @"C:\path\Bla.bla";
      Assert.IsTrue( IsValidFileName( fn ) );
      Assert.AreEqual( fn, MakeValidFileName( fn ) );

      fn = @".\Bla.bla";
      Assert.IsTrue( IsValidFileName( fn ) );
      Assert.AreEqual( fn, MakeValidFileName( fn ) );

      fn = @"..\.\Bla.bla";
      Assert.IsTrue( IsValidFileName( fn ) );
      Assert.AreEqual( fn, MakeValidFileName( fn ) );

      fn = @"Bla.veryLongExtension";
      Assert.IsTrue( IsValidFileName( fn ) );
      Assert.AreEqual( fn, MakeValidFileName( fn ) );

      fn = @"Bla.double.DOT";
      Assert.IsTrue( IsValidFileName( fn ) );
      Assert.AreEqual( fn, MakeValidFileName( fn ) );

      fn = @"C:\path\Bla.double.DOT";
      Assert.IsTrue( IsValidFileName( fn ) );
      Assert.AreEqual( fn, MakeValidFileName( fn ) );

      fn = @"\\ServerName\ServerDir\path\Bla.double.DOT";
      Assert.IsTrue( IsValidFileName( fn ) );
      Assert.AreEqual( fn, MakeValidFileName( fn ) );
    }

    // Invalid filenames must return a patched one
    // invalid chars with _, devices with an added $
    [TestMethod]
    public void Test_MakeFilename_Invalids( )
    {
      Assert.IsFalse( IsValidFileName( @"NUL" ) );
      Assert.AreEqual( @"NUL$", MakeValidFileName( @"NUL" ) );

      Assert.IsFalse( IsValidFileName( @"aux" ) );
      Assert.AreEqual( @"aux$", MakeValidFileName( @"aux" ) );

      Assert.IsFalse( IsValidFileName( @"CoM" ) );
      Assert.AreEqual( @"CoM$", MakeValidFileName( @"CoM" ) );

      Assert.IsFalse( IsValidFileName( @"COM1" ) );
      Assert.AreEqual( @"COM1$", MakeValidFileName( @"COM1" ) );

      Assert.AreEqual( @"COM9$", MakeValidFileName( @"COM9" ) );
      Assert.AreEqual( @"LPT$", MakeValidFileName( @"LPT" ) );
      Assert.AreEqual( @"LPT1$", MakeValidFileName( @"LPT1" ) );
      Assert.AreEqual( @"LPT9$", MakeValidFileName( @"LPT9" ) );

      Assert.IsFalse( IsValidFileName( @"CON" ) );
      Assert.AreEqual( @"CON$", MakeValidFileName( @"CON" ) );

      Assert.IsFalse( IsValidFileName( @"C:\Path\CON.ext" ) );
      Assert.AreEqual( @"C:\Path\CON$.ext", MakeValidFileName( @"C:\Path\CON.ext" ) );

      Assert.IsFalse( IsValidFileName( @"C:\Path\Invalid*Name.ext" ) );
      Assert.AreEqual( @"C:\Path\Invalid_Name.ext", MakeValidFileName( @"C:\Path\Invalid*Name.ext" ) );

      Assert.IsFalse( IsValidFileName( @"C:\Path\Valid$Name.Invalid*ext" ) );
      Assert.AreEqual( @"C:\Path\Valid$Name.Invalid_ext", MakeValidFileName( @"C:\Path\Valid$Name.Invalid*ext" ) );

    }

  }
}
