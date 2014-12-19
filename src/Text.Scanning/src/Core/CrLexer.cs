namespace Text.Scanning.Core
{
    public class CrLexer : Lexer<CrToken>
    {
        public override CrToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            CrToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'CR'");
        }

        public override bool TryRead(ITextScanner scanner, out CrToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(CrToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\r'))
            {
                token = new CrToken(context);
                return true;
            }

            token = default(CrToken);
            return false;
        }
    }
}