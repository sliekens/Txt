namespace SLANG.Core
{
    public class EndOfLineSequenceLexer : SequenceLexer
    {
        public EndOfLineSequenceLexer(ILexer<CarriageReturn> carriageReturnLexer, ILexer<LineFeed> lineFeedLexer)
            : base(carriageReturnLexer, lineFeedLexer)
        {
        }
    }
}