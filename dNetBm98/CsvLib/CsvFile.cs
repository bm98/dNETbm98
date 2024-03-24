using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dNetBm98.CsvLib
{
  /// <summary>
  /// Supported Encodings for CSV files
  /// </summary>
  public enum CsvEncoding
  {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    UTF8,
    Unicode,
    ASCII,
    //ANSI, // not supported, too many dependencies to Net dlls
    iso_8859_1,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
  }

  /// <summary>
  /// Line termination selection
  /// </summary>
  public enum LineMode
  {
    /// <summary>
    /// Linetermination is derived from platform
    ///  aka ReadLine() is used
    /// </summary>
    Platform,
    /// <summary>
    /// Line termination is CRLF
    /// </summary>
    CrLf,
    /// <summary>
    /// Line termination is LF
    /// </summary>
    Cr,
    /// <summary>
    /// Line termination is CR
    /// </summary>
    Lf,
  }

  /// <summary>
  /// Represents a CSV file
  /// </summary>
  public class CsvFile
  {
    // this filename
    private string _fileName = "";

    private CsvContainer _container = new CsvContainer( );
    // encoding used for read/write of files
    private Encoding _encoding = Encoding.UTF8;
    // end of line mode
    private LineMode _lineMode = LineMode.Platform;

    // true to unquote fields
    private bool _unquote = false;
    private bool _valid = false;

    // defaults to current culture list separator
    private readonly char c_localeSeparator
      = Convert.ToChar( System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator.Substring( 0, 1 ) );
    private char _separator
      = Convert.ToChar( System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator.Substring( 0, 1 ) );


    /// <summary>
    /// The CSV container holding the content
    /// </summary>
    public CsvContainer CsvContainer { get => _container; }

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

    // Read the Csv file from the stream
    private void LoadStreamLow( Stream stream )
    {
      _valid = false;

      var reader = new CsvReader( _lineMode );
      reader.CsvProcessingEvent += Reader_CsvProcessingEvent;

      using (var sr = new StreamReader( stream )) {
        reader.LoadStream( sr, 0 );
      }
      _valid = true; // only here we decide
    }


    /// <summary>
    /// cTor: empty
    /// </summary>
    public CsvFile( ) { }

    /// <summary>
    /// cTor: Create a Csv File from a filename
    ///  Note: default encoding is UTF8 
    ///    if you need another encoding:
    ///    - create an empty object
    ///    - Load(file, encoding) 
    ///    
    ///   You may set an other encoding before Writing
    ///   
    /// </summary>
    /// <param name="fileName">Fully qualified path and name</param>
    /// <param name="separator">A column separator character (locale)</param>
    /// <param name="lineMode">Line Termination mode (Platform)</param>
    /// <param name="encoding">Optional encoding of the file</param>
    public CsvFile( string fileName, char separator = '\0', LineMode lineMode = LineMode.Platform, CsvEncoding encoding = CsvEncoding.UTF8 )
    {
      if (separator == '\0')
        _separator = c_localeSeparator;
      else
        _separator = separator;
      _lineMode = lineMode;
      SetEncoding( encoding );

      this.Load( fileName, _separator, _lineMode, encoding );
    }

    /// <summary>
    /// cTor: Create a Csv File from a stream
    ///  Note: default encoding is UTF8 
    ///    if you need another encoding:
    ///    - create an empty object
    ///    - Load(stream, encoding) 
    ///    
    ///   You may set an other encoding before Writing
    ///   
    /// </summary>
    /// <param name="stream">An open stream</param>
    /// <param name="separator">A column separator character (locale)</param>
    /// <param name="lineMode">Line Termination mode (Platform)</param>
    /// <param name="encoding">Optional encoding of the file</param>
    public CsvFile( Stream stream, char separator = '\0', LineMode lineMode = LineMode.Platform, CsvEncoding encoding = CsvEncoding.UTF8 )
    {
      if (separator == '\0')
        _separator = c_localeSeparator;
      else
        _separator = separator;
      _lineMode = lineMode;
      SetEncoding( encoding );

      this.Load( stream, _separator, _lineMode );
    }

    /// <summary>
    /// Set the File Encoding for writing
    /// </summary>
    public void SetEncoding( CsvEncoding encoding )
    {
      switch (encoding) {
        case CsvEncoding.UTF8: SetEncodingUTF8( ); break;
        case CsvEncoding.Unicode: SetEncodingUNICODE( ); break;
        case CsvEncoding.ASCII: SetEncodingASCII( ); break;
        //case CsvEncoding.ANSI: SetEncodingANSI( ); break;
        case CsvEncoding.iso_8859_1: SetEncodingISO_8859_1( ); break;
        default: SetEncodingUTF8( ); break;
      }
    }

    /// <summary>
    /// Set the File Line termination mode
    /// </summary>
    public void SetLineMode( LineMode lineMode )
    {
      _lineMode = lineMode;
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
    /// Set a new filename for this CSV file for writing
    /// </summary>
    /// <param name="fileName">A filename</param>
    public void SetFilename( string fileName )
    {
      _fileName = fileName;
    }


    /// <summary>
    /// Load from a stream (encoding is UTF8)
    /// </summary>
    /// <param name="stream">An open stream</param>
    /// <param name="separator">A column separator character ('\0' = locale)</param>
    /// <param name="lineMode">Line Termination mode</param>
    public void Load( Stream stream, char separator, LineMode lineMode )
    {
      if (separator == '\0')
        _separator = c_localeSeparator;
      else
        _separator = separator;
      _lineMode = lineMode;

      _valid = false;
      _container.Clear( );
      _fileName = "";
      if (stream.Length < 1) return; // ERROR too short...

      _fileName = "$$$STREAM$$$";
      LoadStreamLow( stream ); // get all
    }

    /// <summary>
    /// Load a new file
    /// 
    ///  Note: default encoding is UTF8
    /// </summary>
    /// <param name="fileName">Fully qualified path and name</param>
    /// <param name="separator">A column separator character ('\0' = locale)</param>
    /// <param name="lineMode">Line Termination mode</param>
    /// <param name="encoding">Encoding of the file</param>
    public void Load( string fileName, char separator, LineMode lineMode, CsvEncoding encoding )
    {
      if (separator == '\0')
        _separator = c_localeSeparator;
      else
        _separator = separator;
      _lineMode = lineMode;

      _valid = false;
      _container.Clear( );
      _fileName = "";
      if (!File.Exists( fileName )) return; // ERROR file does not exist..

      _fileName = fileName;
      SetEncoding( encoding );
      byte[] byt;
      try {
        using (var ts = File.Open( _fileName, FileMode.Open, FileAccess.Read, FileShare.Read )) {
          // convert the file content
          byt = new byte[ts.Length];
          ts.Read( byt, 0, byt.Length );
          var iString = Encoding.UTF8.GetString( Encoding.Convert( _encoding, Encoding.UTF8, byt ) );
          using (var ms = new MemoryStream( Encoding.UTF8.GetBytes( iString ) )) {
            LoadStreamLow( ms );
          }
        }
      }
      catch {
        ; // DEBUG
      }
      finally {
        byt = new byte[0]; // help the GC
      }
    }


    /// <summary>
    /// Writes the given CsvContainer to a file as CSV with the current list separator from the Culture setting
    /// </summary>
    /// <param name="filename">Filename</param>
    public void Write( string filename )
    {
      Write( filename, _separator );
    }

    /// <summary>
    /// Writes the given CsvContainer to a file as CSV with the given list separator
    /// </summary>
    /// <param name="filename">Filename</param>
    /// <param name="separator">A column separator</param>
    public void Write( string filename, char separator )
    {
      if (_container == null) return;

      using (StreamWriter sw = new StreamWriter( filename, false )) {
        foreach (CsvLine line in _container) {
          sw.WriteLine( line.GetLine( separator ) );
        }
      }
    }


    /// <summary>
    /// Process the reader event - read one line into the container
    /// </summary>
    private void Reader_CsvProcessingEvent( object sender, CsvProcessingEventArgs e )
    {
      if (_container == null) return;

      var line = new CsvLine( e.LineNumber, _separator, _unquote ) {
        Line = e.CsvString
      };

      _container.Add( line );
    }


  }
}
