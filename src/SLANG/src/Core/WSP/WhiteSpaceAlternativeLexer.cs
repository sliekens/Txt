namespace SLANG.Core
{
    public class WhiteSpaceAlternativeLexer : AlternativeLexer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lexers">SP / HTAB</param>
        public WhiteSpaceAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
