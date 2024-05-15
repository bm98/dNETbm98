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

using dNetBm98;
using dNetBm98.Job;
using dNetBm98.Win;
using dNetBm98.IniLib;
using System.Threading;


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


    private WinKbdSender KBD;
    private void btSendKeyA_Click( object sender, EventArgs e )
    {
      KBD?.Dispose( );

      KBD = new WinKbdSender( );

      KBD.AddStroke( new WinKbdSender.KbdStroke( Keys.A, 5000 ) );
      KBD.AddStroke( new WinKbdSender.KbdStroke( Keys.B, 100 ) );


      RTB.Text += $"Send AB - RunStrokes starts with blocking: {cbxBlocking.Checked}\n";
      var res = KBD.RunStrokes( "new 1", cbxBlocking.Checked );
      RTB.Text += "RunStrokes returned\n";

      if (!res) {
        RTB.Text += "RunStrokes Failed\n";
      }
    }

    private void btSendKeyCV_Click( object sender, EventArgs e )
    {
      KBD?.Dispose( );

      KBD = new WinKbdSender( );
      var stroke = new WinKbdSender.KbdStroke( Keys.V, 100 );
      stroke.AddModifier( Keys.LControlKey );
      KBD.AddStroke( stroke );

      RTB.Text += $"Send AB - RunStrokes starts with blocking: {cbxBlocking.Checked}\n";
      var res = KBD.RunStrokes( "new 1", cbxBlocking.Checked );
      RTB.Text += "RunStrokes returned\n";

      if (!res) {
        RTB.Text += "RunStrokes Failed\n";
      }
    }

    private JobRunner JR = null;
    private WinFormInvoker _invoker;
    private void JOB( )
    {
      _invoker.HandleEvent( ( ) => { RTB.Text += $"Job Done on {DateTime.Now:O}\n"; } );

    }

    private void btJobRunner_Click( object sender, EventArgs e )
    {
      if (JR == null) JR = new JobRunner( );
      if (_invoker == null) _invoker = new WinFormInvoker( RTB );



      JR.AddJob( new JobObj( JOB, $"Job {DateTime.Now:O}" ) );
      JR.AddJob( new JobObj( JOB, $"Job {DateTime.Now:O}" ) );
      JR.AddJob( new JobObj( JOB, $"Job {DateTime.Now:O}" ) );
      JR.AddJob( new JobObj( JOB, $"Job {DateTime.Now:O}" ) );
      //      Thread.Sleep( 5000 );
      JR.AddJob( new JobObj( JOB, $"Job {DateTime.Now:O}" ) );
      JR.AddJob( new JobObj( JOB, $"Job {DateTime.Now:O}" ) );
      JR.AddJob( new JobObj( JOB, $"Job {DateTime.Now:O}" ) );

    }
  }
}
