namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class HexDigLexer : Lexer<HexDigToken>
    {
        private readonly ILexer<DigitToken> digitLexer;

        public HexDigLexer(ITextScanner scanner)
            : this(scanner, new DigitLexer(scanner))
        {
            Contract.Requires(scanner != null);
        }

        public HexDigLexer(ITextScanner scanner, ILexer<DigitToken> digitLexer)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
            this.digitLexer = digitLexer;
        }

        public override HexDigToken Read()
        {
            var context = this.Scanner.GetContext();
            HexDigToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'HEXDIG'");
        }

        public override bool TryRead(out HexDigToken token)
        {
            if (this.Scanner.EndOfInput)
            {
                token = default(HexDigToken);
                return false;
            }

            var context = this.Scanner.GetContext();
            DigitToken digitToken;
            if (this.digitLexer.TryRead(out digitToken))
            {
                token = new HexDigToken(digitToken, context);
                return true;
            }

            // A-F
            for (var c = 'A'; c <= 'F'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new HexDigToken(c, context);
                    return true;
                }
            }

            // a-f
            for (var c = 'a'; c <= 'f'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new HexDigToken(c, context);
                    return true;
                }
            }

            token = default(HexDigToken);
            return false;
        }
    }
}