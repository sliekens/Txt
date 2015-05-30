namespace SLANG
{
    /// <summary>Provides the base class for lexers whose lexer rule is an optional element.</summary>
    public class OptionLexer : RepetitionLexer
    {
        public OptionLexer(ILexer optionalElementLexer)
            : base(optionalElementLexer, 0, 1)
        {
        }
    }
}
