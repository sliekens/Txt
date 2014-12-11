using System.Linq;

namespace Text.Core
{
    /// <summary>A-Z / a-z</summary>
    public class AlphaLexer : Lexer<AlphaToken>
    {
        public AlphaLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override AlphaToken Read()
        {
            var context = this.Scanner.GetContext();
            AlphaToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected 'ALPHA'", context);
        }

        public override bool TryRead(out AlphaToken token)
        {
            token = default(AlphaToken);
            var context = this.Scanner.GetContext();
            var upcase = Enumerable.Range(0x41, 26).Select(i => (char)i);
            var locase = Enumerable.Range(0x61, 26).Select(i => (char)i);
            foreach (var c in upcase.Concat(locase).Where(this.Scanner.TryMatch))
            {
                token = new AlphaToken(char.ToString(c), context);
                return true;
            }

            return false;
        }
    }
}
