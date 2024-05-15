using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98
{
  /// <summary>
  /// Slope Detector Type of pass through to detect
  /// </summary>
  public enum Slope
  {
    /// <summary>
    /// Detect value pass through from either side
    /// </summary>
    BiDirectional = 0,
    /// <summary>
    /// Detect a value passing through from above
    /// </summary>
    FromAbove,
    /// <summary>
    /// Detect a value passing through from below
    /// </summary>
    FromBelow,
  }

  /// <summary>
  /// Detects a value pass through a target value
  /// </summary>
  public class SlopeDetector<T> where T : IComparable<T>
  {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    protected T _currentValue = default;
    protected T _prevValue = default;
    protected bool _slopeDetected = false;
    protected Slope _slope;
    protected readonly Action<T> _action = null;

    protected T _targetValue = default;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// cTor: empty - not for public
    /// </summary>
    protected SlopeDetector( ) { }

    /// <summary>
    /// cTor: Create an SlopeDetector
    ///   The detector will fire for the first value update which passes the target value based on slope
    ///   The detection state remains set until Read
    /// </summary>
    /// <param name="slope">Type of slope to detect</param>
    /// <param name="targetValue">The lower limit of the detector</param>
    /// <param name="value">The start Value</param>
    /// <param name="slopeAction">An action to perfom if a detection fires(retuns the current value)</param>
    public SlopeDetector( Slope slope, T targetValue, T value = default, Action<T> slopeAction = null )
    {
      _slope = slope;
      _targetValue = targetValue;
      _currentValue = value;
      _prevValue = value;
      _slopeDetected = false;
      _action = slopeAction;
    }

    /// <summary>
    /// Returns the current Value
    /// </summary>
    public T Value => _currentValue;

    /// <summary>
    /// Returns the previous Value
    /// </summary>
    public T PrevValue => _prevValue;

    /// <summary>
    /// Returns True if the DetectionState flag is true (not clearing it)
    /// </summary>
    public bool LimitDetected => _slopeDetected;


    /// <summary>
    /// True when a slope has been triggered, according to Slope set
    /// </summary>
    protected bool SlopeTest( T value )
    {
      if (_slopeDetected) return true; // triggered before

      var prevSmaller = (_prevValue.CompareTo( _targetValue ) < 0); // smaller
      var prevLarger = (_prevValue.CompareTo( _targetValue ) > 0);  // larger

      var currSmallEQ = (value.CompareTo( _targetValue ) <= 0); // smaller or equal
      var currLargeEQ = (value.CompareTo( _targetValue ) >= 0); // larger or equal

      var fromBelow = prevSmaller && currLargeEQ;
      var fromAbove = prevLarger && currSmallEQ;

      switch (_slope) {
        case Slope.BiDirectional:
          return fromAbove || fromBelow;
        case Slope.FromAbove:
          return fromAbove;
        case Slope.FromBelow:
          return fromBelow;
        default: return false;
      }
    }

    /// <summary>
    /// Read and Clear the DetectionState returning the Value
    /// </summary>
    /// <returns>Current Value</returns>
    protected T ReadValue( )
    {
      _slopeDetected = false;
      return _currentValue;
    }

    /// <summary>
    /// Read and Clear the DetectionState
    /// </summary>
    /// <returns>True if the trigger fired</returns>
    public bool Read( )
    {
      var ret = _slopeDetected;
      _slopeDetected = false;
      return ret;
    }

    /// <summary>
    /// Read and Clear the DetectionState
    ///  returns the current value
    /// </summary>
    /// <param name="currentValue">Out: Current Value</param>
    /// <returns>True if changed</returns>
    public bool Read( out T currentValue )
    {
      var ret = _slopeDetected;
      _slopeDetected = false;
      currentValue = _currentValue;
      return ret;
    }

    /// <summary>
    /// Update the Value and detect changes
    /// Triggers the ChangeAction if one is defined
    /// </summary>
    /// <param name="value">New Value</param>
    public void Update( T value )
    {
      _prevValue = _currentValue;
      _slopeDetected = SlopeTest( value );
      _currentValue = value;
      // Trigger the action if requested
      if (_slopeDetected) {
        _action?.Invoke( ReadValue( ) );
      }
    }

    /// <summary>
    /// Override the current value and resert the detection state
    /// PrevValue will not change.
    /// </summary>
    /// <param name="value">New Value</param>
    public void OverrideValue( T value )
    {
      _currentValue = value;
      _prevValue = value;
      _slopeDetected = false;
    }

    /// <summary>
    /// Update the Target Value without triggering an event
    /// </summary>
    /// <param name="targetValue">The target value</param>
    public void SetTarget( T targetValue )
    {
      _targetValue = targetValue;
    }

    /// <summary>
    /// Update the Slope type to detect without triggering an event
    /// </summary>
    /// <param name="slope">The slope to detect</param>
    public void SetSlope( Slope slope )
    {
      _slope = slope;
    }


  }
}
