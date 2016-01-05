namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="LineFeedLexer" /> class.</summary>
    public class LineFeedLexerFactory : ILexerFactory<LineFeed>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public LineFeedLexerFactory(ITerminalLexerFactory terminalLexerFactory)
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
            var lineFeedTerminalLexer = this.terminalLexerFactory.Create("\x0A", StringComparer.Ordinal);
            return new LineFeedLexer(lineFeedTerminalLexer);
        }
    }
}