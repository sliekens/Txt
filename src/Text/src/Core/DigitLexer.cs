using System.Linq;

namespace Text.Core
{
    public class DigitLexer : Lexer<DigitToken>
    {
        public DigitLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override DigitToken Read()
        {
            var context = this.Scanner.GetContext();
            DigitToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected 'DIGIT'", context);
        }

        public override bool TryRead(out DigitToken token)
        {
            token = default(DigitToken);
            var context = this.Scanner.GetContext();
            for (int i = '0'; i <= '9'; i++)
            {
                var c = (char)i;
                if (this.Scanner.TryMatch(c))
                {
                    token = new DigitToken(char.ToString(c), context);
                    return true;
                }
            }

            return false;
        }
    }
}
