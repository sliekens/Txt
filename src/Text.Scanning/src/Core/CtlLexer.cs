namespace Text.Scanning.Core
{
    public class CtlLexer : Lexer<CtlToken>
    {
        /// <inheritdoc />
        public override CtlToken Read(ITextScanner scanner)
        {
            CtlToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'CTL'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CtlToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(CtlToken);
                return false;
            }

            var context = scanner.GetContext();

            // %x00-1F
            for (var c = '\u0000'; c <= '\u001F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new CtlToken(c, context);
                    return true;
                }
            }

            // %x7F
            if (scanner.TryMatch('\u007F'))
            {
                token = new CtlToken('\u007F', context);
                return true;
            }

            token = default(CtlToken);
            return false;
        }
    }
}