﻿namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class BitLexer : Lexer<BitToken>
    {
        public BitLexer(ITextScanner scanner)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
        }

        public override BitToken Read()
        {
            var context = this.Scanner.GetContext();
            BitToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'BIT'");
        }

        public override bool TryRead(out BitToken token)
        {
            var context = this.Scanner.GetContext();
            if (this.Scanner.TryMatch('0'))
            {
                token = new BitToken('0', context);
                return true;
            }

            if (this.Scanner.TryMatch('1'))
            {
                token = new BitToken('1', context);
                return true;
            }

            token = default(BitToken);
            return false;
        }
    }
}