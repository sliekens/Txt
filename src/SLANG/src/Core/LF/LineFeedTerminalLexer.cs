namespace SLANG.Core
{
    public class LineFeedTerminalLexer : TerminalsLexer
    {
        public LineFeedTerminalLexer()
            : base('\x0A')
        {
        }
    }
}