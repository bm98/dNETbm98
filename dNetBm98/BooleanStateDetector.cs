using System;


namespace dNetBm98
{
  /// <summary>
  /// A Boolean State detection class
  /// detects changes in State, maintains the internal state to compare to
  /// </summary>
  public class BooleanStateDetector
  {
    private bool _currentState = false;
    private bool _prevState = false;
    private bool _stateChanged = false;
    private readonly Action<bool> _action = null;

    /// <summary>
    /// cTor: Creates a BooleanStateDetector
    ///       Add an Action to be exec on a change detection (this will clear the state change flag immediately)
    /// </summary>
    /// <param name="state">Initial State (defaults to false)</param>
    /// <param name="changeAction">An Action(newState) to be triggered on a state change, will clear the change indication (defaults to null)</param>
    public BooleanStateDetector( bool state = false, Action<bool> changeAction = null )
    {
      _currentState = state;
      _prevState = state;
      _stateChanged = false;
      _action = changeAction;
    }

    /// <summary>
    /// Returns the current State
    /// </summary>
    public bool State => _currentState;

    /// <summary>
    /// Returns the previous State
    /// </summary>
    public bool PrevState => _prevState;

    /// <summary>
    /// Returns True if the StateChange flag is true (not clearing it)
    /// </summary>
    public bool StateChanged => _stateChanged;

    /// <summary>
    /// Read and Clear the StateChange returning the State
    /// </summary>
    /// <returns>Current State</returns>
    private bool ReadState( )
    {
      _stateChanged = false;
      return _currentState;
    }

    /// <summary>
    /// Read and Clear the StateChange
    /// </summary>
    /// <returns>True if changed</returns>
    public bool Read( )
    {
      var ret = _stateChanged;
      _stateChanged = false;
      return ret;
    }

    /// <summary>
    /// Read and Clear the StateChange
    /// </summary>
    /// <returns>True if changed and state is Set</returns>
    public bool Read_IsSet( )
    {
      var ret = _stateChanged;
      _stateChanged = false;
      return ret && _currentState;
    }

    /// <summary>
    /// Read and Clear the StateChange
    /// </summary>
    /// <returns>True if changed and state is NOT Set</returns>
    public bool Read_IsNotSet( )
    {
      var ret = _stateChanged;
      _stateChanged = false;
      return ret && !_currentState;
    }

    /// <summary>
    /// Read and Clear the StateChange
    ///  returns the current state
    /// </summary>
    /// <param name="currentState">Out: Current State</param>
    /// <returns>True if changed</returns>
    public bool Read( out bool currentState )
    {
      var ret = _stateChanged;
      _stateChanged = false;
      currentState = _currentState;
      return ret;
    }

    /// <summary>
    /// Update the State and detect changes
    /// Triggers the ChangeAction if one is defined
    /// </summary>
    /// <param name="state">New State</param>
    public void Update( bool state )
    {
      _stateChanged = state != _currentState;
      _prevState = _currentState;
      _currentState = state;
      // Trigger the action if requested
      if (_stateChanged) {
        _action?.Invoke( ReadState( ) );
      }
    }

    /// <summary>
    /// Override the current state without triggering the state change
    /// PrevState will not change.
    /// </summary>
    /// <param name="state">New State</param>
    public void OverrideState( bool state )
    {
      _currentState = state;
      _stateChanged = false;
    }

  }
}
