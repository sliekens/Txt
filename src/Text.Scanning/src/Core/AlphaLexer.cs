namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>A-Z / a-z</summary>
    public class AlphaLexer : Lexer<AlphaToken>
    {
        public AlphaLexer(ITextScanner scanner)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
        }

        public override AlphaToken Read()
        {
            var context = this.Scanner.GetContext();
            AlphaToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'ALPHA'");
        }

        public override bool TryRead(out AlphaToken token)
        {
            if (this.Scanner.EndOfInput)
            {
                token = default(AlphaToken);
                return false;
            }

            var context = this.Scanner.GetContext();
            return this.TryReadLowercase(out token, context) || this.TryReadUppercase(out token, context);
        }

        private bool TryReadLowercase(out AlphaToken token, ITextContext context)
        {
            for (var c = 'a'; c <= 'z'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new AlphaToken(c, context);
                    return true;
                }
            }

            token = default(AlphaToken);
            return false;
        }

        private bool TryReadUppercase(out AlphaToken token, ITextContext context)
        {
            for (var c = 'A'; c <= 'Z'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new AlphaToken(c, context);
                    return true;
                }
            }

            token = default(AlphaToken);
            return false;
        }
    }
}