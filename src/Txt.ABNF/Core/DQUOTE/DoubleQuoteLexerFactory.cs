using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    /// <summary>Creates instances of the <see cref="DoubleQuoteLexer" /> class.</summary>
    public class DoubleQuoteLexerFactory : ILexerFactory<DoubleQuote>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DoubleQuoteLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<DoubleQuote> Create()
        {
            var innerLexer = terminalLexerFactory.Create("\x22", StringComparer.Ordinal);
            return new DoubleQuoteLexer(innerLexer);
        }
    }
}
