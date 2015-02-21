namespace Text.Scanning.Core
{
    public class ControlCharacterLexer : Lexer<ControlCharacter>
    {
        /// <inheritdoc />
        public override ControlCharacter Read(ITextScanner scanner)
        {
            ControlCharacter element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'CTL'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out ControlCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default(ControlCharacter);
                return false;
            }

            var context = scanner.GetContext();

            // %x00-1F
            for (var c = '\u0000'; c <= '\u001F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new ControlCharacter(c, context);
                    return true;
                }
            }

            // %x7F
            if (scanner.TryMatch('\u007F'))
            {
                element = new ControlCharacter('\u007F', context);
                return true;
            }

            element = default(ControlCharacter);
            return false;
        }
    }
}