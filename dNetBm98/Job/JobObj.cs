using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Job
{
  /// <summary>
  /// A Job Object for the JobRunner
  /// </summary>
  public class JobObj
  {
    private string _jobName = "";
    private Action _action;

    /// <summary>
    /// Name of the Job
    /// </summary>
    public string JobName => _jobName;

    /// <summary>
    /// cTor:
    /// </summary>
    /// <param name="action">A JobAction</param>
    /// <param name="jobName">A descriptive name of this Job</param>
    public JobObj( Action action, string jobName )
    {
      _action = action;
      _jobName = jobName;
    }

    /// <summary>
    /// Perform the action
    /// </summary>
    public void DoJob( )
    {
      _action( );
    }

    /// <inheritdoc/>
    public override string ToString( )
    {
      return _jobName;
    }

  }
}
