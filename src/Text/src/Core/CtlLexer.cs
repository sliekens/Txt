namespace Text.Core
{
    public class CtlLexer : Lexer<CtlToken>
    {
        public CtlLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override CtlToken Read()
        {
            var context = this.Scanner.GetContext();
            CtlToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected CTL", context);
        }

        public override bool TryRead(out CtlToken token)
        {
            token = default(CtlToken);
            var context = this.Scanner.GetContext();
            for (int i = 0x01; i <= 0x7F; i++)
            {
                var c = (char)i;
                if (this.Scanner.TryMatch(c))
                {
                    token = new CtlToken(char.ToString(c), context);
                    return true;
                }
            }

            return false;
        }
    }
}
