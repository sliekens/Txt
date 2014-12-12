namespace Text.Core
{
    public class DQuoteLexer : Lexer<DQuoteToken>
    {
        public DQuoteLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override DQuoteToken Read()
        {
            var context = this.Scanner.GetContext();
            DQuoteToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected DQUOTE", context);
        }

        public override bool TryRead(out DQuoteToken token)
        {
            token = default(DQuoteToken);
            var context = this.Scanner.GetContext();
            if (this.Scanner.TryMatch('\"'))
            {
                token = new DQuoteToken(context);
                return true;
            }

            return false;
        }
    }
}
