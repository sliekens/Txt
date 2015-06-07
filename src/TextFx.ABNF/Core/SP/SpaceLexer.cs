// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;

    [RuleName("SP")]
    public class SpaceLexer : Lexer<Space>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x20</param>
        public SpaceLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Space element)
        {
            Terminal result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new Space(result);
                return true;
            }

            element = default(Space);
            return false;
        }
    }
}