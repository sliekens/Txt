namespace TextFx.ABNF
{
    using System;

    /// <summary>
    ///     Provides methods for reading a terminal value. This implementation is case-sensitive. For a case-insensitive
    ///     implementation, use the <see cref="CaseInsensitiveTerminalLexer" /> class.
    /// </summary>
    public class TerminalLexer : Lexer<Terminal>
    {
        private readonly char terminal;

        public TerminalLexer(char terminal)
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