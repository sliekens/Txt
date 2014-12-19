namespace Text.Scanning.Core
{
    public class OctetLexer : Lexer<OctetToken>
    {
        public override OctetToken Read(ITextScanner scanner)
        {
            OctetToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'OCTET'");
        }

        public override bool TryRead(ITextScanner scanner, out OctetToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(OctetToken);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0000'; c <= '\u00FF'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new OctetToken(c, context);
                    return true;
                }
            }

            token = default(OctetToken);
            return false;
        }
    }
}