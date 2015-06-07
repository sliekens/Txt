// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core
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

        public override bool TryRead(ITextScanner scanner, out LinearWhiteSpace element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new LinearWhiteSpace(result);
                return true;
            }

            element = default(LinearWhiteSpace);
            return false;
        }
    }
}