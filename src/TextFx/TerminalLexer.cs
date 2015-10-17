namespace TextFx
{
    using System;

    /// <summary>
    ///     Provides methods for reading a terminal value. This implementation is case-sensitive. For a case-insensitive
    ///     implementation, use the <see cref="CaseInsensitiveTerminalLexer" /> class.
    /// </summary>
    public class TerminalLexer : Lexer<Terminal>
    {
        private readonly string terminal;

        public TerminalLexer(string terminal)
        {
            this.terminal = terminal;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Terminal element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }

            var context = scanner.GetContext();
            string s;
            if (!scanner.EndOfInput && scanner.TryMatch(this.terminal, out s))
            {
                element = new Terminal(s, context);
                if (previousElementOrNull != null)
                {
                    element.PreviousElement = previousElementOrNull;
                    previousElementOrNull.NextElement = element;
                }

                return true;
            }

            element = default(Terminal);
            return false;
        }
    }
}