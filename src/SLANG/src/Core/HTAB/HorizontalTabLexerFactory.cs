namespace SLANG.Core.HTAB
{
    using System;

    public class HorizontalTabLexerFactory : ILexerFactory<HorizontalTab>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HorizontalTabLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<HorizontalTab> Create()
        {
            var horizontalTabTerminalLexer = this.terminalLexerFactory.Create('\x09');
            return new HorizontalTabLexer(horizontalTabTerminalLexer);
        }
    }
}
