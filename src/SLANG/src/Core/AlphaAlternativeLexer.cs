namespace SLANG.Core
{
    public class AlphaAlternativeLexer : AlternativeLexer
    {
        public AlphaAlternativeLexer(ILexer upperCaseValueRangeLexer, ILexer lowerCaseValueRangeLexer)
            : base(upperCaseValueRangeLexer, lowerCaseValueRangeLexer)
        {
        }
    }
}
