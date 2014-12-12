namespace Text.Core
{
    public class HexDigLexer : Lexer<HexDigToken>
    {
        private readonly ILexer<DigitToken> digitLexer;

        public HexDigLexer(ITextScanner scanner)
            : this(scanner, new DigitLexer(scanner))
        {
        }

        public HexDigLexer(ITextScanner scanner, ILexer<DigitToken> digitLexer)
            : base(scanner)
        {
            this.digitLexer = digitLexer;
        }

        public override HexDigToken Read()
        {
            var context = this.Scanner.GetContext();
            HexDigToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected 'HEXDIG'", context);
        }

        public override bool TryRead(out HexDigToken token)
        {
            var context = this.Scanner.GetContext();
            DigitToken digitToken;
            if (this.digitLexer.TryRead(out digitToken))
            {
                token = new HexDigToken(digitToken, context);
                return true;
            }

            for (int i = 'A'; i <= 'F'; i++)
            {
                var c = (char) i;
                if (this.Scanner.TryMatch(c))
                {
                    token = new HexDigToken(char.ToString(c), context);
                    return true;
                }
            }

            token = default(HexDigToken);
            return false;
        }
    }
}
