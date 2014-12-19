namespace Text.Scanning.Core
{
    public class HTabLexer : Lexer<HTabToken>
    {
        public override HTabToken Read(ITextScanner scanner)
        {
            HTabToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'HTAB'");
        }

        public override bool TryRead(ITextScanner scanner, out HTabToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(HTabToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\t'))
            {
                token = new HTabToken(context);
                return true;
            }

            token = default(HTabToken);
            return false;
        }
    }
}