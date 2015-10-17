namespace TextFx
{
    using System;

    /// <summary>Creates instances of the <see cref="TerminalLexer" /> class.</summary>
    public class TerminalLexerFactory : ITerminalLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Terminal> Create(string terminal, StringComparer stringComparer)
        {
            return new TerminalLexer(terminal, stringComparer);
        }
    }
}