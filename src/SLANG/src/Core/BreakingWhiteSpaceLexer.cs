namespace SLANG.Core
{
    public class BreakingWhiteSpaceLexer : AlternativeLexer
    {
        public BreakingWhiteSpaceLexer(ILexer<WhiteSpace> whiteSpaceLexer, ILexer<Sequence> endOfLineWhiteSpaceSequenceLexer)
            : base(whiteSpaceLexer, endOfLineWhiteSpaceSequenceLexer)
        {
        }
    }
}
