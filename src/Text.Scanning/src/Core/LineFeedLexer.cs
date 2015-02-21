namespace Text.Scanning.Core
{
    public class LineFeedLexer : Lexer<LineFeed>
    {
        /// <inheritdoc />
        public override LineFeed Read(ITextScanner scanner)
        {
            LineFeed element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'LF'");
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
            if (scanner.TryMatch('\u000A'))
            {
                element = new LineFeed(context);
                return true;
            }

            element = default(LineFeed);
            return false;
        }
    }
}