namespace TextFx.ABNF.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="LineFeedLexer" /> class.</summary>
    public class LineFeedLexerFactory : ILexerFactory<LineFeed>
    {
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
            var lineFeedTerminalLexer = this.terminalLexerFactory.Create("\x0A");
            return new LineFeedLexer(lineFeedTerminalLexer);
        }
    }
}