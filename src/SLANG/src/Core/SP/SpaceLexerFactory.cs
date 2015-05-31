namespace SLANG.Core.SP
{
    using System;

    public class SpaceLexerFactory : ILexerFactory<Space>
    {
        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        public SpaceLexerFactory(ITerminalsLexerFactory terminalsLexerFactory)
        {
            if (terminalsLexerFactory == null)
            {
                throw new ArgumentNullException("terminalsLexerFactory", "Precondition: terminalsLexerFactory != null");
            }

            this.terminalsLexerFactory = terminalsLexerFactory;
        }

        public ILexer<Space> Create()
        {
            var spaceTerminalLexer = this.terminalsLexerFactory.Create('\x20');
            return new SpaceLexer(spaceTerminalLexer);
        }
    }
}
