using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;

namespace dNetBm98
{
  /// <summary>
  /// Some primitive Octal number handling
  /// 
  ///  The (int)octValue used for input and output is represented as Octal digits despite the internal binary representation
  ///  Only positive numbers
  ///  Instantiated with a number of digits ( max = 10)
  ///  Inc,Dec, Add, Subtract will be limited to max and 0 
  ///  IncT, DecT will turn around when reaching max or 0 respectively
  ///  
  /// </summary>
  public class Octal
  {
    /*
     calculate the maximum decimal and octal value from digits at Init
     maintain the current octal value from every Set operation
     calculations are always in Dec space
     every number change need to use the Set method or maintain the currentOctalValue by other means 
     */

    private const int c_maxDigits = 10; // Floor(32/3) 

    // number of digits 
    private int _digits = 1;

    // contains the max value for #digits in use
    private int _maxOctalValue = 0;
    private int _maxDecimalValue = 0;

    // holds the current 'octal'' value
    private int _currentOctalValue = 0;

    // contains the value bits
    private BitVector32 _bvector = new BitVector32( 0 );
    // sections to contain the octal digits index 0 for lsOctal
    BitVector32.Section[] _oDigits;

    private void Init( int digits )
    {
      // sanity
      if (digits < 1) throw new ArgumentException( "digits must be > 0" );
      if (digits > c_maxDigits) throw new ArgumentException( $"digits must be <= {c_maxDigits}" );

      _digits = digits;

      // create 3bit sections 0..digits-1
      _oDigits = new BitVector32.Section[digits];
      _oDigits[0] = BitVector32.CreateSection( 7 ); // specify the Max Value of this section ....
      _maxOctalValue = 7;
      for (int i = 1; i < _oDigits.Length; i++) {
        _oDigits[i] = BitVector32.CreateSection( 7, _oDigits[i - 1] );
        _maxOctalValue = _maxOctalValue * 10 + 7;
      }
      // max dec is 8^digits - 1
      _maxDecimalValue = (int)Math.Pow( 8, digits ) - 1;
    }

    // calculate the 'Octal' value from the bits
    private int MaintainOctalValue( )
    {
      int octValue = 0;
      for (int i = _oDigits.Length - 1; i >= 0; i--) {
        int digit = _bvector[_oDigits[i]];
        octValue = octValue * 10 + digit;
      }
      _currentOctalValue = octValue;
      // also return it for convenience
      return octValue;
    }

    /// <summary>
    /// Maximum number of supported digits
    /// Minimum is 1
    /// </summary>
    public int MaxDigits => c_maxDigits;
    /// <summary>
    /// Returns the maximum decimal value based on #digits
    /// </summary>
    public int MaxDecimalValue => _maxDecimalValue;
    /// <summary>
    /// Returns the maximum 'octal' value based on #digits
    /// </summary>
    public int MaxOctalValue => _maxOctalValue;

    /// <summary>
    /// cTor: init size and 0
    /// </summary>
    /// <param name="digits">Number of octal digits</param>
    public Octal( int digits )
    {
      Init( digits );
    }

    /// <summary>
    /// cTor: init size and octalValue
    /// </summary>
    /// <param name="digits">Number of octal digits</param>
    /// <param name="octValue">Value to init with</param>
    public Octal( int digits, int octValue )
    {
      Init( digits );
      SetOct( octValue );
    }

    /// <summary>
    /// Set from a decimal number
    /// </summary>
    /// <param name="decValue">An decimal number</param>
    public void SetDec( int decValue )
    {
      // sanity
      if (decValue < 0) throw new ArgumentException( "Input cannot be negative" );
      if (decValue > _maxDecimalValue) throw new ArgumentException( $"Input cannot be > {_maxDecimalValue}" );

      _bvector = new BitVector32( decValue );
      // update octal
      MaintainOctalValue( );
    }

    /// <summary>
    /// Get the decimal value
    /// </summary>
    /// <returns>A decimal int</returns>
    public int GetDec( ) => _bvector.Data;


    /// <summary>
    /// Set from an octalValue
    /// </summary>
    /// <param name="octValue">An 'Octal' number</param>
    public void SetOct( int octValue )
    {
      // sanity
      if (octValue < 0) throw new ArgumentException( "Input cannot be negative" );
      if (octValue > _maxOctalValue) throw new ArgumentException( $"Input cannot be > {_maxOctalValue}" ); // works in decimal space

      int value = octValue;
      for (int i = 0; i < _oDigits.Length; i++) {
        int digit = value % 10; value /= 10;
        if (digit > 7) throw new ArgumentException( $"Invalid Octal Input: in <{octValue}> one digit is >7 " ); // capture digit errors
        _bvector[_oDigits[i]] = digit;
      }
      // update octal
      _currentOctalValue = octValue; // set as we know it...
    }

    /// <summary>
    /// Get the contained value back
    /// </summary>
    /// <returns>An octValue </returns>
    public int GetOct( ) => _currentOctalValue; // returns the strored value

    /// <summary>
    /// Increment the number
    /// </summary>
    public void Inc( )
    {
      int newDecValue = GetDec( ) + 1;
      // Overflow stays at max
      if (newDecValue <= _maxDecimalValue)
        SetDec( newDecValue );
    }
    /// <summary>
    /// Increment the number
    /// </summary>
    public void Dec( )
    {
      int newDecValue = GetDec( ) - 1;
      // Underflow stays at 0
      if (newDecValue >= 0)
        SetDec( newDecValue );
    }

    /// <summary>
    /// Adds an octValue to this instance
    /// </summary>
    /// <param name="decValue">A decimal value</param>
    public void AddDec( int decValue )
    {
      int newDecValue = GetDec( ) + decValue;
      // Overflow stays at max
      if (newDecValue <= _maxDecimalValue) SetDec( newDecValue );
      else SetDec( _maxDecimalValue );
    }

    /// <summary>
    /// Subracts a decimal number from this instance
    /// </summary>
    /// <param name="decValue">A decimal value</param>
    public void SubtractDec( int decValue )
    {
      int newDecValue = GetDec( ) - decValue;
      // Underflow stays at 0
      if (newDecValue >= 0) SetDec( newDecValue );
      else SetDec( 0 );
    }

    /// <summary>
    /// Adds an octValue to this instance
    /// </summary>
    /// <param name="octValue">An 'octal' value</param>
    public void AddOct( int octValue )
    {
      Octal toAdd = new Octal( _digits, octValue );
      var decValToAdd = toAdd.GetDec( );
      this.AddDec( decValToAdd );
    }

    /// <summary>
    /// Subracts an octValue from this instance
    /// </summary>
    /// <param name="octValue">An 'octal' value</param>
    public void SubtractOct( int octValue )
    {
      Octal toAdd = new Octal( _digits, octValue );
      var decValToSub = toAdd.GetDec( );
      this.SubtractDec( decValToSub );
    }

    // with turnaround at boundaries

    /// <summary>
    /// Increment the number turn around at max
    /// </summary>
    public void IncT( )
    {
      int newDecValue = GetDec( ) + 1;
      // Overflow goes 0
      if (newDecValue <= _maxDecimalValue) SetDec( newDecValue );
      else SetDec( 0 );
    }
    /// <summary>
    /// Increment the number turn around at 0
    /// </summary>
    public void DecT( )
    {
      int newDecValue = GetDec( ) - 1;
      // Underflow goes max
      if (newDecValue >= 0) SetDec( newDecValue );
      else SetDec( _maxDecimalValue );
    }


    /// <inheritdoc/>
    public override string ToString( )
    {
      return GetOct( ).ToString( );
    }

  }
}
