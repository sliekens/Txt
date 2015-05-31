namespace SLANG.Core.HTAB
{
    using System;

    public class HorizontalTabLexerFactory : ILexerFactory<HorizontalTab>
    {
        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        public HorizontalTabLexerFactory(ITerminalsLexerFactory terminalsLexerFactory)
        {
            if (terminalsLexerFactory == null)
            {
                throw new ArgumentNullException("terminalsLexerFactory", "Precondition: terminalsLexerFactory != null");
            }

            this.terminalsLexerFactory = terminalsLexerFactory;
        }

        public ILexer<HorizontalTab> Create()
        {
            var horizontalTabTerminalLexer = this.terminalsLexerFactory.Create('\x09');
            return new HorizontalTabLexer(horizontalTabTerminalLexer);
        }
    }
}
