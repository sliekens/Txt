namespace SLANG
{
    using System;

    public class CaseInsensitiveTerminalLexer : Lexer<Element>
    {
        private readonly char terminal;

        public CaseInsensitiveTerminalLexer(char terminal)
        {
            this.terminal = terminal;
        }

        public override bool TryRead(ITextScanner scanner, out Element element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            char c;
            var context = scanner.GetContext();
            if (!scanner.EndOfInput && scanner.TryMatchIgnoreCase(this.terminal, out c))
            {
                element = new Element(c, context);
                return true;
            }

            element = default(Element);
            return false;
        }
    }
}
