namespace Text.Scanning.Core
{
    public class DigitLexer : Lexer<DigitToken>
    {
        public DigitLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override DigitToken Read()
        {
            var context = this.Scanner.GetContext();
            DigitToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'DIGIT'");
        }

        public override bool TryRead(out DigitToken token)
        {
            token = default(DigitToken);
            var context = this.Scanner.GetContext();
            for (var c = '0'; c <= '9'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new DigitToken(c, context);
                    return true;
                }
            }

            return false;
        }
    }
}
