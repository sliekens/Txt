namespace Text.Scanning.Core
{
    public class HorizontalTabLexer : Lexer<HorizontalTab>
    {
        /// <inheritdoc />
        public override HorizontalTab Read(ITextScanner scanner)
        {
            HorizontalTab element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'HTAB'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HorizontalTab element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HorizontalTab);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\t'))
            {
                element = new HorizontalTab(context);
                return true;
            }

            element = default(HorizontalTab);
            return false;
        }
    }
}