using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class HTabLexer : Lexer<HTabToken>
    {
        public HTabLexer(ITextScanner scanner)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
        }

        public override HTabToken Read()
        {
            HTabToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(this.Scanner.GetContext(), "Expected 'HTAB'");
        }

        public override bool TryRead(out HTabToken token)
        {
            var context = this.Scanner.GetContext();
            if (this.Scanner.TryMatch('\t'))
            {
                token = new HTabToken(context);
                return true;
            }

            token = default(HTabToken);
            return false;
        }
    }
}
