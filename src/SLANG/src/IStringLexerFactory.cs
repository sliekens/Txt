namespace SLANG
{
    public interface IStringLexerFactory
    {
        ILexer<TerminalString> Create(char[] terminals);

        ILexer<TerminalString> Create(string terminals);
    }
}