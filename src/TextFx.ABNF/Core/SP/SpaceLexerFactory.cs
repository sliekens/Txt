namespace TextFx.ABNF.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="SpaceLexer" /> class.</summary>
    public class SpaceLexerFactory : ILexerFactory<Space>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SpaceLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Space> Create()
        {
            var spaceTerminalLexer = this.terminalLexerFactory.Create("\x20");
            return new SpaceLexer(spaceTerminalLexer);
        }
    }
}