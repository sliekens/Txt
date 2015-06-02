namespace SLANG
{
    public interface IOptionLexerFactory
    {
        ILexer<Repetition> Create(ILexer lexer);
    }
}
