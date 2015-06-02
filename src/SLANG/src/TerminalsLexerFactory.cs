namespace SLANG
{
    public class TerminalsLexerFactory : ITerminalsLexerFactory
    {
        public ILexer<Element> Create(char terminal)
        {
            return new TerminalLexer(terminal);
        }

        public ILexer<Element> Create(char[] terminals)
        {
            return new CaseSensitiveStringLexer(terminals);
        }

        public ILexer<Element> Create(string terminals)
        {
            return new CaseInsensitiveStringLexer(terminals.ToCharArray());
        }
    }
}