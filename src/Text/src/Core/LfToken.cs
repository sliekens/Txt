namespace Text.Core
{
    public class LfToken : Token
    {
        public LfToken(ITextContext context)
            : base("\n", context)
        {
        }
    }
}
