namespace Text.Scanning.Core
{
    public class SpLexer : Lexer<SpToken>
    {
        public override SpToken Read(ITextScanner scanner)
        {
            SpToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'SP'");
        }

        public override bool TryRead(ITextScanner scanner, out SpToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(SpToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u0020'))
            {
                token = new SpToken(context);
                return true;
            }

            token = default(SpToken);
            return false;
        }
    }
}