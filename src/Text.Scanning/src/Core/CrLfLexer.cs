namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class CrLfLexer : Lexer<CrLfToken>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<CrToken> crLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<LfToken> lfLexer;

        public CrLfLexer()
            : this(new CrLexer(), new LfLexer())
        {
        }

        public CrLfLexer(ILexer<CrToken> crLexer, ILexer<LfToken> lfLexer)
        {
            Contract.Requires(crLexer != null);
            Contract.Requires(lfLexer != null);
            this.crLexer = crLexer;
            this.lfLexer = lfLexer;
        }

        public override CrLfToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            try
            {
                return new CrLfToken(this.crLexer.Read(scanner), this.lfLexer.Read(scanner), context);
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException(context, "Expected 'CR LF'", syntaxErrorException);
            }
        }

        public override bool TryRead(ITextScanner scanner, out CrLfToken token)
        {
            var context = scanner.GetContext();
            CrToken crToken;
            if (scanner.EndOfInput || !this.crLexer.TryRead(scanner, out crToken))
            {
                token = default(CrLfToken);
                return false;
            }

            LfToken lfToken;
            if (scanner.EndOfInput || !this.lfLexer.TryRead(scanner, out lfToken))
            {
                this.crLexer.PutBack(scanner, crToken);
                token = default(CrLfToken);
                return false;
            }

            token = new CrLfToken(crToken, lfToken, context);
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