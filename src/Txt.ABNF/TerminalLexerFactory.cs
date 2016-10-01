using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Creates instances of the <see cref="TerminalLexer" /> class.</summary>
    public class TerminalLexerFactory : ITerminalLexerFactory
    {
        [NotNull]
        public static TerminalLexerFactory Default { get; } = new TerminalLexerFactory();

        /// <inheritdoc />
        public ILexer<Terminal> Create(string terminal, IEqualityComparer<string> comparer)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }
            return new TerminalLexer(terminal, comparer);
        }
    }
}
