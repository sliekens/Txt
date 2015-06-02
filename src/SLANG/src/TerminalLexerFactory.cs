namespace SLANG
{
    public class TerminalLexerFactory : ITerminalLexerFactory
    {
        public ILexer<Terminal> Create(char terminal)
        {
            return new TerminalLexer(terminal);
        }
    }
}