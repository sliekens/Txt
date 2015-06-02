namespace SLANG
{
    public class TerminalsLexerFactory : ITerminalsLexerFactory
    {
        public ILexer<Terminal> Create(char terminal)
        {
            return new TerminalLexer(terminal);
        }

        public ILexer<Element> Create(char[] terminals)
        {
            return new StringLexer(terminals);
        }

        public ILexer<Element> Create(string terminals)
        {
            return new CaseInsensitiveStringLexer(terminals.ToCharArray());
        }
    }
}