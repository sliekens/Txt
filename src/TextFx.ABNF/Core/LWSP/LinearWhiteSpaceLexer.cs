// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;

    [RuleName("LWSP")]
    public class LinearWhiteSpaceLexer : Lexer<LinearWhiteSpace>
    {
        private readonly ILexer<Repetition> innerLexer;

        public LinearWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out LinearWhiteSpace element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, previousElementOrNull, out result))
            {
                element = new LinearWhiteSpace(result);
                if (previousElementOrNull != null)
                {
                    element.PreviousElement = previousElementOrNull;
                    previousElementOrNull.NextElement = element;
                }

                return true;
            }

            element = default(LinearWhiteSpace);
            return false;
        }
    }
}