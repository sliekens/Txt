namespace Text.Scanning.Core
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

            throw new SyntaxErrorException("Expected 'CTL'", context);
        }

        public override bool TryRead(out CtlToken token)
        {
            var context = this.Scanner.GetContext();

            // %x00-1F
            for (int i = 0x00; i <= 0x1F; i++)
            {
                var c = (char)i;
                if (this.Scanner.TryMatch(c))
                {
                    token = new CtlToken(c, context);
                    return true;
                }
            }

            // %x7F
            if (this.Scanner.TryMatch((char)0x7F))
            {
                token = new CtlToken((char)0x7F, context);
                return true;
            }

            token = default(CtlToken);
            return false;
        }
    }
}
