namespace SLANG.Core.LF
{
    using System;

    public class LineFeedLexerFactory : ILexerFactory<LineFeed>
    {
        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        public LineFeedLexerFactory(ITerminalsLexerFactory terminalsLexerFactory)
        {
            if (terminalsLexerFactory == null)
            {
                throw new ArgumentNullException("terminalsLexerFactory", "Precondition: terminalsLexerFactory != null");
            }

            this.terminalsLexerFactory = terminalsLexerFactory;
        }

        public ILexer<LineFeed> Create()
        {
            var lineFeedTerminalLexer = this.terminalsLexerFactory.Create('\x0A');
            return new LineFeedLexer(lineFeedTerminalLexer);
        }
    }
}
