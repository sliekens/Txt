namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;

    /// <summary>Creates instances of the <see cref="HorizontalTabLexer" /> class.</summary>
    public class HorizontalTabLexerFactory : ILexerFactory<HorizontalTab>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HorizontalTabLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<HorizontalTab> Create()
        {
            var horizontalTabTerminalLexer = terminalLexerFactory.Create("\x09", StringComparer.Ordinal);
            return new HorizontalTabLexer(horizontalTabTerminalLexer);
        }
    }
}
