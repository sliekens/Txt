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

    public partial class CharacterLexer : Lexer<Character>
    {
        private readonly ILexer<Element> characterValueRangeLexer;

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

    public partial class CharacterLexer
    {
        public class CharacterValueRangeLexer : ValueRangeLexer
        {
            public CharacterValueRangeLexer()
                : base('\x01', '\x7F')
            {
            }
        }
    }
}
