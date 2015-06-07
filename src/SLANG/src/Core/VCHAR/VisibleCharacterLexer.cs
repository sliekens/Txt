// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core
{
    using System;

    [RuleName("VCHAR")]
    public class VisibleCharacterLexer : Lexer<VisibleCharacter>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x21-7E</param>
        public VisibleCharacterLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out VisibleCharacter element)
        {
            Terminal result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new VisibleCharacter(result);
                return true;
            }

            element = default(VisibleCharacter);
            return false;
        }
    }
}