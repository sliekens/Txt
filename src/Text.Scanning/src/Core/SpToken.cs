using System.Diagnostics.Contracts;

namespace Text.Core
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
