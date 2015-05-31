// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class CharacterLexer : Lexer<Character>
    {
        private readonly ILexer<Element> characterValueRangeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterValueRangeLexer">%x01-7F</param>
        public CharacterLexer(ILexer<Element> characterValueRangeLexer)
            : base("CHAR")
        {
            if (characterValueRangeLexer == null)
            {
                throw new ArgumentNullException("characterValueRangeLexer", "Precondition: characterValueRangeLexer != null");
            }

            this.characterValueRangeLexer = characterValueRangeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Character element)
        {
            Element value;
            if (this.characterValueRangeLexer.TryRead(scanner, out value))
            {
                element = new Character(value);
                return true;
            }

            element = default(Character);
            return false;
        }
    }
}
