namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="DoubleQuoteLexer" /> class.</summary>
    public class DoubleQuoteLexerFactory : ILexerFactory<DoubleQuote>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public DoubleQuoteLexerFactory(ITerminalLexerFactory terminalLexerFactory)
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
            var doubleQuoteTerminalLexer = this.terminalLexerFactory.Create("\x22", StringComparer.Ordinal);
            return new DoubleQuoteLexer(doubleQuoteTerminalLexer);
        }
    }
}