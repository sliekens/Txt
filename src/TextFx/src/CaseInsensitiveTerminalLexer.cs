namespace TextFx
{
    using System;

    /// <summary>
    ///     Provides methods for reading a terminal value. This implementation is case-insensitive. For a case-sensitive
    ///     implementation, use the <see cref="TerminalLexer" /> class.
    /// </summary>
    public class CaseInsensitiveTerminalLexer : Lexer<Terminal>
    {
        private readonly char terminal;

        public CaseInsensitiveTerminalLexer(char terminal)
        {
            this.terminal = terminal;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Terminal element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
            }

            char c;
            var context = scanner.GetContext();
            if (!scanner.EndOfInput && scanner.TryMatchIgnoreCase(this.terminal, out c))
            {
                element = new Terminal(c, context);
                return true;
            }

            element = default(Terminal);
            return false;
        }
    }
}