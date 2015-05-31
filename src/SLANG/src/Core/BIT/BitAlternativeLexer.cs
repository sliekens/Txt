namespace SLANG.Core
{
    public class BitAlternativeLexer : AlternativeLexer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zeroStringLexer">"0"</param>
        /// <param name="oneStringLexer">"1"</param>
        public BitAlternativeLexer(ILexer zeroStringLexer, ILexer oneStringLexer)
            : base(zeroStringLexer, oneStringLexer)
        {
        }
    }
}
