namespace Text.Scanning.Core
{
    public class VCharLexer : Lexer<VCharToken>
    {
        public override VCharToken Read(ITextScanner scanner)
        {
            VCharToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'VCHAR'");
        }

        public override bool TryRead(ITextScanner scanner, out VCharToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(VCharToken);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0021'; c < '\u007E'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new VCharToken(c, context);
                    return true;
                }
            }

            token = default(VCharToken);
            return false;
        }
    }
}