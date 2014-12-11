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
            foreach (var digit in new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.Where(digit => this.Scanner.TryMatch(digit)))
            {
                token = new DigitToken(char.ToString(digit), context);
                return true;
            }

            return false;
        }
    }
}
