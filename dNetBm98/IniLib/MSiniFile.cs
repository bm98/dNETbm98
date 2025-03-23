using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using Windows.Media.AppBroadcasting;

namespace dNetBm98.IniLib
{
  /// <summary>
  /// Simple INI file reader/writer
  /// ini files are files composed from:
  /// sections as:
  /// [SECTION]
  /// lines as:
  /// ITEM=[CONTENT] [;COMMENT]  ([] items are optional)
  /// Content can be quoted "content bla" anything and will be returned as quoted part only
  /// Comments are lead by a ; (semicolon)
  /// lines may apear before, or within a section
  /// The SECTION name for the lines before any section starts use an empty section "" qualifier
  /// 
  /// Use:
  ///   string = ini.ItemValue("section", "item")
  ///   double = ini.ItemNumber("section", "item")
  ///   bool SetValue( "section", "item", "value" )
  ///   bool SetQuotedValue( "section", "item", "value" )
  ///  
  ///   // raw access to sections and items
  ///   List[string] = GetSection("section")
  ///   IEnumerable[Section] = ini.SectionCatalog.Sections
  ///   IniItem = section.Items.GetItem("item")
  ///   
  /// </summary>
  public class MSiniFile
  {
    /// <summary>
    /// Supported Encodings for INI files
    /// </summary>
    public enum IniEncoding
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
      UTF8,
      Unicode,
      ASCII,
      //ANSI, // not supported, too many dependencies to Net dlls
      iso_8859_1,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    // this filename
    private string _fileName = "";
    // sections of this file
    private SectionCat _sections = new SectionCat( );
    // encoding used for read/write of files
    private Encoding _encoding = Encoding.UTF8;

    private bool _valid = false;
    private bool _unquote = false;


    // Read the Inifile from the stream with given encoding
    private void LoadStreamLow( Stream stream, Encoding encoding )
    {
      _valid = false;
      using (var reader = new StreamReader( stream, encoding, true )) {
        // this shall never throw - we use IsValid to evaluate the outcome
        try {
          IniSection.GetMainSection( _sections, reader );
          bool result = IniSection.GetNextSection( _sections, reader );
          while (result) {
            result = IniSection.GetNextSection( _sections, reader );
          }
          //
          _valid = true; // only here we decide
        }
        catch (Exception ex) {
          Console.WriteLine( "MSiniFile: Cannot Read " + _fileName );
          Console.WriteLine( ex.Message );
          return; // ERROR - remains NOT Valid
        }
      }
    }

    // Set the File Encoding to the ISO-8859-1 codepage (Western European ISO)
    //  this is preferred for MSFS (it seems they are not on UTF8 for at least the FLT files)
    private void SetEncodingISO_8859_1( )
    {
      _encoding = Encoding.GetEncoding( "iso-8859-1" );
    }

    // Set the File Encoding to UTF8
    private void SetEncodingUTF8( )
    {
      _encoding = Encoding.UTF8;
    }

    // Set the File Encoding to UNICODE (UTF16 little endian byte order)
    private void SetEncodingUNICODE( )
    {
      _encoding = Encoding.Unicode;
    }

    // Set the File Encoding to ASCII
    private void SetEncodingASCII( )
    {
      _encoding = Encoding.ASCII;
    }

    // Set the File Encoding to ANSI 
    // DISABLED
    private void SetEncodingANSI( )
    {
      //_encoding = CodePagesEncodingProvider.Instance.GetEncoding( 1252 );
      _encoding = Encoding.ASCII;
    }

    /// <summary>
    /// Returns true if the file is valid
    /// </summary>
    public bool IsValid => _valid;


    /// <summary>
    /// Get; The Sections of this file
    /// </summary>
    public SectionCat SectionCatalog => _sections;


    /// <summary>
    /// cTor: empty
    /// </summary>
    public MSiniFile( ) { }

    /// <summary>
    /// cTor: Create an Ini File from a filename
    ///  Note: default encoding is UTF8 
    ///    if you need another encoding:
    ///    - create an empty object
    ///    - Load(file, encoding) 
    ///    
    ///   You may set an other encoding before Writing
    ///   
    /// </summary>
    /// <param name="fileName">Fully qualified path and name</param>
    public MSiniFile( string fileName )
    {
      this.Load( fileName );
    }

    /// <summary>
    /// cTor: Create an Ini File from a stream
    ///  Note: default encoding is UTF8 
    ///    if you need another encoding:
    ///    - create an empty object
    ///    - Load(stream, encoding) 
    ///    
    ///   You may set an other encoding before Writing
    ///   
    /// </summary>
    /// <param name="stream">An open stream</param>
    public MSiniFile( Stream stream )
    {
      this.Load( stream );
    }

    /// <summary>
    /// Set the File Encoding for writing
    /// </summary>
    public void SetEncoding( IniEncoding encoding )
    {
      switch (encoding) {
        case IniEncoding.UTF8: SetEncodingUTF8( ); break;
        case IniEncoding.Unicode: SetEncodingUNICODE( ); break;
        case IniEncoding.ASCII: SetEncodingASCII( ); break;
        //case IniEncoding.ANSI: SetEncodingANSI( ); break;
        case IniEncoding.iso_8859_1: SetEncodingISO_8859_1( ); break;
        default: SetEncodingUTF8( ); break;
      }
    }

    /// <summary>
    /// Set whether we read and Unquote values or not
    /// </summary>
    /// <param name="unqoting">True to unquote, else false</param>
    public void SetUnqoteValues( bool unqoting )
    {
      _unquote = unqoting;
    }



    /// <summary>
    /// Set a new filename for this INI file for writing
    /// </summary>
    /// <param name="fileName">A filename</param>
    public void SetFilename( string fileName )
    {
      _fileName = fileName;
    }


    /// <summary>
    /// Load from a stream using set encoding
    ///   (defaults to UTF8 if not set otherwise)
    /// </summary>
    /// <param name="stream">An open stream</param>
    public void Load( Stream stream )
    {
      _valid = false;
      _sections.Clear( );
      _fileName = "";
      if (stream.Length < 1) return; // ERROR too short...

      _fileName = "$$$STREAM$$$";
      LoadStreamLow( stream, _encoding ); // get all
    }

    /// <summary>
    /// Load a new file with optional encoding
    /// Sets the Encoding for the MSINI File
    /// 
    ///  Note: default encoding is iso-8859-1 (MSFS FLT encoding) 
    /// </summary>
    /// <param name="fileName">Fully qualified path and name</param>
    /// <param name="encoding">Optional encoding of the file</param>
    public void Load( string fileName, IniEncoding encoding = IniEncoding.iso_8859_1 )
    {
      _valid = false;
      _sections.Clear( );
      _fileName = "";
      if (!File.Exists( fileName )) return; // ERROR file does not exist..

      SetFilename( fileName );
      SetEncoding( encoding );

      // never fail
      try {
        string buf = "";
        using (var sr = new StreamReader( _fileName, _encoding, true )) {
          buf = sr.ReadToEnd( );
        }
        using (var stream = XString.StreamFromString( buf, Encoding.Unicode )) {
          LoadStreamLow( stream, Encoding.Unicode );
        }

        return;
      }
      catch { }
    }

    /// <summary>
    /// Write the INI file to the set filename with the set encoding
    ///  Note: default encoding is UTF8 
    ///    if you need another encoding:
    ///    - Set another encoding
    ///    - Write()
    /// </summary>
    public void WriteFile( )
    {
      if (!string.IsNullOrEmpty( Path.GetDirectoryName( _fileName ) ))
        if (!Directory.Exists( Path.GetDirectoryName( _fileName ) )) return; // ERROR no dir for this file

      // this shall never throw
      try {
        using (var sw = new StreamWriter( _fileName, false, _encoding )) {
          _sections.WriteAll( sw, _unquote );
        }
      }
      catch {
        ; // DEBUG
      }
    }

    /// <summary>
    /// Write the INI file to the stream with the set encoding
    ///  Note: default encoding is UTF8 
    ///    if you need another encoding:
    ///    - Set another encoding
    ///    - Write()
    /// </summary>
    /// <param name="stream">The stream to output to.</param>
    public void WriteStream( Stream stream )
    {
      // sanity
      if (stream == null) throw new ArgumentNullException( "stream" );

      // this shall never throw
      try {
        // don't close the stream after writing
        using (var sw = new StreamWriter( stream, _encoding, 1024, true )) {
          _sections.WriteAll( sw, _unquote );
          sw.Flush( );
        }
      }
      catch {
        ; // DEBUG
      }
    }

    /// <summary>
    /// Set the Value as double quoted String for an item
    ///   will create section and item if needed, else it overwrites
    /// </summary>
    /// <param name="sectionName">A section name (can be empty for the main section)</param>
    /// <param name="itemName">The item name (cannot be empty)</param>
    /// <param name="value">A value to set (can be empty)</param>
    public bool SetQuotedValue( string sectionName, string itemName, string value )
    {
      return SetValue( sectionName, itemName, $"\"{value}\"" );
    }

    /// <summary>
    /// Set the Value for an item
    ///   will create section and item if needed, else it overwrites
    /// </summary>
    /// <param name="sectionName">A section name (can be empty for the main section)</param>
    /// <param name="itemName">The item name (cannot be empty)</param>
    /// <param name="value">A value to set (can be empty)</param>
    public bool SetValue( string sectionName, string itemName, string value )
    {
      if (string.IsNullOrEmpty( itemName )) return false;

      var section = _sections.GetSection( sectionName );
      if (section == null) {
        section = new IniSection( sectionName );
        _sections.Add( section );
      }

      var item = section.Items.GetItem( itemName );
      if (item == null) {
        item = new IniItem( itemName, value );
        section.Items.Add( item );
      }
      else {
        item.SetValue( value ); // overwrite existing value
      }

      return true;
    }

    /// <summary>
    /// Returns all lines of a single section [section]
    ///  Item=Value ; Comment
    /// Note: the match is case insensitive
    /// </summary>
    /// <param name="sectionName">The section name</param>
    /// <returns>The list of lines belonging to the section (can be an empty list)</returns>
    public List<string> GetSection( string sectionName )
    {
      var section = _sections.GetSection( sectionName );
      if (section == null) return new List<string>( ); // empty one

      return section.Items.ItemList( );
    }

    /// <summary>
    /// Returns the value of an item 
    /// Note: the match is case insensitive
    /// </summary>
    /// <param name="sectionName">The section name</param>
    /// <param name="item">The item sought</param>
    /// <param name="unquote">On the fly unqoting</param>
    /// <returns>The item value (can be empty string)</returns>
    public string ItemValue( string sectionName, string item, bool unquote = false )
    {
      var section = _sections.GetSection( sectionName );
      if (section == null) return ""; // empty one

      var it = section.Items.GetItem( item );
      if (it == null) return ""; // empty one

      return (unquote || _unquote) ? Utilities.FromQuoted( it.Value ) : it.Value;
    }

    /// <summary>
    /// Returns a value as number for a key in a section
    ///  use the "" to access the ones not contained in a section
    /// Note: the match is case insensitive
    /// </summary>
    /// <param name="sectionName">The section name</param>
    /// <param name="item">The item sought</param>
    /// <returns>A number or double.MinValue if not found or not a number</returns>
    public double ItemNumber( string sectionName, string item )
    {
      string value = ItemValue( sectionName, item, true ); // get unquoted
      if (string.IsNullOrWhiteSpace( value )) return double.MinValue;
      if (double.TryParse( value, NumberStyles.Float, CultureInfo.InvariantCulture, out double num )) {
        return num;
      }
      return double.MinValue;
    }

    /// <summary>
    /// Returns the INI File content as a string
    /// </summary>
    /// <returns>A string</returns>
    public override string ToString( )
    {
      var sb = new StringBuilder( );
      _sections.WriteAll( sb, _unquote );
      return sb.ToString( );
    }

  }
}
