﻿namespace Text.Scanning.Core
{
    public class CrLfLexer : Lexer<CrLfToken>
    {
        private readonly ILexer<CrToken> crLexer;
        private readonly ILexer<LfToken> lfLexer;

        public CrLfLexer(ITextScanner scanner)
            : this(scanner, new CrLexer(scanner), new LfLexer(scanner))
        {
        }

        public CrLfLexer(ITextScanner scanner, ILexer<CrToken> crLexer, ILexer<LfToken> lfLexer)
            : base(scanner)
        {
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
                throw new SyntaxErrorException("Expected 'CR LF'", syntaxErrorException, context);
            }
        }

        public override bool TryRead(out CrLfToken token)
        {
            var context = this.Scanner.GetContext();
            CrToken crToken;
            if (!this.crLexer.TryRead(out crToken))
            {
                token = default(CrLfToken);
                return false;
            }

            LfToken lfToken;
            if (!this.lfLexer.TryRead(out lfToken))
            {
                token = default(CrLfToken);
                return false;
            }

            token = new CrLfToken(crToken, lfToken, context);
            return true;
        }
    }
}
