namespace SLANG
{
    public interface IValueRangeLexerFactory
    {
        ILexer Create(char lowerBound, char upperBound);
    }
}
