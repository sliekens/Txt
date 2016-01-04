namespace TextFx
{
    using System.Collections.Generic;

    /// <summary>Creates instances of the <see cref="TerminalLexer" /> class.</summary>
    public class TerminalLexerFactory : ITerminalLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Terminal> Create(string terminal, IEqualityComparer<string> comparer)
        {
            return new TerminalLexer(terminal, comparer);
        }
    }
}