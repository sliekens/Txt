using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class OctetLexer : Lexer<OctetToken>
    {
        public OctetLexer(ITextScanner scanner)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
        }

        public override OctetToken Read()
        {
            OctetToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected 'OCTET'", this.Scanner.GetContext());
        }

        public override bool TryRead(out OctetToken token)
        {
            var context = this.Scanner.GetContext();
            for (var c = '\u0000'; c <= '\u00FF'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new OctetToken(c, context);
                    return true;
                }
            }

            token = default(OctetToken);
            return false;
        }
    }
}
