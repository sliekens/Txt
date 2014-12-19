namespace Text.Scanning.Core
{
    public class BitLexer : Lexer<BitToken>
    {
        public override BitToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            BitToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'BIT'");
        }

        public override bool TryRead(ITextScanner scanner, out BitToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(BitToken);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('0'))
            {
                token = new BitToken('0', context);
                return true;
            }

            if (scanner.TryMatch('1'))
            {
                token = new BitToken('1', context);
                return true;
            }

            token = default(BitToken);
            return false;
        }
    }
}