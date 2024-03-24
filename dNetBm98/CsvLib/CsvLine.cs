using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dNetBm98.CsvLib
{
  /// <summary>
  /// Contains one CsvLine, 
  ///   carries the column items as List
  /// </summary>
  public class CsvLine : List<string>
  {
    private int m_lineNo = 0;
    private bool _unquote = false;

    /// <summary>
    /// Line number
    /// </summary>
    public int LineNo { get => m_lineNo; }

    /// <summary>
    /// Column Separator Charachter
    ///  defaults to the local List Separator
    /// </summary>
    public char Separator { get; set; }
      = Convert.ToChar( System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator.Substring( 0, 1 ) );

    //  Decompose strings where the delimiter in text fields must be ignored.
    //   11,11,"abc,def",1,2,3  properly i.e does not handle the comma in " " strings as delimiter
    //  Credit:  2008 Kim Anthony Gentes - 
    private Regex _regex = new Regex( "(((?<x>(?=[,]+))|(?<x>\"([^\"]|\"\")+\")|(?<x>[^,]+)),?)", RegexOptions.ExplicitCapture );
    private string _needsEscape = @"^$()[]{}|.,+-*=\/?<>";

    private void MakeRegex( )
    {
      // add escape if needed
      string sep = Separator.ToString( );
      if (_needsEscape.Contains( Separator )) sep = '\\' + sep;

      var rx = $"(((?<x>(?=[{sep}]+))|(?<x>\"([^\"]|\"\")+\")|(?<x>[^{sep}]+)){sep}?)";
      _regex = new Regex( rx, RegexOptions.ExplicitCapture | RegexOptions.Compiled );
    }

    // split with regex, don't capture separators within quotes
    private string[] Split( string csvLine )
    {

      //  catch some EOF issues with old files here
      string[] elements = _regex.Split( csvLine );

      // Kill empty entries from Regex - makes every second one a Null element
      List<string> el = new List<string>( );
      for (int i = 1; i < elements.Length; i += 2) {
        el.Add( elements[i] );
      }
      elements = el.ToArray( );

      return elements;
    }


    /// <summary>
    /// cTor with default separator (comma)
    /// </summary>
    /// <param name="lineNo">Line Number</param>
    /// <param name="unquote">True will unquote content</param>
    public CsvLine( int lineNo, bool unquote = false )
    {
      m_lineNo = lineNo;
      _unquote = unquote;
      MakeRegex( );
    }

    /// <summary>
    /// cTor with given separator
    /// </summary>
    /// <param name="lineNo">Line Number</param>
    /// <param name="separator">A separator char (take care ...)</param>
    /// <param name="unquote">True will unquote content</param>
    public CsvLine( int lineNo, char separator, bool unquote = false )
    {
      m_lineNo = lineNo;
      Separator = separator;
      _unquote = unquote;
      MakeRegex( );
    }

    /// <summary>
    /// Get, Set the line with the current separator
    /// </summary>
    public string Line {
      get { return GetLine( ); }
      set { AddLine( value ); }
    }

    /// <summary>
    /// Add a new line 
    /// </summary>
    /// <param name="lineS">The Csv line as string</param>
    public void AddLine( string lineS )
    {
      string[] e = Split( lineS );
      for (int i = 0; i < e.Length; i++) {
        this.Add( _unquote ? Utilities.FromQuoted( e[i] ) : e[i] );
      }
    }

    /// <summary>
    /// Return the line with the current Separator
    /// </summary>
    /// <returns></returns>
    public string GetLine( )
    {
      return GetLine( Separator );
    }

    /// <summary>
    /// Return the line with the given Separator
    /// </summary>
    /// <returns>A line</returns>
    public string GetLine( char separator )
    {
      string r = "";
      foreach (string s in this) {
        r += (_unquote ? $"\"{s}\"" : s) + separator; // quote the line if unquoted
      }
      r = r.TrimEnd( new char[] { separator } ); // remove last inserted one
      return r;
    }

  }
}
