namespace Text.Scanning.Core
{
    public class SpLexer : Lexer<SpElement>
    {
        /// <inheritdoc />
        public override SpElement Read(ITextScanner scanner)
        {
            SpElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'SP'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out SpElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(SpElement);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u0020'))
            {
                element = new SpElement(context);
                return true;
            }

            element = default(SpElement);
            return false;
        }
    }
}