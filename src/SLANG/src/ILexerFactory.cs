namespace SLANG
{
    public interface ILexerFactory<T>
        where T : Element
    {
        ILexer<T> Create();
    }
}