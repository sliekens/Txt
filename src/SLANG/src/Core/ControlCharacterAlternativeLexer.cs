namespace SLANG.Core
{
    public class ControlCharacterAlternativeLexer : AlternativeLexer
    {
        public ControlCharacterAlternativeLexer(ILexer controlsValueRangeLexer, ILexer deleteTerminalLexer)
            : base(controlsValueRangeLexer, deleteTerminalLexer)
        {
        }
    }
}
