namespace Text.Scanning.Core
{
    public class CharLexer : Lexer<CharToken>
    {
        /// <inheritdoc />
        public override CharToken Read(ITextScanner scanner)
        {
            CharToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'CHAR'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CharToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(CharToken);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0001'; c <= '\u007F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new CharToken(c, context);
                    return true;
                }
            }

            token = default(CharToken);
            return false;
        }
    }
}