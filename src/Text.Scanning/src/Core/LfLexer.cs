namespace Text.Scanning.Core
{
    public class LfLexer : Lexer<LfToken>
    {
        public LfLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override LfToken Read()
        {
            var context = this.Scanner.GetContext();
            LfToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'LF'");
        }

        public override bool TryRead(out LfToken token)
        {
            var context = this.Scanner.GetContext();
            if (this.Scanner.TryMatch('\n'))
            {
                token = new LfToken(context);
                return true;
            }

            token = default(LfToken);
            return false;
        }
    }
}
