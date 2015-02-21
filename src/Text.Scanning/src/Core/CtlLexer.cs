namespace Text.Scanning.Core
{
    public class CtlLexer : Lexer<CtlElement>
    {
        /// <inheritdoc />
        public override CtlElement Read(ITextScanner scanner)
        {
            CtlElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'CTL'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CtlElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(CtlElement);
                return false;
            }

            var context = scanner.GetContext();

            // %x00-1F
            for (var c = '\u0000'; c <= '\u001F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new CtlElement(c, context);
                    return true;
                }
            }

            // %x7F
            if (scanner.TryMatch('\u007F'))
            {
                element = new CtlElement('\u007F', context);
                return true;
            }

            element = default(CtlElement);
            return false;
        }
    }
}