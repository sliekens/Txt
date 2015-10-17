namespace TextFx
{
    using System;

    /// <summary>
    ///     Provides methods for reading a terminal value using the specified casing rules.
    /// </summary>
    public class TerminalLexer : Lexer<Terminal>
    {
        private readonly string terminal;

        private readonly StringComparer stringComparer;

        public TerminalLexer(string terminal, StringComparer stringComparer)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            if (stringComparer == null)
            {
                throw new ArgumentNullException(nameof(stringComparer));
            }

            this.terminal = terminal;
            this.stringComparer = stringComparer;
        }

        public override ReadResult<Terminal> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }

            var context = scanner.GetContext();
            if (scanner.EndOfInput)
            {
                return ReadResult<Terminal>.FromError(new SyntaxError
                {
                    Message = $"Unexpected end of input. Expected symbol: '{this.terminal}'",
                    Context = context
                });
            }

            string next;
            if (!scanner.TryMatch(this.terminal, this.stringComparer, out next))
            {
                return ReadResult<Terminal>.FromError(new SyntaxError
                {
                    Message = $"Unexpected symbol: '{next}'. Expected symbol: '{this.terminal}'",
                    Context = context
                });
            }

            var element = new Terminal(next, context);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return ReadResult<Terminal>.FromResult(element);
        }
    }
}