namespace Text.Scanning.Core
{
    public class HTabLexer : Lexer<HTabElement>
    {
        /// <inheritdoc />
        public override HTabElement Read(ITextScanner scanner)
        {
            HTabElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'HTAB'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HTabElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HTabElement);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\t'))
            {
                element = new HTabElement(context);
                return true;
            }

            element = default(HTabElement);
            return false;
        }
    }
}