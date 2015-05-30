namespace SLANG.Core
{
    public class BreakingWhiteSpaceLexer : AlternativeLexer
    {
        public BreakingWhiteSpaceLexer(ILexer<EndOfLine> whiteSpaceLexer, ILexer<Sequence> endOfLineWhiteSpaceSequenceLexer)
            : base(whiteSpaceLexer, endOfLineWhiteSpaceSequenceLexer)
        {
        }
    }
}
