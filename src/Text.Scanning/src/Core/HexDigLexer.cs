namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class HexDigLexer : Lexer<HexDigElement>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<DigitElement> digitLexer;

        public HexDigLexer()
            : this(new DigitLexer())
        {
        }

        public HexDigLexer(ILexer<DigitElement> digitLexer)
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        /// <inheritdoc />
        public override HexDigElement Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HexDigElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'HEXDIG'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HexDigElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HexDigElement);
                return false;
            }

            var context = scanner.GetContext();
            DigitElement digitElement;
            if (this.digitLexer.TryRead(scanner, out digitElement))
            {
                element = new HexDigElement(digitElement, context);
                return true;
            }

            // A-F
            for (var c = 'A'; c <= 'F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new HexDigElement(c, context);
                    return true;
                }
            }

            // a-f
            for (var c = 'a'; c <= 'f'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new HexDigElement(c, context);
                    return true;
                }
            }

            element = default(HexDigElement);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}