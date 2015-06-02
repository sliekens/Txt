// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.BIT
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
            Alternative result;
            if (this.bitAlternativeLexer.TryRead(scanner, out result))
            {
                element = new Bit(result);
                return true;
            }

            element = default(Bit);
            return false;
        }
    }
}