namespace SLANG.Core
{
    public class CarriageReturnTerminalLexer : TerminalsLexer
    {
        public CarriageReturnTerminalLexer()
            : base('\x0D')
        {
        }
    }
}