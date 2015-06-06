// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.CHAR
{
    using System;

    [RuleName("CHAR")]
    public class CharacterLexer : Lexer<Character>
    {
        private readonly ILexer<Terminal> characterValueRangeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterValueRangeLexer">%x01-7F</param>
        public CharacterLexer(ILexer<Terminal> characterValueRangeLexer)
        {
            if (characterValueRangeLexer == null)
            {
                throw new ArgumentNullException("characterValueRangeLexer", "Precondition: characterValueRangeLexer != null");
            }

            this.characterValueRangeLexer = characterValueRangeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Character element)
        {
            Terminal result;
            if (this.characterValueRangeLexer.TryRead(scanner, out result))
            {
                element = new Character(result);
                return true;
            }

            element = default(Character);
            return false;
        }
    }
}
