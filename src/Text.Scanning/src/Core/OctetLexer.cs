namespace Text.Scanning.Core
{
    public class OctetLexer : Lexer<OctetElement>
    {
        /// <inheritdoc />
        public override OctetElement Read(ITextScanner scanner)
        {
            OctetElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'OCTET'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out OctetElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(OctetElement);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0000'; c <= '\u00FF'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new OctetElement(c, context);
                    return true;
                }
            }

            element = default(OctetElement);
            return false;
        }
    }
}