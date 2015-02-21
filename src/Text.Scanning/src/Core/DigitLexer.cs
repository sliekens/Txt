namespace Text.Scanning.Core
{
    public class DigitLexer : Lexer<DigitToken>
    {
        /// <inheritdoc />
        public override DigitToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            DigitToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'DIGIT'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out DigitToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(DigitToken);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '0'; c <= '9'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new DigitToken(c, context);
                    return true;
                }
            }

            token = default(DigitToken);
            return false;
        }
    }
}