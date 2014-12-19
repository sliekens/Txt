namespace Text.Scanning.Core
{
    public class LfLexer : Lexer<LfToken>
    {
        public override LfToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            LfToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'LF'");
        }

        public override bool TryRead(ITextScanner scanner, out LfToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(LfToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\n'))
            {
                token = new LfToken(context);
                return true;
            }

            token = default(LfToken);
            return false;
        }
    }
}