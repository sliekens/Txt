namespace Text.Scanning.Core
{
    public class LfLexer : Lexer<LfElement>
    {
        /// <inheritdoc />
        public override LfElement Read(ITextScanner scanner)
        {
            LfElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'LF'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LfElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(LfElement);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u000A'))
            {
                element = new LfElement(context);
                return true;
            }

            element = default(LfElement);
            return false;
        }
    }
}