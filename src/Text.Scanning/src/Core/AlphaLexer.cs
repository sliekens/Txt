namespace Text.Scanning.Core
{
    /// <summary>A-Z / a-z</summary>
    public class AlphaLexer : Lexer<AlphaToken>
    {
        public override AlphaToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            AlphaToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'ALPHA'");
        }

        public override bool TryRead(ITextScanner scanner, out AlphaToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(AlphaToken);
                return false;
            }

            var context = scanner.GetContext();
            return this.TryReadLowercase(scanner, context, out token) ||
                   this.TryReadUppercase(scanner, context, out token);
        }

        private bool TryReadLowercase(ITextScanner scanner, ITextContext context, out AlphaToken token)
        {
            for (var c = 'a'; c <= 'z'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new AlphaToken(c, context);
                    return true;
                }
            }

            token = default(AlphaToken);
            return false;
        }

        private bool TryReadUppercase(ITextScanner scanner, ITextContext context, out AlphaToken token)
        {
            for (var c = 'A'; c <= 'Z'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new AlphaToken(c, context);
                    return true;
                }
            }

            token = default(AlphaToken);
            return false;
        }
    }
}