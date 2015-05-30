// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class BitLexer : Lexer<Bit>
    {
        private readonly ILexer<Alternative> bitAlternativeLexer;

        public BitLexer(ILexer<Alternative> bitAlternativeLexer)
            : base("BIT")
        {
            if (bitAlternativeLexer == null)
            {
                throw new ArgumentNullException("bitAlternativeLexer", "Precondition: bitAlternativeLexer != null");
            }

            this.bitAlternativeLexer = bitAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Bit element)
        {
            Element terminal;
            if (this.bitAlternativeLexer.TryReadElement(scanner, out terminal))
            {
                element = new Bit(terminal);
                return true;
            }

            element = default(Bit);
            return false;
        }
    }
}