namespace SLANG
{
    public class RepetitionLexerFactory : IRepetitionLexerFactory
    {
        public ILexer<Repetition> Create(ILexer lexer, int lowerBound, int upperBound)
        {
            return new RepetitionLexer(lexer, lowerBound, upperBound);
        }
    }
}