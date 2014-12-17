namespace Text.Scanning
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

        public void PutBack(TToken token)
        {
            var data = token.Data;
            if (data == null)
            {
                return;
            }

            for (int i = data.Length - 1; i >= 0; i--)
            {
                this.scanner.PutBack(data[i]);
            }
        }
    }
}
