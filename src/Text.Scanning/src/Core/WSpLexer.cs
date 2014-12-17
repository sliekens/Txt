using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class WSpLexer : Lexer<WSpToken>
    {
        private readonly SpLexer spLexer;
        private readonly HTabLexer hTabLexer;

        public WSpLexer(ITextScanner scanner)
            : this(scanner, new SpLexer(scanner), new HTabLexer(scanner))
        {
            Contract.Requires(scanner != null);
        }

        public WSpLexer(ITextScanner scanner, SpLexer spLexer, HTabLexer hTabLexer)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
            Contract.Requires(spLexer != null);
            Contract.Requires(hTabLexer != null);
            this.spLexer = spLexer;
            this.hTabLexer = hTabLexer;
        }

        public override WSpToken Read()
        {
            WSpToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(this.Scanner.GetContext(), "Expected 'WSP'");
        }

        public override bool TryRead(out WSpToken token)
        {
            var context = this.Scanner.GetContext();
            SpToken spToken;
            HTabToken hTabToken;
            if (this.spLexer.TryRead(out spToken))
            {
                token = new WSpToken(spToken, context);
                return true;
            }

            if (this.hTabLexer.TryRead(out hTabToken))
            {
                token = new WSpToken(hTabToken, context);
                return true;
            }

            token = default(WSpToken);
            return false;
        }
    }
}
