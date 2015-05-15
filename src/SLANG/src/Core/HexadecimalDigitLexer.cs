// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class HexadecimalDigitLexer : AlternativeLexer<HexadecimalDigit, Digit, HexadecimalDigit.A, HexadecimalDigit.B, HexadecimalDigit.C, HexadecimalDigit.D, HexadecimalDigit.E, HexadecimalDigit.F>
    {
        public HexadecimalDigitLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "HEXDIG")
        {   
        }
    }
}