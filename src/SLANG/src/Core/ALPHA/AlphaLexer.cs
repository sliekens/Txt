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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alphaAlternativeLexer">%x41-5A / %x61-7A</param>
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
            Alternative result;
            if (this.alphaAlternativeLexer.TryRead(scanner, out result))
            {
                element = new Alpha(result);
                return true;
            }

            element = default(Alpha);
            return false;
        }
    }
}