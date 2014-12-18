namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class CharLexer : Lexer<CharToken>
    {
        public CharLexer(ITextScanner scanner)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
        }

        public override CharToken Read()
        {
            var context = this.Scanner.GetContext();
            CharToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'CHAR'");
        }

        public override bool TryRead(out CharToken token)
        {
            if (this.Scanner.EndOfInput)
            {
                token = default(CharToken);
                return false;
            }

            var context = this.Scanner.GetContext();
            for (var c = '\u0001'; c <= '\u007F'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new CharToken(c, context);
                    return true;
                }
            }

            token = default(CharToken);
            return false;
        }
    }
}