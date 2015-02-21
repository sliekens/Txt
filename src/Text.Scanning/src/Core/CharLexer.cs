namespace Text.Scanning.Core
{
    public class CharLexer : Lexer<CharElement>
    {
        /// <inheritdoc />
        public override CharElement Read(ITextScanner scanner)
        {
            CharElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'CHAR'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CharElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(CharElement);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0001'; c <= '\u007F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new CharElement(c, context);
                    return true;
                }
            }

            element = default(CharElement);
            return false;
        }
    }
}