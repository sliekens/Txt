namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class CrLfLexer : Lexer<CrLfElement>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<CrElement> crLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<LfElement> lfLexer;

        public CrLfLexer()
            : this(new CrLexer(), new LfLexer())
        {
        }

        public CrLfLexer(ILexer<CrElement> crLexer, ILexer<LfElement> lfLexer)
        {
            Contract.Requires(crLexer != null);
            Contract.Requires(lfLexer != null);
            this.crLexer = crLexer;
            this.lfLexer = lfLexer;
        }

        /// <inheritdoc />
        public override CrLfElement Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            try
            {
                var cr = this.crLexer.Read(scanner);
                var lf = this.lfLexer.Read(scanner);
                Contract.Assume(lf.Offset == cr.Offset + 1);
                return new CrLfElement(cr, lf, context);
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException(context, "Expected 'CRLF'", syntaxErrorException);
            }
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CrLfElement element)
        {
            var context = scanner.GetContext();
            CrElement crElement;
            if (scanner.EndOfInput || !this.crLexer.TryRead(scanner, out crElement))
            {
                element = default(CrLfElement);
                return false;
            }

            LfElement lfElement;
            if (scanner.EndOfInput || !this.lfLexer.TryRead(scanner, out lfElement))
            {
                this.crLexer.PutBack(scanner, crElement);
                element = default(CrLfElement);
                return false;
            }

            Contract.Assume(lfElement.Offset == crElement.Offset + 1);
            element = new CrLfElement(crElement, lfElement, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.crLexer != null);
            Contract.Invariant(this.lfLexer != null);
        }
    }
}