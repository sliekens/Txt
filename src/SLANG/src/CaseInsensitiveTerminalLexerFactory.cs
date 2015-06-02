namespace SLANG
{
    public class CaseInsensitiveTerminalLexerFactory : ITerminalLexerFactory
    {
        public ILexer<Terminal> Create(char terminal)
        {
            return new CaseInsensitiveTerminalLexer(terminal);
        }
    }
}