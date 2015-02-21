namespace Text.Scanning.Core
{
    public class DQuoteLexer : Lexer<DQuoteElement>
    {
        /// <inheritdoc />
        public override DQuoteElement Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            DQuoteElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'DQUOTE'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out DQuoteElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(DQuoteElement);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\"'))
            {
                element = new DQuoteElement(context);
                return true;
            }

            element = default(DQuoteElement);
            return false;
        }
    }
}