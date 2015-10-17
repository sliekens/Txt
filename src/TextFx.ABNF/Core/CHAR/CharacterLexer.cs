// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;

    [RuleName("CHAR")]
    public class CharacterLexer : Lexer<Character>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x01-7F</param>
        public CharacterLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Character element)
        {
            Terminal result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Character(result);
                if (previousElementOrNull != null)
                {
                    element.PreviousElement = previousElementOrNull;
                    previousElementOrNull.NextElement = element;
                }

                return true;
            }

            element = default(Character);
            return false;
        }
    }
}