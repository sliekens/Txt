namespace Text.Core
{
    public class DQuoteToken : Token
    {
        public DQuoteToken(ITextContext context)
            : base("\"", context)
        {
        }
    }
}
