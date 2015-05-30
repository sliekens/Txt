namespace SLANG.Core
{
    public class LinearWhiteSpaceRepetitionLexer : RepetitionLexer
    {
        public LinearWhiteSpaceRepetitionLexer(ILexer<Alternative> breakingWhiteSpaceLexer)
            : base(breakingWhiteSpaceLexer, 0, int.MaxValue)
        {
        }
    }
}
