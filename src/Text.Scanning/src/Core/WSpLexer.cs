namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class WSpLexer : Lexer<WSpElement>
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

        /// <inheritdoc />
        public override WSpElement Read(ITextScanner scanner)
        {
            WSpElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'WSP'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out WSpElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(WSpElement);
                return false;
            }

            var context = scanner.GetContext();
            SpElement spElement;
            HTabElement hTabElement;
            if (this.spLexer.TryRead(scanner, out spElement))
            {
                element = new WSpElement(spElement, context);
                return true;
            }

            if (this.hTabLexer.TryRead(scanner, out hTabElement))
            {
                element = new WSpElement(hTabElement, context);
                return true;
            }

            element = default(WSpElement);
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