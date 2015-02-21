namespace Text.Scanning.Core
{
    public class VCharLexer : Lexer<VCharElement>
    {
        /// <inheritdoc />
        public override VCharElement Read(ITextScanner scanner)
        {
            VCharElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'VCHAR'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out VCharElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(VCharElement);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0021'; c < '\u007E'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new VCharElement(c, context);
                    return true;
                }
            }

            element = default(VCharElement);
            return false;
        }
    }
}