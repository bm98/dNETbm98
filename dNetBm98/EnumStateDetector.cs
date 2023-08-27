using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dNetBm98
{
  /// <summary>
  /// Detects a value for the given type which is _outside_ given limits
  /// Must provide a Type which implement IComparable(TRef)
  /// 
  ///   triggers a predefined action if one is provided
  /// </summary>
  public class EnumStateDetector<T> where T : Enum
  {
    private T _currentState = default;
    private T _prevState = default;
    private bool _stateChanged = false;
    private readonly Action<T> _action = null;

    /// <summary>
    /// cTor: Creates a BooleanStateDetector
    ///       Add an Action to be exec on a change detection (this will clear the state change flag immediately)
    /// </summary>
    /// <param name="state">Initial State (defaults to false)</param>
    /// <param name="changeAction">An Action(newState) to be triggered on a state change, will clear the change indication (defaults to null)</param>
    public EnumStateDetector( T state = default, Action<T> changeAction = null )
    {
      _currentState = state;
      _prevState = state;
      _stateChanged = false;
      _action = changeAction;
    }

    /// <summary>
    /// Returns the current State
    /// </summary>
    public T State => _currentState;

    /// <summary>
    /// Returns the previous State
    /// </summary>
    public T PrevState => _prevState;

    /// <summary>
    /// Returns True if the StateChange flag is true (not clearing it)
    /// </summary>
    public bool StateChanged => _stateChanged;

    // True when a the state is not matching the current state
    private bool ChangeDetected( T state ) => state.CompareTo( _currentState ) != 0;


    /// <summary>
    /// Read and Clear the StateChange returning the State
    /// </summary>
    /// <returns>Current State</returns>
    private T ReadState( )
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
    ///  returns the current state
    /// </summary>
    /// <param name="currentState">Out: Current State</param>
    /// <returns>True if changed</returns>
    public bool Read( out T currentState )
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
    public void Update( T state )
    {
      _stateChanged = ChangeDetected( state );
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
    public void OverrideState( T state )
    {
      _currentState = state;
      _stateChanged = false;
    }

  }
}
