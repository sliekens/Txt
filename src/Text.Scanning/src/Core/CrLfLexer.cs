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

        public CrLfLexer(ITextScanner scanner)
            : this(scanner, new CrLexer(scanner), new LfLexer(scanner))
        {
            Contract.Requires(scanner != null);
        }

        public CrLfLexer(ITextScanner scanner, ILexer<CrToken> crLexer, ILexer<LfToken> lfLexer)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
            this.crLexer = crLexer;
            this.lfLexer = lfLexer;
        }

        public override CrLfToken Read()
        {
            var context = this.Scanner.GetContext();
            try
            {
                return new CrLfToken(this.crLexer.Read(), this.lfLexer.Read(), context);
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException(context, "Expected 'CR LF'", syntaxErrorException);
            }
        }

        public override bool TryRead(out CrLfToken token)
        {
            var context = this.Scanner.GetContext();
            CrToken crToken;
            if (this.Scanner.EndOfInput || !this.crLexer.TryRead(out crToken))
            {
                token = default(CrLfToken);
                return false;
            }

            LfToken lfToken;
            if (this.Scanner.EndOfInput || !this.lfLexer.TryRead(out lfToken))
            {
                this.crLexer.PutBack(crToken);
                token = default(CrLfToken);
                return false;
            }

            token = new CrLfToken(crToken, lfToken, context);
            return true;
        }
    }
}