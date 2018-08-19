using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>
    ///     Provides methods for reading a terminal value using the specified casing rules.
    /// </summary>
    public class TerminalLexer : Lexer<Terminal>
    {
        private readonly IEqualityComparer<string> comparer;

        private readonly string terminal;

        public TerminalLexer([NotNull] string terminal, [NotNull] IEqualityComparer<string> comparer)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            if (string.IsNullOrEmpty(terminal))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(terminal));
            }
            this.terminal = terminal;
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        protected override IEnumerable<Terminal> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            if (scanner.Peek() == -1)
            {
                return Empty;
            }
            var result = scanner.TryMatch(terminal, comparer);
            if (result.IsMatch)
            {
                return new[] { new Terminal(result.Text, context) };
            }
            return Empty;
        }
    }
}
