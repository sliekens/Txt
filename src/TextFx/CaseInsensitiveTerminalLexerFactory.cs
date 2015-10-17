namespace TextFx
{
    /// <summary>Creates instances of the <see cref="CaseInsensitiveTerminalLexer" /> class.</summary>
    public class CaseInsensitiveTerminalLexerFactory : ITerminalLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Terminal> Create(string terminal)
        {
            return new CaseInsensitiveTerminalLexer(terminal);
        }
    }
}