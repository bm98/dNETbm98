using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static dNetBm98.IniLib.MSiniFile;
using static dNetBm98.XIO;

namespace dNetBm98.IniLib
{
  /// <summary>
  /// (De)Serializer for INI files
  ///   Similar to System.Runtime.Serialization
  ///   
  ///   Constructor:
  ///     IniSerializer(Type type);
  ///   Methods:
  ///     object ReadObject(Stream stream)
  ///     void WriteObject(Stream stream, object graph)
  ///     string GetObjectString( object graph )
  ///   Properties:
  ///     bool HandleQuotedValues { get; set; } = false;
  ///       remove double quotes when reading 
  ///       add double quotes to strings and Dictionary entries
  ///       
  ///     bool SerializeReadOnlyTypes { get; set; } = false;
  ///       Also serialize Propertes without setter
  ///       
  ///   Use IniFileAttributes on Properties
  ///     [IniFileSection("sectionName")]
  ///       creates a INI section entry [SECTION]
  ///       
  ///     [IniFileKey("keyName")]
  ///       creates an INI Key=Value entry
  ///       
  ///     [IniFileIgnore]
  ///       ignores this Property
  ///       
  ///   Supported Property Types:
  ///     string
  ///     int, long, short
  ///     uint, ulong, ushort
  ///     float, double
  ///     Dictionary[string,string]  - for a list of IniKeys where key is the IniKey and Value is the IniValue
  ///      e.g. 
  ///      KEY.0= asd
  ///      KEY.1= asd
  ///      ..
  ///      
  /// </summary>
  public class IniSerializer
  {
    #region Generic Serializer

    /// <summary>
    /// Creates an INI file string from an object
    /// </summary>
    /// <param name="obj">An Obj of T to serialize</param>
    /// <param name="quoteStrings">Set true to write strings in double quotes (") (default false)</param>
    /// <param name="serializeReadonly">Set true to include Readonly properties (default false)</param>
    /// <returns>String (can be empty on error)</returns>
    public static string ToIniString<T>( T obj, bool quoteStrings = false, bool serializeReadonly = false )
    {
      try {
        var iniSerializer = new IniSerializer( typeof( T ) ) {
          HandleQuotedValues = quoteStrings,
          SerializeReadOnlyTypes = serializeReadonly
        };
        return iniSerializer.GetObjectString( obj );
      }
      catch (Exception ex) {
        Console.WriteLine( $"ToIniString: Failed with exception <{ex}>" );
        return string.Empty;
      }
    }

    /// <summary>
    /// Serialize the object as INI file into a Stream
    /// </summary>
    /// <param name="stream">An Stream to output to</param>
    /// <param name="obj">An Obj of T to serialize</param>
    /// <param name="quoteStrings">Set true to write strings in double quotes (") (default false)</param>
    /// <param name="serializeReadonly">Set true to include Readonly properties (default false)</param>
    /// <returns>True when successfull</returns>
    public static bool ToIniStream<T>( Stream stream, T obj,
                                        bool quoteStrings = false, bool serializeReadonly = false )
    {
      try {
        var iniSerializer = new IniSerializer( typeof( T ) ) {
          HandleQuotedValues = quoteStrings,
          SerializeReadOnlyTypes = serializeReadonly
        };
        iniSerializer.WriteObject( stream, obj );
        return true;
      }
      catch (Exception ex) {
        Console.WriteLine( $"ToIniStream: Failed with exception <{ex}>" );
        return false;
      }
    }

    /// <summary>
    /// Serialize the object as INI file into a file
    /// </summary>
    /// <param name="fileName">A Filename</param>
    /// <param name="encoding">An Encoding</param>
    /// <param name="obj">An Obj of T to serialize</param>
    /// <param name="quoteStrings">Set true to write strings in double quotes (") (default false)</param>
    /// <param name="serializeReadonly">Set true to include Readonly properties (default false)</param>
    /// <returns>True when successfull</returns>
    public static bool ToIniFile<T>( string fileName, Encoding encoding, T obj,
                                        bool quoteStrings = false, bool serializeReadonly = false )
    {
      try {
        using (var sw = new StreamWriter( fileName, false, encoding )) {
          sw.Write( ToIniString( obj, quoteStrings, serializeReadonly ) );
        }
        return true;
      }
      catch (Exception ex) {
        Console.WriteLine( $"ToIniFile: Failed with exception <{ex}>" );
        return false;
      }
    }

    #endregion

    #region Generic DeSerializers

    /// <summary>
    /// Deserializes from an open UTF8 stream returning an object
    /// </summary>
    /// <param name="iStream">An open stream at position</param>
    /// <param name="unQuoteStrings">Set true to remove double quotes (") from values (default false)</param>
    /// <returns>An obj or null for errors</returns>
    public static T FromIniStream<T>( Stream iStream, bool unQuoteStrings = false )
    {
      try {
        var iniSerializer = new IniSerializer( typeof( T ) ) {
          HandleQuotedValues = unQuoteStrings,
        };
        object objResponse = iniSerializer.ReadObject( iStream );
        iStream.Flush( );
        var iniResults = (T)objResponse;
        return iniResults;
      }
      catch (Exception ex) {
        Console.WriteLine( $"FromIniStream: Failed with exception <{ex}>" );
        return default( T );
      }
    }

    /// <summary>
    /// Deserializes from the supplied string returning an object
    /// </summary>
    /// <param name="iString">A INI formatted string</param>
    /// <param name="unQuoteStrings">Set true to remove double quotes (") from values (default false)</param>
    /// <returns>An obj or null for errors</returns>
    public static T FromIniString<T>( string iString, bool unQuoteStrings = false )
    {
      try {
        T iniResults = default;
        using (var ms = new MemoryStream( Encoding.UTF8.GetBytes( iString ) )) {
          iniResults = FromIniStream<T>( ms, unQuoteStrings );
        }
        return iniResults;
      }
      catch (Exception ex) {
        Console.WriteLine( $"FromIniString: Failed with exception <{ex}>" );
        return default;
      }
    }


    /// <summary>
    /// Deserializes from the given file returning an object
    /// Tries to aquire a shared Read Access and blocks for max 100ms while doing so
    /// </summary>
    /// <param name="iFilename">The INI Filename</param>
    /// <param name="encoding">Encoding (default to iso_8859_1)</param>
    /// <param name="unQuoteStrings">Set true to remove double quotes (") from values (default false)</param>
    /// <returns>An obj or null for errors</returns>
    public static T FromIniFile<T>( string iFilename, IniEncoding encoding = IniEncoding.iso_8859_1,
                                        bool unQuoteStrings = false )
    {
      T retVal = default;
      if (!File.Exists( iFilename )) {
        Console.WriteLine( $"IniLib: File not found <{iFilename}>" );
        return retVal;
      }
      // retry a few times if the file is locked
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
              string tmp = Encoding.Unicode.GetString( Encoding.Convert( encoder, Encoding.Unicode, byt ) );
              retVal = FromIniString<T>( tmp, unQuoteStrings );
            }
            else if (encoding == IniEncoding.ASCII) {
              var encoder = Encoding.ASCII;
              string tmp = Encoding.Unicode.GetString( Encoding.Convert( encoder, Encoding.Unicode, byt ) );
              retVal = FromIniString<T>( tmp, unQuoteStrings );
            }
            /* // ANSI is not available due to dependencies
            else if (encoding == IniEncoding.ANSI) {
              var encoder = CodePagesEncodingProvider.Instance.GetEncoding( 1252 );
              string tmp = Encoding.Unicode.GetString( Encoding.Convert( encoder, Encoding.Unicode, byt ) );
              retVal = FromIniString<T>( tmp, unQuoteStrings );
            }
            */
            else if (encoding == IniEncoding.Unicode) {
              // default String Encoding used
              string tmp = Encoding.Unicode.GetString( byt );
              retVal = FromIniString<T>( tmp, unQuoteStrings );
            }
            else {
              // UTF8 for the rest, return from bytes as above
              var encoder = Encoding.UTF8;
              string tmp = Encoding.Unicode.GetString( Encoding.Convert( encoder, Encoding.Unicode, byt ) );
              retVal = FromIniString<T>( tmp, unQuoteStrings );
            }
          }
          return retVal;
        }
#pragma warning disable CS0168 // Variable is declared but never used
        catch (IOException ioex) {
#pragma warning restore CS0168 // Variable is declared but never used
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

    #endregion

    // *** CLASS Implementation

    #region Deserialize a section

    // need the type for subsection encoding
    private object DeSerializeSection( Type st, string section, MSiniFile iniFile, int level )
    {
      //  Console.WriteLine( $"IniLib: Desection type: <{st.Name}> sect: <{section}> lvl: <{level}>" );

      var svSection = (section == IniFileSection.MainSection) ? "" : section; // Main in the INI is an empty string
      var ret = Activator.CreateInstance( st ); // we need an typed object to return

      // Base Type scan
      PropertyInfo[] propertyInfo;
      // Get the type and fields of FieldInfoClass.
      propertyInfo = st.GetProperties( BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly );

      //  Console.WriteLine( $"IniLib: Desection PropInfo Array Len: <{propertyInfo.Length}>" );

      // loop all fields 
      foreach (var prop in propertyInfo) {
        var cArgs = prop.GetCustomAttributes( false );
        string svKey = null, sectionAttr = null;
        // check if setable
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
          // now we should be able to load items.., force unquoting if set and for value types anyway
          if (prop.PropertyType == typeof( string )) {
            var strValue = iniFile.ItemValue( svSection, svKey, HandleQuotedValues );
            prop.SetValue( ret, strValue );
          }
          else if (prop.PropertyType == typeof( float )) {
            var strValue = iniFile.ItemValue( svSection, svKey, true );
            if (XIO.TryParseX( strValue, out float dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( double )) {
            var strValue = iniFile.ItemValue( svSection, svKey, true );
            if (XIO.TryParseX( strValue, out double dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( int )) {
            var strValue = iniFile.ItemValue( svSection, svKey, true );
            if (XIO.TryParseX( strValue, out int dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( uint )) {
            var strValue = iniFile.ItemValue( svSection, svKey, true );
            if (XIO.TryParseX( strValue, out uint dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( long )) {
            var strValue = iniFile.ItemValue( svSection, svKey, true );
            if (XIO.TryParseX( strValue, out long dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( ulong )) {
            var strValue = iniFile.ItemValue( svSection, svKey, true );
            if (XIO.TryParseX( strValue, out ulong dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( short )) {
            var strValue = iniFile.ItemValue( svSection, svKey, true );
            if (XIO.TryParseX( strValue, out short dValue )) { prop.SetValue( ret, dValue ); }
          }
          else if (prop.PropertyType == typeof( ushort )) {
            var strValue = iniFile.ItemValue( svSection, svKey, true );
            if (XIO.TryParseX( strValue, out ushort dValue )) { prop.SetValue( ret, dValue ); }
          }

          else if (prop.PropertyType == typeof( Dictionary<string, string> )) {
            // a sequence of svNameN items
            var dict = new Dictionary<string, string>( );
            int index = 0;
            var strValue = iniFile.ItemValue( svSection, $"{svKey}{index}", HandleQuotedValues );
            while (!string.IsNullOrEmpty( strValue )) {
              dict.Add( $"{svKey}{index}", strValue );
              // next
              strValue = iniFile.ItemValue( svSection, $"{svKey}{++index}", HandleQuotedValues );
            }
            prop.SetValue( ret, dict );
          }
          else {
            // custom type, represents a section can only be on level 0
            if ((level == 0) && !string.IsNullOrEmpty( sectionAttr )) {
              object item = DeSerializeSection( prop.PropertyType, sectionAttr, iniFile, level + 1 );
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

    #endregion

    #region Serialize a section

    // need the type for subsection encoding
    private void SerializeSection( Type st, object graph, string section, MSiniFile iniFile, int level )
    {
      //  Console.WriteLine( $"IniLib: SerializeSection type: <{st.Name}> sect: <{section}> lvl: <{level}>" );

      var svSection = (section == IniFileSection.MainSection) ? "" : section; // Main in the INI is an empty string

      // Base Type scan
      PropertyInfo[] propertyInfo;
      // Get the type and fields of FieldInfoClass.
      propertyInfo = st.GetProperties( BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly );

      //  Console.WriteLine( $"IniLib: SerializeSection PropInfo Array Len: <{propertyInfo.Length}>" );

      // loop all fields 
      foreach (var prop in propertyInfo) {
        var cArgs = prop.GetCustomAttributes( false );
        string svKey = null, sectionAttr = null;

        // check if getable
        if (prop.GetGetMethod( true ) == null) { continue; } // cannot, no getter
        // check if setable if requested
        if (!SerializeReadOnlyTypes)
          if (prop.GetSetMethod( true ) == null) { continue; } // cannot, no setter but should have

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
          // now we should be able to set items..
          if (prop.PropertyType == typeof( string )) {
            string v = (string)prop.GetValue( graph );
            if (HandleQuotedValues)
              iniFile.SetQuotedValue( svSection, svKey, v );
            else
              iniFile.SetValue( svSection, svKey, v );
          }
          else if (prop.PropertyType == typeof( float )) {
            string v = ((float)prop.GetValue( graph )).ToStringX( );
            iniFile.SetValue( svSection, svKey, v );
          }
          else if (prop.PropertyType == typeof( double )) {
            string v = ((double)prop.GetValue( graph )).ToStringX( );
            iniFile.SetValue( svSection, svKey, v );
          }
          else if (prop.PropertyType == typeof( int )) {
            string v = ((int)prop.GetValue( graph )).ToStringX( );
            iniFile.SetValue( svSection, svKey, v );
          }
          else if (prop.PropertyType == typeof( uint )) {
            string v = ((uint)prop.GetValue( graph )).ToStringX( );
            iniFile.SetValue( svSection, svKey, v );
          }
          else if (prop.PropertyType == typeof( long )) {
            string v = ((long)prop.GetValue( graph )).ToStringX( );
            iniFile.SetValue( svSection, svKey, v );
          }
          else if (prop.PropertyType == typeof( ulong )) {
            string v = ((ulong)prop.GetValue( graph )).ToStringX( );
            iniFile.SetValue( svSection, svKey, v );
          }
          else if (prop.PropertyType == typeof( short )) {
            string v = ((short)prop.GetValue( graph )).ToStringX( );
            iniFile.SetValue( svSection, svKey, v );
          }
          else if (prop.PropertyType == typeof( ushort )) {
            string v = ((ushort)prop.GetValue( graph )).ToStringX( );
            iniFile.SetValue( svSection, svKey, v );
          }

          else if (prop.PropertyType == typeof( Dictionary<string, string> )) {
            // a sequence of svNameN items
            var dict = (Dictionary<string, string>)prop.GetValue( graph );
            foreach (var kv in dict) {
              string v = kv.Value;
              if (HandleQuotedValues)
                iniFile.SetQuotedValue( svSection, $"{kv.Key}", v ); // key is the fully named item
              else
                iniFile.SetValue( svSection, $"{kv.Key}", v ); // key is the fully named item
            }
          }
          else {
            // custom type, represents a section can only be on level 0
            if ((level == 0) && !string.IsNullOrEmpty( sectionAttr )) {
              iniFile.SectionCatalog.Add( new IniSection( sectionAttr ) );
              SerializeSection( prop.PropertyType, prop.GetValue( graph ), sectionAttr, iniFile, level + 1 );
            }
            else if (!string.IsNullOrEmpty( svKey )) {
              // encode the string content itself into an object
            }
            else {
              throw new ApplicationException( $"ERROR: Nested section of Type: {prop.PropertyType} of Property {prop.Name}" );
            }
          }
        }
      }
    }

    #endregion

    // Type to handle
    private Type _type;

    /// <summary>
    /// cTor: 
    /// </summary>
    /// <param name="type">Ini File Main Class</param>
    public IniSerializer( Type type )
    {
      if (!type.GetTypeInfo( ).IsClass) throw new ArgumentException( "type must be Class Type" );

      _type = type;
    }
    /// <summary>
    /// Gets or sets a value that specifies whether to handle double-quoted ["] strings
    ///  if true: on serialize:  Quote strings for output
    ///           on deserialze: Unquote strings for storage
    ///   Default is false
    /// </summary>
    public bool HandleQuotedValues { get; set; } = false;
    /// <summary>
    /// Gets or sets a value that specifies whether to serialize read only types.
    ///   Default is false
    /// </summary>
    public bool SerializeReadOnlyTypes { get; set; } = false;


    /// <summary>
    /// Deserializes INI data and returns the deserialized object.
    /// </summary>
    /// <param name="stream">The Stream to be read.</param>
    /// <returns>The deserialized object.</returns>
    public object ReadObject( Stream stream )
    {
      // sanity
      if (stream == null) throw new ArgumentNullException( "stream" );


      // use the MSiniFile as decoder
      MSiniFile iniFile = new MSiniFile( );
      iniFile.SetUnqoteValues( HandleQuotedValues );
      iniFile.Load( stream );

      var ret = Activator.CreateInstance( _type );
      ret = DeSerializeSection( _type, IniFileSection.MainSection, iniFile, 0 );

      return ret;
    }

    /// <summary>
    /// Serializes an object to an INI document.
    /// </summary>
    /// <param name="stream">The Stream that is written to.</param>
    /// <param name="graph">The object that contains the data to write to the stream.</param>
    public void WriteObject( Stream stream, object graph )
    {
      // sanity
      if (graph.GetType( ) != _type) throw new ArgumentException( "Argument is not of expected Type" );

      // use the MSiniFile as encoder
      MSiniFile iniFile = new MSiniFile( );
      iniFile.SetUnqoteValues( HandleQuotedValues );
      SerializeSection( _type, graph, IniFileSection.MainSection, iniFile, 0 );
      iniFile.WriteStream( stream );
    }

    /// <summary>
    /// Serializes an object to an INI document and returns the INI content as string
    /// </summary>
    /// <param name="graph">The object that contains the data to write to the stream.</param>
    public string GetObjectString( object graph )
    {
      // sanity
      if (graph.GetType( ) != _type) throw new ArgumentException( "Argument is not of expected Type" );

      // use the MSiniFile as encoder
      MSiniFile iniFile = new MSiniFile( );
      iniFile.SetUnqoteValues( HandleQuotedValues );
      SerializeSection( _type, graph, IniFileSection.MainSection, iniFile, 0 );
      return iniFile.ToString( );
    }


  }
}
