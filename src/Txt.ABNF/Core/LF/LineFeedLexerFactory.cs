using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt;

namespace Text.ABNF.Core.LF
{
    /// <summary>Creates instances of the <see cref="LineFeedLexer" /> class.</summary>
    public class LineFeedLexerFactory : ILexerFactory<LineFeed>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// 
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
            var lineFeedTerminalLexer = terminalLexerFactory.Create("\x0A", StringComparer.Ordinal);
            return new LineFeedLexer(lineFeedTerminalLexer);
        }
    }
}
