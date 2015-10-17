// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineFeedLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;

    [RuleName("LF")]
    public class LineFeedLexer : Lexer<LineFeed>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x0A</param>
        public LineFeedLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out LineFeed element)
        {
            Terminal result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new LineFeed(result);
                if (previousElementOrNull != null)
                {
                    element.PreviousElement = previousElementOrNull;
                    previousElementOrNull.NextElement = element;
                }

                return true;
            }

            element = default(LineFeed);
            return false;
        }
    }
}