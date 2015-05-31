namespace SLANG.Core
{
    public class ControlCharacterAlternativeLexer : AlternativeLexer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lexers">%x00-1F / %x7F</param>
        public ControlCharacterAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
