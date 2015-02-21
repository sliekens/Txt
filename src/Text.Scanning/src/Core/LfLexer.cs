namespace Text.Scanning.Core
{
    public class LfLexer : Lexer<LfToken>
    {
        /// <inheritdoc />
        public override LfToken Read(ITextScanner scanner)
        {
            LfToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'LF'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LfToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(LfToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u000A'))
            {
                token = new LfToken(context);
                return true;
            }

            token = default(LfToken);
            return false;
        }
    }
}