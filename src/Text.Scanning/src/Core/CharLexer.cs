namespace Text.Scanning.Core
{
    public class CharLexer : Lexer<CharToken>
    {
        public override CharToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            CharToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'CHAR'");
        }

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