namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class CtlLexer : Lexer<CtlToken>
    {
        public CtlLexer(ITextScanner scanner)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
        }

        public override CtlToken Read()
        {
            var context = this.Scanner.GetContext();
            CtlToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'CTL'");
        }

        public override bool TryRead(out CtlToken token)
        {
            var context = this.Scanner.GetContext();

            // %x00-1F
            for (var c = '\u0000'; c <= '\u001F'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new CtlToken(c, context);
                    return true;
                }
            }

            // %x7F
            if (this.Scanner.TryMatch('\u007F'))
            {
                token = new CtlToken('\u007F', context);
                return true;
            }

            token = default(CtlToken);
            return false;
        }
    }
}