namespace SLANG.Core.DQUOTE
{
    using System;

    public class DoubleQuoteLexerFactory : ILexerFactory<DoubleQuote>
    {
        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        public DoubleQuoteLexerFactory(ITerminalsLexerFactory terminalsLexerFactory)
        {
            if (terminalsLexerFactory == null)
            {
                throw new ArgumentNullException("terminalsLexerFactory", "Precondition: terminalsLexerFactory != null");
            }

            this.terminalsLexerFactory = terminalsLexerFactory;
        }

        public ILexer<DoubleQuote> Create()
        {
            var doubleQuoteTerminalLexer = this.terminalsLexerFactory.Create('\x22');
            return new DoubleQuoteLexer(doubleQuoteTerminalLexer);
        }
    }
}