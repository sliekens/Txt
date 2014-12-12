using System;

namespace Text.Core
{
    public class LfLexer : Lexer<LfToken>
    {
        public LfLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override LfToken Read()
        {
            var context = this.Scanner.GetContext();
            LfToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected LF", context);
        }

        public override bool TryRead(out LfToken token)
        {
            token = default(LfToken);
            var context = this.Scanner.GetContext();
            if (this.Scanner.TryMatch('\n'))
            {
                token = new LfToken(context);
                return true;
            }

            return false;
        }
    }
}
