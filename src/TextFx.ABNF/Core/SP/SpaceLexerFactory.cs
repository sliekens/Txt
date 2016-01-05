namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="SpaceLexer" /> class.</summary>
    public class SpaceLexerFactory : ILexerFactory<Space>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
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
            var spaceTerminalLexer = this.terminalLexerFactory.Create("\x20", StringComparer.Ordinal);
            return new SpaceLexer(spaceTerminalLexer);
        }
    }
}