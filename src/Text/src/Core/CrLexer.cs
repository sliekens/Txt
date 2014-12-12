namespace Text.Core
{
    public class CrLexer : Lexer<CrToken>
    {
        public CrLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override CrToken Read()
        {
            var context = this.Scanner.GetContext();
            CrToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected CR", context);
        }

        public override bool TryRead(out CrToken token)
        {
            token = default(CrToken);
            var context = this.Scanner.GetContext();
            if (this.Scanner.TryMatch('\r'))
            {
                token = new CrToken("\r", context);
                return true;
            }

            return false;
        }
    }
}
