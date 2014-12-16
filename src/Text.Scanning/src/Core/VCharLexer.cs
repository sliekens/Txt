﻿using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class VCharLexer : Lexer<VCharToken>
    {
        public VCharLexer(ITextScanner scanner)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
        }

        public override VCharToken Read()
        {
            VCharToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected 'VCHAR'", this.Scanner.GetContext());
        }

        public override bool TryRead(out VCharToken token)
        {
            var context = this.Scanner.GetContext();
            for (var c = '\u0021'; c < '\u007E'; c++)
            {
                if (this.Scanner.TryMatch(c))
                {
                    token = new VCharToken(c, context);
                    return true;
                }
            }

            token = default(VCharToken);
            return false;
        }
    }
}
