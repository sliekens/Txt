namespace SLANG.Core
{
    public class AlphaAlternativeLexer : AlternativeLexer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="upperCaseValueRangeLexer">%x41-5A</param>
        /// <param name="lowerCaseValueRangeLexer">%x61-7A</param>
        public AlphaAlternativeLexer(ILexer upperCaseValueRangeLexer, ILexer lowerCaseValueRangeLexer)
            : base(upperCaseValueRangeLexer, lowerCaseValueRangeLexer)
        {
        }
    }
}
