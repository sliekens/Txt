namespace SLANG
{
    using System;

    public class TerminalLexer : Lexer<Element>
    {
        private readonly char terminal;

        public TerminalLexer(char terminal)
        {
            this.terminal = terminal;
        }

        public override bool TryRead(ITextScanner scanner, out Element element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch(this.terminal))
            {
                element = new Element(this.terminal, context);
                return true;
            }

            element = default(Element);
            return false;
        }
    }
}
