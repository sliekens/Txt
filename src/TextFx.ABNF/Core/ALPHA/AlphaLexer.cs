// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;

    [RuleName("ALPHA")]
    public class AlphaLexer : Lexer<Alpha>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x41-5A / %x61-7A</param>
        public AlphaLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Alpha element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Alpha(result);
                if (previousElementOrNull != null)
                {
                    element.PreviousElement = previousElementOrNull;
                    previousElementOrNull.NextElement = element;
                }

                return true;
            }

            element = default(Alpha);
            return false;
        }
    }
}