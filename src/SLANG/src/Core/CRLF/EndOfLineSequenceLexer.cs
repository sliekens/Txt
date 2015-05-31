namespace SLANG.Core
{
    public class EndOfLineSequenceLexer : SequenceLexer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="carriageReturnLexer">CR</param>
        /// <param name="lineFeedLexer">LF</param>
        public EndOfLineSequenceLexer(ILexer<CarriageReturn> carriageReturnLexer, ILexer<LineFeed> lineFeedLexer)
            : base(carriageReturnLexer, lineFeedLexer)
        {
        }
    }
}