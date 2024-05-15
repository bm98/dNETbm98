using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;

using dNetBm98.Win;
using static dNetBm98.Win.WinKbdSender;
using System.Windows.Forms;


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




  }
}
