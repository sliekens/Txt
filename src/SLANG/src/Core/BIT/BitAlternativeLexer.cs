namespace SLANG.Core
{
    public class BitAlternativeLexer : AlternativeLexer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lexers">"0" / "1"</param>
        public BitAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
