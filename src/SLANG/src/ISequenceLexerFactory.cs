namespace SLANG
{
    public interface ISequenceLexerFactory
    {
        ILexer<Sequence> Create(params ILexer[] lexers);
    }
}