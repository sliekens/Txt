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

    public class HexadecimalDigitLexer : Lexer<HexadecimalDigit>
    {
        private readonly ILexer<Alternative> hexadecimalDigitAlternativeLexer;

        public HexadecimalDigitLexer(ILexer<Alternative> hexadecimalDigitAlternativeLexer)
            : base("HEXDIG")
        {
            if (hexadecimalDigitAlternativeLexer == null)
            {
                throw new ArgumentNullException("hexadecimalDigitAlternativeLexer", "Precondition: hexadecimalDigitAlternativeLexer != null");
            }

            this.hexadecimalDigitAlternativeLexer = hexadecimalDigitAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HexadecimalDigit element)
        {
            Element terminal;
            if (this.hexadecimalDigitAlternativeLexer.TryReadElement(scanner, out terminal))
            {
                element = new HexadecimalDigit(terminal);
                return true;
            }

            element = default(HexadecimalDigit);
            return false;
        }
    }
}