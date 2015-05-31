namespace SLANG.Core
{
    public class EndOfLineWhiteSpaceSequenceLexer : SequenceLexer
    {
        public EndOfLineWhiteSpaceSequenceLexer(ILexer<EndOfLine> endOfLineLexer, ILexer<WhiteSpace> whiteSpaceLexer)
            : base(endOfLineLexer, whiteSpaceLexer)
        {
        }
    }
}