﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Logging
{
  /// <summary>
  /// A Logger
  /// </summary>
  public class Logger : ILog
  {
    private string _assembly = "";
    private string _type = "";
    private string _module = "";

    // the composed Module Name
    private string _modName = "";


    /// <summary>
    /// cTor: Empty
    /// </summary>
    public Logger( )
    {
      _module = "Generic";

      _modName = _module;
    }

    /// <summary>
    /// cTor: Module name
    /// </summary>
    public Logger( string module )
    {
      _module = module;

      _modName = _module;
    }

    /// <summary>
    /// cTor: Module Prefix
    /// </summary>
    public Logger( Type type )
    {
      _type = type.Name;

      _modName = $"{_type}";
    }

    /// <summary>
    /// cTor: Assembly.Module Prefix
    /// </summary>
    public Logger( Assembly assembly, Type type )
    {
      _assembly = assembly.FullName;
      _type = type.Name;

      _modName = $"{assembly.GetName( ).Name}.{_type}";
    }

    /// <summary>
    /// Log a Text Item, writes immediately to the file
    /// </summary>
    /// <param name="text">Log Text</param>
    public void LogInfo( string text )
    {
      Log.Instance.LogInfo( $"({_modName})", text );
    }

    /// <summary>
    /// Log a Text Item as Error
    /// </summary>
    /// <param name="text">Log Text</param>
    public void LogError( string text )
    {
      Log.Instance.LogError( $"({_modName})", text );
    }


    /// <summary>
    /// Log a text and dump the stacktrace from the calling process
    /// </summary>
    /// <param name="text">Log Text</param>
    public void LogStackTrace( string text )
    {
      Log.Instance.LogStackTrace( $"({_modName})", text );
    }

    /// <summary>
    /// Log a Text Item, writes immediately to the file
    /// </summary>
    /// <param name="context">A context</param>
    /// <param name="text">Log Text</param>
    public void LogInfo( string context, string text )
    {
      Log.Instance.LogInfo( $"({_modName}.{context})", text );
    }

    /// <summary>
    /// Log a Text Item as Error
    /// </summary>
    /// <param name="context">A context</param>
    /// <param name="text">Log Text</param>
    public void LogError( string context, string text )
    {
      Log.Instance.LogError( $"({_modName}.{context})", text );
    }


    /// <summary>
    /// Log a text and dump the stacktrace from the calling process
    /// </summary>
    /// <param name="context">A context</param>
    /// <param name="text">Log Text</param>
    public void LogStackTrace( string context, string text )
    {
      Log.Instance.LogStackTrace( $"({_modName}.{context})", text );
    }

  }
}
