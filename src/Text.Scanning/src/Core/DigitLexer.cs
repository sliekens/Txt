namespace Text.Scanning.Core
{
    public class DigitLexer : Lexer<DigitElement>
    {
        /// <inheritdoc />
        public override DigitElement Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            DigitElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'DIGIT'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out DigitElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(DigitElement);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '0'; c <= '9'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new DigitElement(c, context);
                    return true;
                }
            }

            element = default(DigitElement);
            return false;
        }
    }
}