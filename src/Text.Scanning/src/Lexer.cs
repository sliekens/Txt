namespace Text
{
    public abstract class Lexer<TToken> : ILexer<TToken>
        where TToken : Token
    {
        private readonly ITextScanner scanner;

        protected Lexer(ITextScanner scanner)
        {
            this.scanner = scanner;
        }

        protected ITextScanner Scanner
        {
            get { return this.scanner; }
        }

        public abstract TToken Read();
        
        public abstract bool TryRead(out TToken token);
    }
}
