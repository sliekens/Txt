namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class DQuoteToken : Token
    {
        public DQuoteToken(ITextContext context)
            : base('\"', context)
        {
            Contract.Requires(context != null);
        }
    }
}