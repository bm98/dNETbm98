using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.CsvLib
{
  /// <summary>
  /// Event supporting reading CSV files
  /// Event is triggered for each content line of the CSV file
  /// </summary>
  public class CsvProcessingEventArgs
  {
    /// <summary>
    /// Line Number 1..
    /// </summary>
    public int LineNumber { get; set; }   // 1..
    /// <summary>
    /// Percent Progress 0..100
    /// </summary>
    public int ProgressPrct { get; set; } // 0..100
    /// <summary>
    /// The Line to process
    /// </summary>
    public string CsvString { get; set; } // Empty while OK else contains a hint of the issue encountered
    /// <summary>
    /// cTor:
    /// </summary>
    public CsvProcessingEventArgs( int lineNo, int progress, string csvString )
    {
      LineNumber = lineNo;
      ProgressPrct = progress;
      CsvString = csvString;
    }

  }
}
