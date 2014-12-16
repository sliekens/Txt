using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class DQuoteToken : Token
    {
        public DQuoteToken(ITextContext context)
            : base('\"', context)
        {
            Contract.Requires(context != null);
        }
    }
}
