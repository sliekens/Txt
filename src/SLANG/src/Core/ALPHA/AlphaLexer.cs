// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class AlphaLexer : Lexer<Alpha>
    {
        private readonly ILexer<Alternative> alphaAlternativeLexer;

        public AlphaLexer(ILexer<Alternative> alphaAlternativeLexer)
            : base("ALPHA")
        {
            if (alphaAlternativeLexer == null)
            {
                throw new ArgumentNullException("alphaAlternativeLexer", "Precondition: alphaAlternativeLexer != null");
            }

            this.alphaAlternativeLexer = alphaAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Alpha element)
        {
            Element terminal;
            if (this.alphaAlternativeLexer.TryReadElement(scanner, out terminal))
            {
                element = new Alpha(terminal);
                return true;
            }

            element = default(Alpha);
            return false;
        }
    }
}