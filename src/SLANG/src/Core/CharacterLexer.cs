// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>TODO </summary>
    public class CharacterLexer : Lexer<Character>
    {
        /// <summary>Initializes a new instance of the <see cref="CharacterLexer"/> class.</summary>
        public CharacterLexer()
            : base("CHAR")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Character element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Character);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0001'; c <= '\u007F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Character(c, context);
                    return true;
                }
            }

            element = default(Character);
            return false;
        }
    }
}