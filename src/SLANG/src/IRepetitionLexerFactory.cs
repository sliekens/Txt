namespace SLANG
{
    public interface IRepetitionLexerFactory
    {
        ILexer<Repetition> Create(ILexer lexer, int lowerBound, int upperBound);
    }
}
