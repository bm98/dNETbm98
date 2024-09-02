using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// Perform the action
    /// </summary>
    public override void DoJob( ) => _actionT?.Invoke( );

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

  /// <summary>
  /// A Job Object with one argument for the JobRunner
  /// </summary>
  public class JobObj<T> : JobObjBase
  {
    private readonly Action<T> _actionT = null;
    private readonly T _argT = default;

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="action">A JobAction with argument of T</param>
    /// <param name="argT">The argument</param>
    /// <param name="jobName">A descriptive name of this Job</param>
    public JobObj( Action<T> action, T argT, string jobName )
      : base( jobName )
    {
      _actionT = action;
      _argT = argT;
    }

    /// <summary>
    /// Perform the action
    /// </summary>
    public override void DoJob( ) => _actionT?.Invoke( _argT );

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

  /// <summary>
  /// A Job Object with 2 arguments for the JobRunner
  /// </summary>
  public class JobObj<T1, T2> : JobObjBase
  {
    private readonly Action<T1, T2> _actionT = null;
    private readonly T1 _argT1 = default;
    private readonly T2 _argT2 = default;

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
      _argT1 = argT1;
      _argT2 = argT2;
    }

    /// <summary>
    /// Perform the action
    /// </summary>
    public override void DoJob( ) => _actionT?.Invoke( _argT1, _argT2 );

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

  /// <summary>
  /// A Job Object with 3 arguments for the JobRunner
  /// </summary>
  public class JobObj<T1, T2, T3> : JobObjBase
  {
    private readonly Action<T1, T2, T3> _actionT = null;
    private readonly T1 _argT1 = default;
    private readonly T2 _argT2 = default;
    private readonly T3 _argT3 = default;

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
      _argT1 = argT1;
      _argT2 = argT2;
      _argT3 = argT3;
    }

    /// <summary>
    /// Perform the action
    /// </summary>
    public override void DoJob( ) => _actionT?.Invoke( _argT1, _argT2, _argT3 );

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }
  
  /// <summary>
  /// A Job Object with 4 arguments for the JobRunner
  /// </summary>
  public class JobObj<T1, T2, T3, T4> : JobObjBase
  {
    private readonly Action<T1, T2, T3, T4> _actionT = null;
    private readonly T1 _argT1 = default;
    private readonly T2 _argT2 = default;
    private readonly T3 _argT3 = default;
    private readonly T4 _argT4 = default;

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
      _argT1 = argT1;
      _argT2 = argT2;
      _argT3 = argT3;
      _argT4 = argT4;
    }

    /// <summary>
    /// Perform the action
    /// </summary>
    public override void DoJob( ) => _actionT?.Invoke( _argT1, _argT2, _argT3, _argT4 );

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
    /// Perform the action
    /// </summary>
    public abstract void DoJob( );

    /// <inheritdoc/>
    public override string ToString( ) => _jobName;
  }

}
