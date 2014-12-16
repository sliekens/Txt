using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class SpToken : Token
    {
        public SpToken(ITextContext context)
            : base('\u0020', context)
        {
            Contract.Requires(context != null);
        }
    }
}
