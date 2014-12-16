namespace Text.Scanning.Core
{
    /// <summary>A-Z / a-z</summary>
    public class AlphaLexer : Lexer<AlphaToken>
    {
        public AlphaLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override AlphaToken Read()
        {
            var context = this.Scanner.GetContext();
            AlphaToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected 'ALPHA'", context);
        }

        public override bool TryRead(out AlphaToken token)
        {
            token = default(AlphaToken);
            var context = this.Scanner.GetContext();
            for (int i = 'A'; i <= 'Z'; i++)
            {
                var c = (char) i;
                if (this.Scanner.TryMatch(c))
                {
                    token = new AlphaToken(c, context);
                    return true;
                }
            }

            for (int i = 'a'; i <= 'z'; i++)
            {
                var c = (char)i;
                if (this.Scanner.TryMatch(c))
                {
                    token = new AlphaToken(c, context);
                    return true;
                }
            }

            return false;
        }
    }
}
