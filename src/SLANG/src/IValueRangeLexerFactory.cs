namespace SLANG
{
    public interface IValueRangeLexerFactory
    {
        ILexer<Terminal> Create(char lowerBound, char upperBound);
    }
}
