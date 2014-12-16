using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class CrToken : Token
    {
        public CrToken(ITextContext context)
            : base('\r', context)
        {
            Contract.Requires(context != null);
        }
    }
}
