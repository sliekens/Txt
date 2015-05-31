namespace SLANG
{
    public class ValueRangeLexerFactory : IValueRangeLexerFactory
    {
        public ILexer<Element> Create(char lowerBound, char upperBound)
        {
            return new ValueRangeLexer(lowerBound, upperBound);
        }
    }
}