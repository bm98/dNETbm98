using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dNetBm98.IniLib;
using System.IO;
using static System.Collections.Specialized.BitVector32;

namespace NTEST_dNETbm98
{
  [TestClass]
  public class T_IniLib
  {
    private static string _testFile = @".\UNITTEST.ini";

    private void CreateTestfile_UTF8( string iniContent )
    {
      DeleteTestfile( );
      using (var sw = new StreamWriter( File.OpenWrite( _testFile ), Encoding.UTF8 )) {
        sw.Write( iniContent );
      }
    }
    private void DeleteTestfile( )
    {
      if (File.Exists( _testFile )) File.Delete( _testFile );
    }

    private MSiniFile GetIniFile_UTF8( bool handleQuotes )
    {
      var ini = new MSiniFile( );
      ini.SetUnqoteValues( handleQuotes );
      ini.Load( _testFile, MSiniFile.IniEncoding.UTF8 );

      return ini;
    }



    [TestMethod]
    public void BasicTests( )
    {
      // plain ini
      CreateTestfile_UTF8( IniTest.IniTestFile( ) );

      var ini = GetIniFile_UTF8( false );

      Assert.IsNotNull( ini );
      Assert.IsTrue( ini.IsValid );
      Assert.AreEqual( 3, ini.SectionCatalog.Count );

      var s = ini.GetSection( "" ); // List of main items
      Assert.HasCount( 3, s );
      var sect = ini.SectionCatalog.GetSection( "" );
      Assert.AreEqual( 3, sect.Items.Count );
      // uses raw data access via Section and Item objects
      var item = sect.Items.GetItem( "M_K1" ).Value; Assert.AreEqual( "Main Section String", item );
      item = sect.Items.GetItem( "M_K2" ).Value; Assert.AreEqual( "123", item );
      item = sect.Items.GetItem( "M_K3" ).Value; Assert.AreEqual( "123.456", item );

      Assert.AreEqual( "Section1", ini.SectionCatalog.Sections.ElementAt( 1 ).Name );
      Assert.AreEqual( "Section2", ini.SectionCatalog.Sections.ElementAt( 2 ).Name );

      s = ini.GetSection( ini.SectionCatalog.Sections.ElementAt( 1 ).Name ); // S1
      Assert.HasCount( 3, s );
      sect = ini.SectionCatalog.GetSection( "Section1" );
      Assert.AreEqual( 3, sect.Items.Count );

      item = sect.Items.GetItem( "S1_K1" ).Value; Assert.AreEqual( "Section 1 String", item );
      item = sect.Items.GetItem( "S1_K2" ).Value; Assert.AreEqual( "12345", item );
      item = sect.Items.GetItem( "S1_K3" ).Value; Assert.AreEqual( "12345.456", item );

      s = ini.GetSection( ini.SectionCatalog.Sections.ElementAt( 2 ).Name ); // S2
      Assert.HasCount( 3 + 4, s ); // Dicts are not supported in plain ini file
      sect = ini.SectionCatalog.GetSection( "Section2" );
      Assert.AreEqual( 3 + 4, sect.Items.Count );

      item = sect.Items.GetItem( "S2_K1" ).Value; Assert.AreEqual( "Section 2 String", item );
      item = sect.Items.GetItem( "S2_K2" ).Value; Assert.AreEqual( "1234567", item );
      item = sect.Items.GetItem( "S2_K3" ).Value; Assert.AreEqual( "1234567.456", item );

      item = sect.Items.GetItem( "S2_K4.0" ).Value; Assert.AreEqual( "Entry 0", item );
      item = sect.Items.GetItem( "S2_K4.1" ).Value; Assert.AreEqual( "Entry 1", item );
      item = sect.Items.GetItem( "S2_K4.2" ).Value; Assert.AreEqual( "Entry 2", item );
      item = sect.Items.GetItem( "S2_K4.3" ).Value; Assert.AreEqual( "Entry 3", item );
      // does not exist
      item = sect.Items.GetItem( "S2_K4.4" )?.Value; Assert.IsNull( item );

      DeleteTestfile( );
    }

    [TestMethod]
    public void BasicTestsQuotedStrings( )
    {
      // plain ini with quoted content
      CreateTestfile_UTF8( IniTest.IniTestFileQuoted( ) );

      var ini = GetIniFile_UTF8( true ); // unquote

      Assert.IsNotNull( ini );
      Assert.IsTrue( ini.IsValid );
      Assert.AreEqual( 3, ini.SectionCatalog.Count );

      var s = ini.GetSection( "" ); // List of main items
      Assert.HasCount( 3, s );
      var sect = ini.SectionCatalog.GetSection( "" );
      Assert.AreEqual( 3, sect.Items.Count );
      // uses primary data access via Sect and ItemKey
      var item = ini.ItemValue( "", "M_K1" ); Assert.AreEqual( "Main Section String", item );
      item = ini.ItemValue( "", "M_K2" ); Assert.AreEqual( "123", item );
      item = ini.ItemValue( "", "M_K3" ); Assert.AreEqual( "123.456", item );

      Assert.AreEqual( "Section1", ini.SectionCatalog.Sections.ElementAt( 1 ).Name );
      Assert.AreEqual( "Section2", ini.SectionCatalog.Sections.ElementAt( 2 ).Name );

      s = ini.GetSection( ini.SectionCatalog.Sections.ElementAt( 1 ).Name ); // S1
      Assert.HasCount( 3, s );
      sect = ini.SectionCatalog.GetSection( "Section1" );
      Assert.AreEqual( 3, sect.Items.Count );

      item = ini.ItemValue( "Section1", "S1_K1" ); Assert.AreEqual( "Section 1 String", item );
      item = ini.ItemValue( "Section1", "S1_K2" ); Assert.AreEqual( "12345", item );
      item = ini.ItemValue( "Section1", "S1_K3" ); Assert.AreEqual( "12345.456", item );

      s = ini.GetSection( ini.SectionCatalog.Sections.ElementAt( 2 ).Name ); // S2
      Assert.HasCount( 3 + 4, s ); // Dicts are not supported in plain ini file
      sect = ini.SectionCatalog.GetSection( "Section2" );
      Assert.AreEqual( 3 + 4, sect.Items.Count );

      item = ini.ItemValue( "Section2", "S2_K1" ); Assert.AreEqual( "Section 2 String", item );
      item = ini.ItemValue( "Section2", "S2_K2" ); Assert.AreEqual( "1234567", item );
      item = ini.ItemValue( "Section2", "S2_K3" ); Assert.AreEqual( "1234567.456", item );

      item = ini.ItemValue( "Section2", "S2_K4.0" ); Assert.AreEqual( "Entry 0", item );
      item = ini.ItemValue( "Section2", "S2_K4.1" ); Assert.AreEqual( "Entry 1", item );
      item = ini.ItemValue( "Section2", "S2_K4.2" ); Assert.AreEqual( "Entry 2", item );
      item = ini.ItemValue( "Section2", "S2_K4.3" ); Assert.AreEqual( "Entry 3", item );
      // does not exist
      item = ini.ItemValue( "Section2", "S2_K4.4" ); Assert.AreEqual( "", item );

      DeleteTestfile( );
    }

    [TestMethod]
    public void DeSerializeTests( )
    {
      // plain ini
      CreateTestfile_UTF8( IniTest.IniTestFile( ) );

      var obj = IniSerializer.FromIniFile<IniTestMain>( _testFile, MSiniFile.IniEncoding.UTF8 );
      Assert.IsNotNull( obj );

      Assert.AreEqual( "Main Section String", obj.K1_string );
      Assert.AreEqual( 123, obj.K2_int );
      Assert.AreEqual( 123.456f, obj.K3_float );

      Assert.IsNotNull( obj.S1 );
      Assert.AreEqual( "Section 1 String", obj.S1.K1_string );
      Assert.AreEqual( 12345, obj.S1.K2_int );
      Assert.AreEqual( 12345.456, obj.S1.K3_double );

      Assert.IsNotNull( obj.S2 );
      Assert.AreEqual( "Section 2 String", obj.S2.K1_string );
      Assert.AreEqual( 1234567, obj.S2.K2_int );
      Assert.AreEqual( 1234567.456, obj.S2.K3_double );

      Assert.IsNotNull( obj.S2.K4_dict );
      Assert.AreEqual( "Entry 0", obj.S2.K4_dict["S2_K4.0"] );
      Assert.AreEqual( "Entry 1", obj.S2.K4_dict["S2_K4.1"] );
      Assert.AreEqual( "Entry 2", obj.S2.K4_dict["S2_K4.2"] );
      Assert.AreEqual( "Entry 3", obj.S2.K4_dict["S2_K4.3"] );

      DeleteTestfile( );
    }

    [TestMethod]
    public void SerializeTests( )
    {
      DeleteTestfile( );

      // create a file from the Obj
      var doc = IniTest.IniDoc1( );
      IniSerializer.ToIniFile( _testFile, Encoding.UTF8, doc );

      // and read it back as above in the Deserialize Test
      var obj = IniSerializer.FromIniFile<IniTestMain>( _testFile, MSiniFile.IniEncoding.UTF8 );
      Assert.IsNotNull( obj );

      Assert.AreEqual( "Main Section String", obj.K1_string );
      Assert.AreEqual( 123, obj.K2_int );
      Assert.AreEqual( 123.456f, obj.K3_float );

      Assert.IsNotNull( obj.S1 );
      Assert.AreEqual( "Section 1 String", obj.S1.K1_string );
      Assert.AreEqual( 12345, obj.S1.K2_int );
      Assert.AreEqual( 12345.456, obj.S1.K3_double, 0.001 );

      Assert.IsNotNull( obj.S2 );
      Assert.AreEqual( "Section 2 String", obj.S2.K1_string );
      Assert.AreEqual( 1234567, obj.S2.K2_int );
      Assert.AreEqual( 1234567.456, obj.S2.K3_double, 0.001 );

      Assert.IsNotNull( obj.S2.K4_dict );
      Assert.AreEqual( "Entry 0", obj.S2.K4_dict["S2_K4.0"] );
      Assert.AreEqual( "Entry 1", obj.S2.K4_dict["S2_K4.1"] );
      Assert.AreEqual( "Entry 2", obj.S2.K4_dict["S2_K4.2"] );
      Assert.AreEqual( "Entry 3", obj.S2.K4_dict["S2_K4.3"] );

      DeleteTestfile( );
    }

  }
}
