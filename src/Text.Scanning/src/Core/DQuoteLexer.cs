namespace Text.Scanning.Core
{
    public class DQuoteLexer : Lexer<DQuoteToken>
    {
        public override DQuoteToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            DQuoteToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'DQUOTE'");
        }

        public override bool TryRead(ITextScanner scanner, out DQuoteToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(DQuoteToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\"'))
            {
                token = new DQuoteToken(context);
                return true;
            }

            token = default(DQuoteToken);
            return false;
        }
    }
}