// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineFeedLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class LineFeedLexer : Lexer<LineFeed>
    {
        /// <summary>Initializes a new instance of the <see cref="LineFeedLexer"/> class.</summary>
        public LineFeedLexer()
            : base("LF")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LineFeed element)
        {
            if (scanner.EndOfInput)
            {
                element = default(LineFeed);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\x0A'))
            {
                element = new LineFeed(context);
                return true;
            }

            element = default(LineFeed);
            return false;
        }
    }
}