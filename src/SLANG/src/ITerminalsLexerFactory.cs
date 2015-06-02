namespace SLANG
{
    public interface ITerminalsLexerFactory
    {
        ILexer<Terminal> Create(char terminal);

        ILexer<Element> Create(char[] terminals);

        ILexer<Element> Create(string terminals);
    }
}
