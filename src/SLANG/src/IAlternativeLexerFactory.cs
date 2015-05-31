namespace SLANG
{
    public interface IAlternativeLexerFactory
    {
        ILexer<Alternative> Create(params ILexer[] lexers);
    }
}
