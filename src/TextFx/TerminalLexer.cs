namespace TextFx
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    ///     Provides methods for reading a terminal value using the specified casing rules.
    /// </summary>
    public class TerminalLexer : Lexer<Terminal>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly string terminal;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IEqualityComparer<string> comparer;

        public TerminalLexer(string terminal, IEqualityComparer<string> comparer)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            this.terminal = terminal;
            this.comparer = comparer;
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

            var result = scanner.TryMatch(this.terminal, this.comparer);
            if (!result.Success)
            {
                return ReadResult<Terminal>.FromError(new SyntaxError
                {
                    Message = $"Unexpected symbol: '{result.Text}'. Expected symbol: '{this.terminal}'",
                    Context = context
                });
            }

            var element = new Terminal(result.Text, context);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return ReadResult<Terminal>.FromResult(element);
        }
    }
}