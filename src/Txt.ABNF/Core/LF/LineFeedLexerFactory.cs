using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    /// <summary>Creates instances of the <see cref="LineFeedLexer" /> class.</summary>
    public class LineFeedLexerFactory : ILexerFactory<LineFeed>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public LineFeedLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<LineFeed> Create()
        {
            var innerLexer = terminalLexerFactory.Create("\x0A", StringComparer.Ordinal);
            return new LineFeedLexer(innerLexer);
        }
    }
}
