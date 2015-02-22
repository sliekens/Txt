// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    /// <summary>TODO </summary>
    public class DigitLexer : Lexer<Digit>
    {
        /// <summary>Initializes a new instance of the <see cref="DigitLexer"/> class.</summary>
        public DigitLexer()
            : base("DIGIT")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Digit element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Digit);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '0'; c <= '9'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Digit(c, context);
                    return true;
                }
            }

            element = default(Digit);
            return false;
        }
    }
}