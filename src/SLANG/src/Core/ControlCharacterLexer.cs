// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>TODO </summary>
    public class ControlCharacterLexer : Lexer<ControlCharacter>
    {
        /// <summary>Initializes a new instance of the <see cref="ControlCharacterLexer"/> class.</summary>
        public ControlCharacterLexer()
            : base("CTL")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out ControlCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default(ControlCharacter);
                return false;
            }

            var context = scanner.GetContext();

            // %x00-1F
            for (var c = '\x00'; c <= '\x1F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new ControlCharacter(c, context);
                    return true;
                }
            }

            // %x7F
            if (scanner.TryMatch('\x7F'))
            {
                element = new ControlCharacter('\x7F', context);
                return true;
            }

            element = default(ControlCharacter);
            return false;
        }
    }
}