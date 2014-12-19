namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class WSpLexer : Lexer<WSpToken>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HTabLexer hTabLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpLexer spLexer;

        public WSpLexer()
            : this(new SpLexer(), new HTabLexer())
        {
        }

        public WSpLexer(SpLexer spLexer, HTabLexer hTabLexer)
        {
            Contract.Requires(spLexer != null);
            Contract.Requires(hTabLexer != null);
            this.spLexer = spLexer;
            this.hTabLexer = hTabLexer;
        }

        public override WSpToken Read(ITextScanner scanner)
        {
            WSpToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'WSP'");
        }

        public override bool TryRead(ITextScanner scanner, out WSpToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(WSpToken);
                return false;
            }

            var context = scanner.GetContext();
            SpToken spToken;
            HTabToken hTabToken;
            if (this.spLexer.TryRead(scanner, out spToken))
            {
                token = new WSpToken(spToken, context);
                return true;
            }

            if (this.hTabLexer.TryRead(scanner, out hTabToken))
            {
                token = new WSpToken(hTabToken, context);
                return true;
            }

            token = default(WSpToken);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.spLexer != null);
            Contract.Invariant(this.hTabLexer != null);
        }
    }
}