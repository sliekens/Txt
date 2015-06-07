namespace TextFx.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="DoubleQuoteLexer" /> class.</summary>
    public class DoubleQuoteLexerFactory : ILexerFactory<DoubleQuote>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public DoubleQuoteLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<DoubleQuote> Create()
        {
            var doubleQuoteTerminalLexer = this.terminalLexerFactory.Create('\x22');
            return new DoubleQuoteLexer(doubleQuoteTerminalLexer);
        }
    }
}