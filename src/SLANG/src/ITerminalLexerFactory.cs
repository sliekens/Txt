namespace SLANG
{
    public interface ITerminalLexerFactory
    {
        ILexer<Terminal> Create(char terminal);
    }
}
