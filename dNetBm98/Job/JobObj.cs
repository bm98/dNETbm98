using System;
using System.Threading;

namespace dNetBm98.Job
{
  /// <summary>
  /// A Job Object without argument for the JobRunner
  /// </summary>
  public class JobObj : JobObjBase
  {
    private readonly Action _actionT = null;

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="action">A JobAction with argument of T</param>
    /// <param name="jobName">A descriptive name of this Job</param>
    public JobObj( Action action, string jobName )
      : base( jobName )
    {
      _actionT = action;
    }

    /// <summary>
    /// Perform the action on ThreadPool
    /// </summary>
    protected override void DoTpJob( object state )
    {
      try {
        _actionT?.Invoke( );
      }
      catch { }
    }

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

  /// <summary>
  /// A Job Object with one argument for the JobRunner
  /// </summary>
  public class JobObj<T1> : JobObjBase
  {
    private readonly Action<T1> _actionT = null;
    private struct TpObj
    {
      public T1 ArgT1;
      public TpObj( T1 a1 )
      {
        ArgT1 = a1;
      }
    }

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="action">A JobAction with argument of T</param>
    /// <param name="argT1">The argument</param>
    /// <param name="jobName">A descriptive name of this Job</param>
    public JobObj( Action<T1> action, T1 argT1, string jobName )
      : base( jobName )
    {
      _actionT = action;
      _tpObj = new TpObj( argT1 );
    }

    /// <summary>
    /// Perform the action on ThreadPool
    /// </summary>
    protected override void DoTpJob( object state )
    {
      try {
        if (state is TpObj vTpObj)
          _actionT?.Invoke( vTpObj.ArgT1 );
      }
      catch { }
    }

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

  /// <summary>
  /// A Job Object with 2 arguments for the JobRunner
  /// </summary>
  public class JobObj<T1, T2> : JobObjBase
  {
    private readonly Action<T1, T2> _actionT = null;
    private struct TpObj
    {
      public T1 ArgT1;
      public T2 ArgT2;
      public TpObj( T1 a1, T2 a2 )
      {
        ArgT1 = a1;
        ArgT2 = a2;
      }
    }

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="action">A JobAction with argument of T</param>
    /// <param name="argT1">The argument of Type 1</param>
    /// <param name="argT2">The argument of Type 2</param>
    /// <param name="jobName">A descriptive name of this Job</param>
    public JobObj( Action<T1, T2> action, T1 argT1, T2 argT2, string jobName )
      : base( jobName )
    {
      _actionT = action;
      _tpObj = new TpObj( argT1, argT2 );
    }

    /// <summary>
    /// Perform the action on ThreadPool
    /// </summary>
    protected override void DoTpJob( object state )
    {
      try {
        if (state is TpObj vTpObj)
          _actionT?.Invoke( vTpObj.ArgT1, vTpObj.ArgT2 );
      }
      catch { }
    }

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

  /// <summary>
  /// A Job Object with 3 arguments for the JobRunner
  /// </summary>
  public class JobObj<T1, T2, T3> : JobObjBase
  {
    private readonly Action<T1, T2, T3> _actionT = null;
    private struct TpObj
    {
      public T1 ArgT1;
      public T2 ArgT2;
      public T3 ArgT3;
      public TpObj( T1 a1, T2 a2, T3 a3 )
      {
        ArgT1 = a1;
        ArgT2 = a2;
        ArgT3 = a3;
      }
    }

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="action">A JobAction with argument of T</param>
    /// <param name="argT1">The argument of Type 1</param>
    /// <param name="argT2">The argument of Type 2</param>
    /// <param name="argT3">The argument of Type 3</param>
    /// <param name="jobName">A descriptive name of this Job</param>
    public JobObj( Action<T1, T2, T3> action, T1 argT1, T2 argT2, T3 argT3, string jobName )
      : base( jobName )
    {
      _actionT = action;
      _tpObj = new TpObj( argT1, argT2, argT3 );
    }

    /// <summary>
    /// Perform the action on ThreadPool
    /// </summary>
    protected override void DoTpJob( object state )
    {
      try {
        if (state is TpObj vTpObj)
          _actionT?.Invoke( vTpObj.ArgT1, vTpObj.ArgT2, vTpObj.ArgT3 );
      }
      catch { }
    }

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

  /// <summary>
  /// A Job Object with 4 arguments for the JobRunner
  /// </summary>
  public class JobObj<T1, T2, T3, T4> : JobObjBase
  {
    private readonly Action<T1, T2, T3, T4> _actionT = null;
    private struct TpObj
    {
      public T1 ArgT1;
      public T2 ArgT2;
      public T3 ArgT3;
      public T4 ArgT4;
      public TpObj( T1 a1, T2 a2, T3 a3, T4 a4 )
      {
        ArgT1 = a1;
        ArgT2 = a2;
        ArgT3 = a3;
        ArgT4 = a4;
      }
    }

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="action">A JobAction with argument of T</param>
    /// <param name="argT1">The argument of Type 1</param>
    /// <param name="argT2">The argument of Type 2</param>
    /// <param name="argT3">The argument of Type 3</param>
    /// <param name="argT4">The argument of Type 4</param>
    /// <param name="jobName">A descriptive name of this Job</param>
    public JobObj( Action<T1, T2, T3, T4> action, T1 argT1, T2 argT2, T3 argT3, T4 argT4, string jobName )
      : base( jobName )
    {
      _actionT = action;
      _tpObj = new TpObj( argT1, argT2, argT3, argT4 );
    }

    /// <summary>
    /// Perform the action on ThreadPool
    /// </summary>
    protected override void DoTpJob( object state )
    {
      try {
        if (state is TpObj vTpObj)
          _actionT?.Invoke( vTpObj.ArgT1, vTpObj.ArgT2, vTpObj.ArgT3, vTpObj.ArgT4 );
      }
      catch { }
    }

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }



  /// <summary>
  /// Baseclass for JobObjs
  /// </summary>
  public abstract class JobObjBase
  {
    /// <summary>
    /// The descriptive name of the Job
    /// </summary>
    protected readonly string _jobName = "";

    /// <summary>
    /// The data obj
    /// </summary>
    protected object _tpObj = null;

    /// <summary>
    /// Name of the Job
    /// </summary>
    public string JobName => _jobName;

    /// <summary>
    /// cTor:
    /// </summary>
    public JobObjBase( string jobName )
    {
      _jobName = jobName;
    }

    /// <summary>
    /// Perform the action using the internal argument _tpObj
    /// </summary>
    internal virtual void DoJob( ) => DoTpJob( _tpObj );

    /// <summary>
    /// Add the job to the ThreadPool
    /// </summary>
    internal virtual void AddToThrearPool( )
    {
      ThreadPool.QueueUserWorkItem( DoTpJob, _tpObj );
    }

    /// <summary>
    /// Perform the action using a state obj as argument
    /// </summary>
    protected abstract void DoTpJob( object state );

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

}
