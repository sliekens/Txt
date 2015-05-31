namespace SLANG.Core
{
    public class WhiteSpaceAlternativeLexer : AlternativeLexer
    {
        public WhiteSpaceAlternativeLexer(ILexer<Space> spaceLexer, ILexer<HorizontalTab> horizontalTabLexer)
            : base(spaceLexer, horizontalTabLexer)
        {
            
        }
    }
}
