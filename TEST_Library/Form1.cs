using dNetBm98;
using dNetBm98.IniLib;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST_Library
{
  public partial class Form1 : Form
  {
    public Form1( )
    {
      InitializeComponent( );
    }

    private enum TestEnum
    {
      Item0 = 0,
      Item1 = 1,
      Item2 = 2,
      Item3 = 3,
    }

    private class TestClass
    {
      public TestEnum ID { get; set; }
      public string Item = "";
      public TestClass( TestEnum id, string item )
      {
        ID = id;
        Item = item;
      }
    }

    private EnumLookup<TestEnum, TestClass> EL = new EnumLookup<TestEnum, TestClass>( );


    private void button1_Click( object sender, EventArgs e )
    {
      RTB.Text += $"EL is empty\n";

      RTB.Text += $"EL.Count: {EL.Count}\n";
      bool b = EL.ContainsKey( TestEnum.Item0 );
      RTB.Text += $"EL contains Item0 => {b}\n";

      RTB.Text += $"foreach loop \n";
      foreach (var e1 in EL) {
        RTB.Text += $"Item found: {e1}\n";
      }

      EL.Add( TestEnum.Item0, new TestClass( TestEnum.Item0, "Item0" ) );
      EL.Add( TestEnum.Item1, new TestClass( TestEnum.Item1, "Item1" ) );
      EL.Add( TestEnum.Item2, new TestClass( TestEnum.Item2, "Item2" ) );
      EL.Add( TestEnum.Item3, new TestClass( TestEnum.Item3, "Item3" ) );

      RTB.Text += $"\nEL is full\n";
      RTB.Text += $"EL.Count: {EL.Count}\n";
      b = EL.ContainsKey( TestEnum.Item0 );
      RTB.Text += $"EL contains Item0 => {b}\n";
      RTB.Text += $"foreach loop \n";
      foreach (var e1 in EL) {
        RTB.Text += $"Item found: {e1.ID}\n";
      }

      RTB.Text += $"\nEL is cleared\n";
      EL.Clear( );
      RTB.Text += $"EL.Count: {EL.Count}\n";

    }

    private void lblFocus_MouseEnter( object sender, EventArgs e )
    {
      WinUser.ForceForegroundWindow( this.Handle );
      this.TopMost = true;
      lblFocus.ForeColor = Color.Red;
    }

    private void lblFocus_MouseLeave( object sender, EventArgs e )
    {
      lblFocus.ForeColor = Color.Black;
    }

    private void Form1_Load( object sender, EventArgs e )
    {
      lblDPI.Text = $"DPI: {this.DeviceDpi}";
    }

    private void btCalcTAS_Click( object sender, EventArgs e )
    {
      double cas = double.Parse( txCAS.Text );
      double alt = double.Parse( txALT.Text );
      double tas = Units.TAS_From_CAS( cas, alt );
      txTAS.Text = $"{tas:##0.00}";
    }

    private void btReadEnc_Click( object sender, EventArgs e )
    {
      string tFile = @".\Test-iso_8859_1.ini";

      var MSINI = new MSiniFile( );
      MSINI.Load( tFile, MSiniFile.IniEncoding.iso_8859_1 );
      RTB.Text = MSINI.ToString( );
    }

    private void btWriteEnc_Click( object sender, EventArgs e )
    {
      string tFile = @".\Test-iso_8859_1.ini";
      var MSINI = new MSiniFile( );
      MSINI.Load( tFile, MSiniFile.IniEncoding.iso_8859_1 );

      tFile = @".\Test-iso_8859_1.ini.written";
      if (File.Exists( tFile )) { File.Delete( tFile ); }

      MSINI.SetFilename( tFile );
      MSINI.WriteFile( );

      RTB.Text = $"{tFile} written";

    }
  }
}
