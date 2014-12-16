namespace Text.Scanning.Core
{
    public class CharLexer : Lexer<CharToken>
    {
        public CharLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override CharToken Read()
        {
            var context = this.Scanner.GetContext();
            CharToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected 'CHAR'", context);
        }

        public override bool TryRead(out CharToken token)
        {
            var context = this.Scanner.GetContext();
            for (var i = 0x01; i <= 0x7F; i++)
            {
                var c = (char) i;
                if (this.Scanner.TryMatch(c))
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
