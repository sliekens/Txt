namespace Text.Scanning.Core
{
    public class AlphaLexer : Lexer<Alpha>
    {
        /// <inheritdoc />
        public override Alpha Read(ITextScanner scanner)
        {
            Alpha element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'ALPHA'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Alpha element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Alpha);
                return false;
            }

            var context = scanner.GetContext();

            // A - Z
            for (var c = '\u0041'; c <= '\u005A'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Alpha(c, context);
                    return true;
                }
            }

            // a - z
            for (var c = '\u0061'; c <= '\u007A'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Alpha(c, context);
                    return true;
                }
            }

            element = default(Alpha);
            return false;
        }
    }
}