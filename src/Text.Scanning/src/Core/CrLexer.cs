namespace Text.Scanning.Core
{
    public class CrLexer : Lexer<CrElement>
    {
        /// <inheritdoc />
        public override CrElement Read(ITextScanner scanner)
        {
            CrElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'CR'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CrElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(CrElement);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u000D'))
            {
                element = new CrElement(context);
                return true;
            }

            element = default(CrElement);
            return false;
        }
    }
}