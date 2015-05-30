namespace SLANG.Core
{
    public class HexadecimalDigitAlternativeLexer : AlternativeLexer
    {
        public HexadecimalDigitAlternativeLexer(ILexer<Digit> digitLexer, ILexer aTerminalLexer, ILexer bTerminalLexer, ILexer cTerminalLexer, ILexer dTerminalLexer, ILexer eTerminalLexer, ILexer fTerminalLexer)
            : base(digitLexer, aTerminalLexer, bTerminalLexer, cTerminalLexer, dTerminalLexer, eTerminalLexer, fTerminalLexer)
        {
        }
    }
}
