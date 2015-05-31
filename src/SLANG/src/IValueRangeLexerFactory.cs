namespace SLANG
{
    public interface IValueRangeLexerFactory
    {
        ILexer<Element> Create(char lowerBound, char upperBound);
    }
}
