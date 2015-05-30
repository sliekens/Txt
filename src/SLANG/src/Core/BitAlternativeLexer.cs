namespace SLANG.Core
{
    public class BitAlternativeLexer : AlternativeLexer
    {
        public BitAlternativeLexer(ILexer zeroTerminalLexer, ILexer oneTerminalLexer)
            : base(zeroTerminalLexer, oneTerminalLexer)
        {
        }
    }
}
