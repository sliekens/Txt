namespace SLANG
{
    public interface ITerminalsLexerFactory
    {
        ILexer<Element> Create(char terminal);

        ILexer<Element> Create(char[] terminals);

        ILexer<Element> Create(string terminals);
    }
}
