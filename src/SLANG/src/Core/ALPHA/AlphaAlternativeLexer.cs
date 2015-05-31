namespace SLANG.Core
{
    public class AlphaAlternativeLexer : AlternativeLexer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lexers">%x41-5A / %x61-7A</param>
        public AlphaAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
