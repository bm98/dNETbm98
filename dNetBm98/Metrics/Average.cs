using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98.Metrics
{
  /// <summary>
  /// Class to support Average calculations
  ///  Calculates 'real' average of the samples provided
  ///  Take care: Long chains may have performance penalties..
  /// </summary>
  public class Average
  {
    // full resolution numbers
    private double m_currentValue = 0;
    private double m_prevValue = 0;

    private ushort m_nSamples = 1;
    private ushort m_precision = 3;
    private Queue<double> m_samples;

    /// <summary>
    /// cTor: init with the sample size
    /// </summary>
    /// <param name="numSamples">Length of the number chain to average (default=5)</param>
    /// <param name="precision">Outgoing number of Digits (default=3)</param>
    public Average( ushort numSamples = 5, ushort precision = 3 )
    {
      m_nSamples = numSamples;
      m_precision = precision;
      m_samples = new Queue<double>( m_nSamples + 1 );
    }

    /// <summary>
    /// Reset the Module
    /// </summary>
    public void Reset( )
    {
      m_samples.Clear( );
      m_currentValue = 0;
      m_prevValue = 0;
    }

    /// <summary>
    /// Add one sample
    /// </summary>
    /// <param name="value">A sample</param>
    public void Sample( float value )
    {
      if (float.IsNaN( value )) return; // simply ignore NaNs

      m_prevValue = m_currentValue;
      m_samples.Enqueue( value / m_nSamples ); // store scaled, so we only use the Sum for returning the value
      while (m_samples.Count > m_nSamples) {
        m_samples.Dequeue( );
      }
      m_currentValue = (m_samples.Count <= 0) ? 0 : m_samples.Sum( );
    }

    /// <summary>
    /// Returns the Average
    /// </summary>
    public float Avg => (float)Math.Round( m_currentValue, m_precision );
    /// <summary>
    /// Returns the Previous Average
    /// </summary>
    public float AvgPrev => (float)Math.Round( m_prevValue, m_precision );
    /// <summary>
    /// Returns the Direction from Prev to Current Value (1: going up; -1 going down; 0: stay)
    /// </summary>
    public int Trend => (Avg > AvgPrev) ? 1 : (Avg < AvgPrev) ? -1 : 0;
  }
}