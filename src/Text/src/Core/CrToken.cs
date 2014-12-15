namespace Text.Core
{
    public class CrToken : Token
    {
        public CrToken(ITextContext context)
            : base('\r', context)
        {
        }
    }
}
