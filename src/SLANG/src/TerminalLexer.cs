namespace SLANG
{
    using System;

    public class TerminalLexer : Lexer<Terminal>
    {
        private readonly char terminal;

        public TerminalLexer(char terminal)
        {
            this.terminal = terminal;
        }

        public override bool TryRead(ITextScanner scanner, out Terminal element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            var context = scanner.GetContext();
            if (!scanner.EndOfInput && scanner.TryMatch(this.terminal))
            {
                element = new Terminal(this.terminal, context);
                return true;
            }

            element = default(Terminal);
            return false;
        }
    }
}
