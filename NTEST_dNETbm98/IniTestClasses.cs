using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dNetBm98.IniLib;

namespace NTEST_dNETbm98
{
  /// <summary>
  /// Test Classes for the INI serializer
  /// </summary>
  internal class IniTestMain
  {
    // Main Section
    [IniFileKey( "M_K1" )]
    public string K1_string { get; set; }

    [IniFileKey( "M_K2" )]
    public int K2_int { get; set; }

    [IniFileKey( "M_K3" )]
    public float K3_float { get; set; }


    [IniFileSection( "Section1" )]
    public IniTestSect1 S1 { get; set; } = new IniTestSect1( );
    [IniFileSection( "Section2" )]
    public IniTestSect2 S2 { get; set; } = new IniTestSect2( );

  }

  internal class IniTestSect1
  {
    [IniFileKey( "S1_K1" )]
    public string K1_string { get; set; }

    [IniFileKey( "S1_K2" )]
    public int K2_int { get; set; }

    [IniFileKey( "S1_K3" )]
    public double K3_double { get; set; }

  }

  internal class IniTestSect2
  {
    [IniFileKey( "S2_K1" )]
    public string K1_string { get; set; }

    [IniFileKey( "S2_K2" )]
    public int K2_int { get; set; }

    [IniFileKey( "S2_K3" )]
    public double K3_double { get; set; }

    [IniFileKey( "S2_K4." )]
    public Dictionary<string, string> K4_dict { get; set; }
  }

  // Test Factory
  internal static class IniTest
  {
    public static IniTestMain IniDoc1( )
    {
      return new IniTestMain( ) {
        K1_string = "Main Section String",
        K2_int = 123,
        K3_float = 123.456f,
        S1 = new IniTestSect1( ) { K1_string = "Section 1 String", K2_int = 12345, K3_double = 12345.456 },
        S2 = new IniTestSect2( ) {
          K1_string = "Section 2 String",
          K2_int = 1234567,
          K3_double = 1234567.456,
          K4_dict = new Dictionary<string, string>( ) {
            { "S2_K4.0", "Entry 0" },
            { "S2_K4.1", "Entry 1" } ,
            { "S2_K4.2", "Entry 2" },
            { "S2_K4.3", "Entry 3" },
          }
        },
      };
    }
    public static string IniTestFile( )
    {
      return
@"
; Section MAIN comment
M_K1=Main Section String  ; Comment 
M_K2=123  ; Comment 
M_K3=123.456  ; Comment 

[Section1]
; Section 1 comment
S1_K1=Section 1 String  ; Comment 
S1_K2=12345  ; Comment 
S1_K3=12345.456  ; Comment 

[Section2]
; Section 2 comment
S2_K1=Section 2 String  ; Comment 
S2_K2=1234567  ; Comment 
S2_K3=1234567.456

S2_K4.0=Entry 0  ; Comment 
S2_K4.1=Entry 1  ; Comment 
S2_K4.2=Entry 2  ; Comment 
S2_K4.3=Entry 3  ; Comment 

";
    }
    public static string IniTestFileQuoted( )
    {
      return
@"
; Section MAIN comment
M_K1=""Main Section String""  ; Comment 
M_K2=123  ; Comment 
M_K3=123.456  ; Comment 

[Section1]
; Section 1 comment
S1_K1=""Section 1 String""  ; Comment 
S1_K2=12345  ; Comment 
S1_K3=12345.456

[Section2]
; Section 2 comment
S2_K1=""Section 2 String""  ; Comment 
S2_K2=1234567  ; Comment 
S2_K3=1234567.456

S2_K4.0=""Entry 0""  ; Comment 
S2_K4.1=""Entry 1""  ; Comment 
S2_K4.2=""Entry 2""
S2_K4.3=""Entry 3""  ; Comment 

";

    }


  }
}
