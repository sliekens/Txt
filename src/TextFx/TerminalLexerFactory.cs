namespace TextFx
{
    /// <summary>Creates instances of the <see cref="TerminalLexer" /> class.</summary>
    public class TerminalLexerFactory : ITerminalLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Terminal> Create(string terminal)
        {
            return new TerminalLexer(terminal);
        }
    }
}