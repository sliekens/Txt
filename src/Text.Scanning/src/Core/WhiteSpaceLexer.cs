namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class WhiteSpaceLexer : Lexer<WhiteSpace>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HorizontalTabLexer horizontalTabLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpaceLexer spaceLexer;

        public WhiteSpaceLexer()
            : this(new SpaceLexer(), new HorizontalTabLexer())
        {
        }

        public WhiteSpaceLexer(SpaceLexer spaceLexer, HorizontalTabLexer horizontalTabLexer)
        {
            Contract.Requires(spaceLexer != null);
            Contract.Requires(horizontalTabLexer != null);
            this.spaceLexer = spaceLexer;
            this.horizontalTabLexer = horizontalTabLexer;
        }

        /// <inheritdoc />
        public override WhiteSpace Read(ITextScanner scanner)
        {
            WhiteSpace element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'WSP'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out WhiteSpace element)
        {
            if (scanner.EndOfInput)
            {
                element = default(WhiteSpace);
                return false;
            }

            var context = scanner.GetContext();
            Space space;
            HorizontalTab horizontalTab;
            if (this.spaceLexer.TryRead(scanner, out space))
            {
                element = new WhiteSpace(space, context);
                return true;
            }

            if (this.horizontalTabLexer.TryRead(scanner, out horizontalTab))
            {
                element = new WhiteSpace(horizontalTab, context);
                return true;
            }

            element = default(WhiteSpace);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.spaceLexer != null);
            Contract.Invariant(this.horizontalTabLexer != null);
        }
    }
}