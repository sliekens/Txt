using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>
    ///     Provides methods for reading a terminal value using the specified casing rules.
    /// </summary>
    public class TerminalLexer : Lexer<Terminal>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IEqualityComparer<string> comparer;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly string terminal;

        public TerminalLexer([NotNull] string terminal, [NotNull] IEqualityComparer<string> comparer)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }
            if (string.IsNullOrEmpty(terminal))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(terminal));
            }
            this.terminal = terminal;
            this.comparer = comparer;
        }

        protected override IReadResult<Terminal> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = scanner.TryMatch(terminal, comparer);
            if (result.Success)
            {
                return new ReadResult<Terminal>(new Terminal(result.Text, context));
            }
            return new ReadResult<Terminal>(SyntaxError.FromMatchResult(result, context));
        }
    }
}
