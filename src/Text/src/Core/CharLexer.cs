namespace Text.Core
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

            throw new SyntaxErrorException("Expected CHAR", context);
        }

        public override bool TryRead(out CharToken token)
        {
            token = default(CharToken);
            var context = this.Scanner.GetContext();
            for (var i = 0x01; i <= 0x7F; i++)
            {
                var character = (char) i;
                if (this.Scanner.TryMatch(character))
                {
                    token = new CharToken(char.ToString(character), context);
                    return true;
                }
            }

            return false;
        }
    }
}
