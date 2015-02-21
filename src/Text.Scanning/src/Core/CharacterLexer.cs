namespace Text.Scanning.Core
{
    public class CharacterLexer : Lexer<Character>
    {
        /// <inheritdoc />
        public override Character Read(ITextScanner scanner)
        {
            Character element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'CHAR'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Character element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Character);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0001'; c <= '\u007F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Character(c, context);
                    return true;
                }
            }

            element = default(Character);
            return false;
        }
    }
}