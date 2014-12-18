namespace Text.Scanning
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public abstract class Lexer<TToken> : ILexer<TToken>
        where TToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ITextScanner scanner;

        protected Lexer(ITextScanner scanner)
        {
            Contract.Requires(scanner != null);
            this.scanner = scanner;
        }

        /// <summary>Gets the text scanner that provides the individual characters of the source text.</summary>
        protected ITextScanner Scanner
        {
            get
            {
                Contract.Ensures(Contract.Result<ITextScanner>() != null);
                return this.scanner;
            }
        }

        /// <inheritdoc />
        public virtual void PutBack(TToken token)
        {
            var data = token.Data;
            if (data == null)
            {
                return;
            }

            for (var i = data.Length - 1; i >= 0; i--)
            {
                this.scanner.PutBack(data[i]);
            }
        }

        /// <inheritdoc />
        public abstract TToken Read();

        /// <inheritdoc />
        public abstract bool TryRead(out TToken token);

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.scanner != null);
        }
    }
}