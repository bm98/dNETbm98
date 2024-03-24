using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dNetBm98.CsvLib
{
  /// <summary>
  /// A generic and a specifc Line reader with a callback function to process the line
  /// </summary>
  public class CsvReader
  {
    /// <summary>
    /// Callback of the reader - for each line it calls to process the line read
    /// also reports the line number (1...) and the progress as % 
    /// </summary>
    public event EventHandler<CsvProcessingEventArgs> CsvProcessingEvent;
    private void ProcessEvent( string csvString )
    {
      double progress = (double)_lineNo * 100 / _fileNumLines;
      CsvProcessingEvent?.Invoke( this, new CsvProcessingEventArgs( _lineNo, (int)progress, csvString ) );
    }


    //  The default interval based on the 'Old' version
    private int _fileNumLines = 0;
    private int _lineNo = 0;
    private int _numberOfLines = 0;

    private LineMode _lineMode = LineMode.Platform;

    private void SetLineMode( LineMode lineMode )
    {
      switch (lineMode) {
        case LineMode.Platform:
          LT = "\n";
          break;
        case LineMode.CrLf:
          LT = CRLF;
          break;
        case LineMode.Cr:
          LT = CR;
          break;
        case LineMode.Lf:
          LT = LF;
          break;
        default:
          LT = "\n";
          break;
      }
    }


    /// <summary>
    /// Returns the number of lines in the file
    /// </summary>
    public int NumberOfLines => _numberOfLines;

    private int CountFileLines( StreamReader sr )
    {
      sr.BaseStream.Seek( 0, SeekOrigin.Begin );
      string line;
      int count = 0;
      while ((line = sr.ReadLine( )) != null) {
        count++;
      }
      sr.BaseStream.Seek( 0, SeekOrigin.Begin );
      return count;
    }

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="lineMode">The Line Termination mode</param>
    public CsvReader(LineMode lineMode )
    {
      _lineMode = lineMode;
      SetLineMode( _lineMode );
    }

    /// <summary>
    /// A generic reader for UTF0
    ///  it will not check for valid characters whatsoever
    ///  it will skip lines shorter than minLength
    /// </summary>
    /// <param name="reader">The open stream as StreamReader</param>
    /// <param name="minLength">The minimum expected input line length</param>
    public void LoadStream(StreamReader reader, int minLength = 0 )
    {
      _lineNo = 1;
      if ( _lineMode== LineMode.Platform) {
        _fileNumLines = CountFileLines( reader );
        // read until end
        while (!reader.EndOfStream) {
          string buf = reader.ReadLine( ).Trim( );
          if (buf.Length >= minLength) {
            ProcessEvent( buf ); // let the line be processed
            _lineNo++;
          }
        }
      }
      else {
        // use CRLF reader
        SetLineMode(_lineMode );
        _fileNumLines = InitCRLFReader( reader );
        // read until end
        while ((!CRLFReaderEOS)) {
          string buf = CRLFReadLine( ).Trim( );
          if (buf.Length >= minLength) {
            ProcessEvent( buf ); // let the line be processed
            _lineNo++;
          }
        }
      }
    }


    #region CRLF Reader Implementation

    private string _lBuf = "";
    private const string CRLF = "\r\n";
    private const string CR = "\r";
    private const string LF = "\n";
    private string LT = CRLF;

    // a reader to return only CRLF terminated lines
    // returns the number of line on Init
    private int InitCRLFReader( StreamReader sr )
    {
      _lBuf = sr.ReadToEnd( );
      return Regex.Matches( _lBuf, LT ).Count;
    }
    // returns CRLF terminated parts (without CRLF) Like ReadLine()
    private string CRLFReadLine( )
    {
      if (_lBuf.Length <= 0) return ""; // EOS

      string buf;
      int lt = _lBuf.IndexOf( LT );
      if (lt < 0) {
        // no CRLF anymore - return rest
        buf = _lBuf; _lBuf = "";
        return buf;
      }

      buf = _lBuf.Substring( 0, lt );
      if (_lBuf.Length > lt + 2) {
        _lBuf = _lBuf.Substring( lt + 2 );  // there is more
      }
      else {
        _lBuf = ""; // there is no more after CRLF
      }
      return buf;

    }
    private bool CRLFReaderEOS { get => string.IsNullOrEmpty( _lBuf ); }

    #endregion


  }
}
