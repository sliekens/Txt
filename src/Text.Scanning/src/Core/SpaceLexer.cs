namespace Text.Scanning.Core
{
    public class SpaceLexer : Lexer<Space>
    {
        /// <inheritdoc />
        public override Space Read(ITextScanner scanner)
        {
            Space element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'SP'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Space element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Space);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u0020'))
            {
                element = new Space(context);
                return true;
            }

            element = default(Space);
            return false;
        }
    }
}