namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>A-Z / a-z</summary>
    public class AlphaToken : Token
    {
        public AlphaToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires((data >= 'A' && data <= 'Z') || (data >= 'a' && data <= 'z'));
            Contract.Requires(context != null);
        }
    }
}