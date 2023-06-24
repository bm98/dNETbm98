using System;


namespace dNetBm98
{
  /// <summary>
  /// Detects a value for the given type which is _outside_ given limits
  /// Must provide a Type which implement IComparable(T)
  /// 
  ///   triggers a predefined action if one is provided
  /// </summary>
  public class OutsideLimitDetector<T> where T : IComparable<T>
  {
    private T _currentValue = default;
    private T _prevValue = default;
    private bool _limitDetected = false;
    private readonly Action<T> _action = null;

    private T _limitLow = default;
    private T _limitHigh = default;

    /// <summary>
    /// cTor: Create an InsideLimitDetector
    ///   The detector will fire for the first value update which gets outside the limits
    ///   If the value gets back into limits but was not Read the detection state remains set 
    ///   until Read
    /// </summary>
    /// <param name="lowerLimit">The lower limit of the detector</param>
    /// <param name="higherLimit">The higher limit of the detector</param>
    /// <param name="value">The start Value</param>
    /// <param name="limitAction">An action to perfom if a detection fires(retuns the current value)</param>
    public OutsideLimitDetector( T lowerLimit, T higherLimit, T value = default, Action<T> limitAction = null )
    {
      _limitLow = lowerLimit;
      _limitHigh = higherLimit;
      _currentValue = value;
      _prevValue = value;
      _limitDetected = false;
      _action = limitAction;
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
    public bool LimitDetected => _limitDetected;

    // True if value is outside limits
    private bool OutsideLimit( T value )
    {
      return (value.CompareTo( _limitHigh ) > 0) || (value.CompareTo( _limitLow ) < 0);
    }

    /// <summary>
    /// Read and Clear the DetectionState returning the Value
    /// </summary>
    /// <returns>Current Value</returns>
    private T ReadValue( )
    {
      _limitDetected = false;
      return _currentValue;
    }

    /// <summary>
    /// Read and Clear the DetectionState
    /// </summary>
    /// <returns>True if outside limit detected</returns>
    public bool Read( )
    {
      var ret = _limitDetected;
      _limitDetected = false;
      return ret;
    }

    /// <summary>
    /// Read and Clear the DetectionState
    /// </summary>
    /// <returns>True if changed and the value is outside limits</returns>
    public bool Read_IsOutside( )
    {
      var ret = _limitDetected;
      _limitDetected = false;
      return ret && OutsideLimit( _currentValue );
    }

    /// <summary>
    /// Read and Clear the DetectionState
    /// </summary>
    /// <returns>True if changed and the value is NOT outside limits</returns>
    public bool Read_IsNotOutside( )
    {
      var ret = _limitDetected;
      _limitDetected = false;
      return ret && !OutsideLimit( _currentValue );
    }

    /// <summary>
    /// Read and Clear the DetectionState
    ///  returns the current value
    /// </summary>
    /// <param name="currentValue">Out: Current Value</param>
    /// <returns>True if changed</returns>
    public bool Read( out T currentValue )
    {
      var ret = _limitDetected;
      _limitDetected = false;
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
      _limitDetected = OutsideLimit( value );
      _prevValue = _currentValue;
      _currentValue = value;
      // Trigger the action if requested
      if (_limitDetected) {
        _action?.Invoke( ReadValue( ) );
      }
    }

    /// <summary>
    /// Override the current value without triggering the state change
    /// PrevValue will not change.
    /// </summary>
    /// <param name="value">New Value</param>
    public void OverrideValue( T value )
    {
      _currentValue = value;
      _limitDetected = false;
    }

    /// <summary>
    /// Update the Limits without triggering an event
    /// </summary>
    /// <param name="lowerLimit">The lower limit of the detector</param>
    /// <param name="higherLimit">The higher limit of the detector</param>
    public void SetLimits( T lowerLimit, T higherLimit )
    {
      _limitLow = lowerLimit;
      _limitHigh = higherLimit;
    }

  }
}
