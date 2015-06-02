namespace SLANG
{
    public class ValueRangeLexerFactory : IValueRangeLexerFactory
    {
        public ILexer<Terminal> Create(char lowerBound, char upperBound)
        {
            return new ValueRangeLexer(lowerBound, upperBound);
        }
    }
}