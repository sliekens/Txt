namespace SLANG.Core
{
    using System;

    public class SpaceLexerFactory : ILexerFactory<Space>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SpaceLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<Space> Create()
        {
            var spaceTerminalLexer = this.terminalLexerFactory.Create('\x20');
            return new SpaceLexer(spaceTerminalLexer);
        }
    }
}