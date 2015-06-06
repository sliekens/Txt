namespace SLANG.Core.LF
{
    using System;

    public class LineFeedLexerFactory : ILexerFactory<LineFeed>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public LineFeedLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<LineFeed> Create()
        {
            var lineFeedTerminalLexer = this.terminalLexerFactory.Create('\x0A');
            return new LineFeedLexer(lineFeedTerminalLexer);
        }
    }
}