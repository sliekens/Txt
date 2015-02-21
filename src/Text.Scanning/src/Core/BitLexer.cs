namespace Text.Scanning.Core
{
    public class BitLexer : Lexer<BitElement>
    {
        /// <inheritdoc />
        public override BitElement Read(ITextScanner scanner)
        {
            BitElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'BIT'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out BitElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(BitElement);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u0030'))
            {
                element = new BitElement('\u0030', context);
                return true;
            }

            if (scanner.TryMatch('\u0031'))
            {
                element = new BitElement('\u0031', context);
                return true;
            }

            element = default(BitElement);
            return false;
        }
    }
}