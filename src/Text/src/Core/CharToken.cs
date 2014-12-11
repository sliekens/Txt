namespace Text.Core
{
    /// <summary>\u0001 - \u007F</summary>
    public class CharToken : Token
    {
        public CharToken(string data, ITextContext context)
            : base(data, context)
        {
        }
    }
}
