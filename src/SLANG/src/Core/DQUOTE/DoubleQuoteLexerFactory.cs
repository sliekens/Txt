namespace SLANG.Core.DQUOTE
{
    using System;

    public class DoubleQuoteLexerFactory : ILexerFactory<DoubleQuote>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public DoubleQuoteLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<DoubleQuote> Create()
        {
            var doubleQuoteTerminalLexer = this.terminalLexerFactory.Create('\x22');
            return new DoubleQuoteLexer(doubleQuoteTerminalLexer);
        }
    }
}