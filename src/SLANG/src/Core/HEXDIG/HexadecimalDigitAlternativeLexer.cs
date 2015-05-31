namespace SLANG.Core
{
    public class HexadecimalDigitAlternativeLexer : AlternativeLexer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lexers">DIGIT / "A" / "B" / "C" / "D" / "E" / "F"</param>
        public HexadecimalDigitAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
