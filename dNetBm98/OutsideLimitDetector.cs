﻿using System;


namespace dNetBm98
{
  /// <summary>
  /// Detects a value for the given type which is _outside_ given limits
  /// Must provide a Type which implement IComparable(TRef)
  /// 
  ///   triggers a predefined action if one is provided
  /// </summary>
  public class OutsideLimitDetector<T> where T : IComparable<T>
  {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    protected T _currentValue = default;
    protected T _prevValue = default;
    protected bool _limitDetected = false;
    protected readonly Action<T> _action = null;

    protected T _limitLow = default;
    protected T _limitHigh = default;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// cTor: empty - not for public
    /// </summary>
    protected OutsideLimitDetector( ) { }

    /// <summary>
    /// cTor: Create an OutsideLimitDetector
    ///   The detector will fire the Action for any value update which is outside the limits
    ///   If the value gets into the limits but was not Read before; the trigger state remains set until Read
    ///   Using an action will clear the detection state when fired !!!
    /// </summary>
    /// <param name="lowerLimit">The lower limit of the detector (below)</param>
    /// <param name="higherLimit">The higher limit of the detector (above)</param>
    /// <param name="value">The start Value</param>
    /// <param name="limitAction">An action to perfom if a detection fires(retuns the current value)</param>
    public OutsideLimitDetector( T lowerLimit, T higherLimit, T value = default, Action<T> limitAction = null )
    {
      // sanity 
      if (lowerLimit.CompareTo( higherLimit ) > 0) throw new ArgumentException( "Lower limit must be < higher limit" );

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
    /// Returns the lower limit
    /// </summary>
    public T LowerLimit => _limitLow;

    /// <summary>
    /// Returns the upper limit
    /// </summary>
    public T UpperLimit => _limitHigh;

    /// <summary>
    /// Returns True if the DetectionState flag is true (not clearing it)
    /// </summary>
    public bool LimitDetected => _limitDetected;

    /// <summary>
    /// True if value is outside limits not including limits
    /// </summary>
    protected virtual bool OutsideLimit( T value )
    {
      return (value.CompareTo( _limitHigh ) > 0) || (value.CompareTo( _limitLow ) < 0);
    }

    /// <summary>
    /// Read and Clear the DetectionState returning the Value
    /// </summary>
    /// <returns>Current Value</returns>
    protected T ReadValue( )
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
    public virtual void Update( T value )
    {
      _prevValue = _currentValue;
      var triggered = OutsideLimit( value );
      _currentValue = value;
      _limitDetected |= triggered; // stays on if previously detected but no longer triggered
      // Trigger the action if the new value is ourside limits
      if (triggered) {
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
      _prevValue = _currentValue;
      _limitDetected = false;
    }

    /// <summary>
    /// Update the Limits without triggering an event
    /// </summary>
    /// <param name="lowerLimit">The lower limit of the detector</param>
    /// <param name="higherLimit">The higher limit of the detector</param>
    public void SetLimits( T lowerLimit, T higherLimit )
    {
      // sanity 
      if (lowerLimit.CompareTo( higherLimit ) > 0) throw new ArgumentException("Lower limit must be < higher limit" );

      _limitLow = lowerLimit;
      _limitHigh = higherLimit;
    }

  }
}
