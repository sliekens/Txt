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
        private readonly ILexer characterValueRangeLexer;

        public CharacterLexer(ILexer characterValueRangeLexer)
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
            if (this.characterValueRangeLexer.TryReadElement(scanner, out value))
            {
                element = new Character(value);
                return true;
            }

            element = default(Character);
            return false;
        }
    }
}
