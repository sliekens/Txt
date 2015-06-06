// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core
{
    using System;

    [RuleName("HEXDIG")]
    public class HexadecimalDigitLexer : Lexer<HexadecimalDigit>
    {
        private readonly ILexer<Alternative> hexadecimalDigitAlternativeLexer;

        /// <summary>
        /// </summary>
        /// <param name="hexadecimalDigitAlternativeLexer">DIGIT / "A" / "B" / "C" / "D" / "E" / "F"</param>
        public HexadecimalDigitLexer(ILexer<Alternative> hexadecimalDigitAlternativeLexer)
        {
            if (hexadecimalDigitAlternativeLexer == null)
            {
                throw new ArgumentNullException(
                    "hexadecimalDigitAlternativeLexer",
                    "Precondition: hexadecimalDigitAlternativeLexer != null");
            }

            this.hexadecimalDigitAlternativeLexer = hexadecimalDigitAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HexadecimalDigit element)
        {
            Alternative result;
            if (this.hexadecimalDigitAlternativeLexer.TryRead(scanner, out result))
            {
                element = new HexadecimalDigit(result);
                return true;
            }

            element = default(HexadecimalDigit);
            return false;
        }
    }
}