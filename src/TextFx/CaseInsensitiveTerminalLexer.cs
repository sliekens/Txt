namespace TextFx
{
    using System;

    /// <summary>
    ///     Provides methods for reading a terminal value. This implementation is case-insensitive. For a case-sensitive
    ///     implementation, use the <see cref="TerminalLexer" /> class.
    /// </summary>
    public class CaseInsensitiveTerminalLexer : Lexer<Terminal>
    {
        private readonly string terminal;

        public CaseInsensitiveTerminalLexer(string terminal)
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

            string c;
            var context = scanner.GetContext();
            if (!scanner.EndOfInput && scanner.TryMatch(this.terminal, StringComparer.OrdinalIgnoreCase, out c))
            {
                element = new Terminal(c, context);
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