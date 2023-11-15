using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static dNetBm98.IniLib.MSiniFile;

namespace dNetBm98.IniLib
{
  /// <summary>
  /// (De)Serializer for INI files
  /// </summary>
  public class IniSerializer
  {

    #region Generic DeSerializers


#pragma warning disable CS0168 // Variable is declared but never used

    /// <summary>
    /// Reads from the open stream one entry
    /// </summary>
    /// <param name="iStream">An open stream at position</param>
    /// <param name="unQuoteValues">Set true to 'unquote' the values (default false)</param>
    /// <returns>An obj or null for errors</returns>
    public static T FromIniStream<T>( Stream iStream, bool unQuoteValues = false )
    {
      try {
        var iniSerializer = new IniSerializer( typeof( T ) );
        object objResponse = iniSerializer.Deserialize( iStream, unQuoteValues );
        var iniResults = (T)objResponse;
        iStream.Flush( );
        return iniResults;
      }
      catch (Exception ex) {
        Console.WriteLine( $"FromIniStream: Failed with exception <{ex}>" );
        return default( T );
      }
    }

    /// <summary>
    /// Reads from the supplied string
    /// </summary>
    /// <param name="iString">A INI formatted string</param>
    /// <param name="unQuoteValues">Set true to 'unquote' the values (default false)</param>
    /// <returns>An obj or null for errors</returns>
    public static T FromIniString<T>( string iString, bool unQuoteValues = false )
    {
      try {
        T iniResults = default;
        using (var ms = new MemoryStream( Encoding.UTF8.GetBytes( iString ) )) {
          iniResults = FromIniStream<T>( ms, unQuoteValues );
        }
        return iniResults;
      }
      catch (Exception ex) {
        Console.WriteLine( $"FromIniString: Failed with exception <{ex}>" );
        return default;
      }
    }


    /// <summary>
    /// Reads from a file one entry
    /// Tries to aquire a shared Read Access and blocks for max 100ms while doing so
    /// </summary>
    /// <param name="iFilename">The INI Filename</param>
    /// <param name="unQuoteValues">Set true to 'unquote' the values (default false)</param>
    /// <param name="encoding">Encoding (default to iso_8859_1)</param>
    /// <returns>An obj or null for errors</returns>
    public static T FromIniFile<T>( string iFilename, bool unQuoteValues = false, IniEncoding encoding = IniEncoding.iso_8859_1 )
    {
      T retVal = default;
      if (!File.Exists( iFilename )) {
        Console.WriteLine( $"IniLib: File not found <{iFilename}>" );
        return retVal;
      }

      int retries = 10; // 100ms worst case
      while (retries-- > 0) {
        byte[] byt;
        try {
          using (var ts = File.Open( iFilename, FileMode.Open, FileAccess.Read, FileShare.Read )) {
            ts.Seek( 0, SeekOrigin.Begin ); // reset
            byt = new byte[ts.Length];
            ts.Read( byt, 0, byt.Length );

            if (encoding == IniEncoding.iso_8859_1) {
              var encoder = Encoding.GetEncoding( "iso-8859-1" );
              var tmp = Encoding.UTF8.GetString( Encoding.Convert( encoder, Encoding.UTF8, byt ) );
              retVal = FromIniString<T>( tmp, unQuoteValues );
            }
            else if (encoding == IniEncoding.ASCII) {
              var encoder = Encoding.ASCII;
              var tmp = Encoding.UTF8.GetString( Encoding.Convert( encoder, Encoding.UTF8, byt ) );
              retVal = FromIniString<T>( tmp, unQuoteValues );
            }
            /*
            else if (encoding == IniEncoding.ANSI) {
              //var encoder = CodePagesEncodingProvider.Instance.GetEncoding( 1252 );
              var encoder = Encoding.ASCII;
              var tmp = Encoding.UTF8.GetString( Encoding.Convert( encoder, Encoding.UTF8, byt ) );
              retVal = FromIniString<T>( tmp );
            }
            */
            else if (encoding == IniEncoding.Unicode) {
              var encoder = Encoding.Unicode;
              var tmp = Encoding.UTF8.GetString( Encoding.Convert( encoder, Encoding.UTF8, byt ) );
              retVal = FromIniString<T>( tmp, unQuoteValues );
            }
            else {
              // UTF8 for the rest, return from bytes as above
              var tmp = Encoding.UTF8.GetString( byt );
              retVal = FromIniString<T>( tmp, unQuoteValues );
            }
          }
          return retVal;
        }
        catch (IOException ioex) {
          // retry after a short wait
          Thread.Sleep( 10 ); // allow the others fileIO to be completed
        }
        catch (Exception ex) {
          // not an IO exception - just fail
          Console.WriteLine( $"IniLib: Failed with exception <{ex}>" );
          return retVal;
        }
        finally {
          byt = new byte[0]; // help the GC
        }
      }

      return retVal;
    }
#pragma warning restore CS0168 // Variable is declared but never used

    #endregion

    // Deserialize a section
    private object DeSection( Type st, string section, MSiniFile iniFile, int level )
    {
    //  Console.WriteLine( $"IniLib: Desection type: <{st.Name}> sect: <{section}> lvl: <{level}>" );

      var svSection = (section == IniFileSection.MainSection) ? "" : section; // Main in the INI is an empty string
      var ret = Activator.CreateInstance( st );

      // Base Type scan
      PropertyInfo[] propertyInfo;
      Type myType = st;
      // Get the type and fields of FieldInfoClass.
      propertyInfo = myType.GetProperties( BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly );

    //  Console.WriteLine( $"IniLib: Desection PropInfo Array Len: <{propertyInfo.Length}>" );

      // loop all fields 
      foreach (var prop in propertyInfo) {
        var cArgs = prop.GetCustomAttributes( false );
        string svKey = null, sectionAttr = null;
        // check if settable
        if (prop.GetSetMethod( true ) == null) { continue; } // cannot 
        // find the Ini attribute of this Property
        for (int a = 0; a < cArgs.Length; a++) {
          if (cArgs[a] is IniFileKey) {
            svKey = (cArgs[a] as IniFileKey).Name;
          }
          else if (cArgs[a] is IniFileSection) {
            sectionAttr = (cArgs[a] as IniFileSection).Name;
          }
          else if (cArgs[a] is IniFileIgnore) { continue; } // jump to next
          else {
            // other attribute - we don't care
          }
        }

        // check for Ini attribute available
        if (string.IsNullOrEmpty( svKey ) && string.IsNullOrEmpty( sectionAttr )) {
          // for now we ignore them
          //throw new ApplicationException( $"RegisterIniVars - missing Attribute for property: {prop.Name}" );
        }

        else {
          // now we should be able to load items..
          if (prop.PropertyType == typeof( string )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            prop.SetValue( ret, strValue );
          }
          else if (prop.PropertyType == typeof( float )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            if (float.TryParse( strValue, out float dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( double )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            if (double.TryParse( strValue, out double dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( int )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            if (int.TryParse( strValue, out int dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( uint )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            if (uint.TryParse( strValue, out uint dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( long )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            if (long.TryParse( strValue, out long dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( ulong )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            if (ulong.TryParse( strValue, out ulong dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( short )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            if (short.TryParse( strValue, out short dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( ushort )) {
            var strValue = iniFile.ItemValue( svSection, svKey );
            if (ushort.TryParse( strValue, out ushort dValue )) { prop.SetValue( ret, dValue ); }
          }

          else if (prop.PropertyType == typeof( Dictionary<string, string> )) {
            // a sequence of svNameN items
            var dict = new Dictionary<string, string>( );
            int index = 0;
            var strValue = iniFile.ItemValue( svSection, $"{svKey}{index}" );
            while (!string.IsNullOrEmpty( strValue )) {
              dict.Add( $"{svKey}{index}", strValue );
              // next
              strValue = iniFile.ItemValue( svSection, $"{svKey}{++index}" );
            }
            prop.SetValue( ret, dict );
          }
          else {
            // custom type, represents a section can only be on level 0
            if ((level == 0) && !string.IsNullOrEmpty( sectionAttr )) {
              object item = DeSection( prop.PropertyType, sectionAttr, iniFile, level + 1 );
              prop.SetValue( ret, item );
            }
            else if (!string.IsNullOrEmpty( svKey )) {
              // decode the string content itself into an object
            }
            else {
              throw new ApplicationException( $"ERROR: Nested section of Type: {prop.PropertyType} of Property {prop.Name}" );
            }
          }
        }
      }
      return ret;
    }

    private Type _type;

    /// <summary>
    /// cTor: 
    /// </summary>
    /// <param name="type">Ini File Main Class</param>
    public IniSerializer( Type type )
    {
      _type = type;
    }

    /// <summary>
    /// Deserializes the INI document contained by the specified System.IO.Stream.
    /// </summary>
    /// <param name="stream">The System.IO.Stream that contains the INI document to deserialize.</param>
    /// <param name="unQuoteValues">Set true to 'unquote' the values (default false)</param>
    /// <returns>The System.Object being deserialized.</returns>
    public object Deserialize( Stream stream, bool unQuoteValues = false )
    {
      var ret = Activator.CreateInstance( _type );

      MSiniFile iniFile = new MSiniFile( );
      iniFile.SetUnqoteValues( unQuoteValues );
      iniFile.Load( stream );

      ret = DeSection( _type, IniFileSection.MainSection, iniFile, 0 );
    //  Console.WriteLine( $"IniLib: Desection returned <{ret}>" );

      return ret;
    }
  }
}
