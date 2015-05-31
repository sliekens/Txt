namespace SLANG
{
    public class TerminalsLexerFactory : ITerminalsLexerFactory
    {
        public ILexer<Element> Create(char terminal)
        {
            return new TerminalsLexer(terminal);
        }

        public ILexer<Element> Create(char[] terminals)
        {
            return new TerminalsLexer(terminals);
        }

        public ILexer<Element> Create(string terminals)
        {
            return new StringLexer(terminals);
        }
    }
}