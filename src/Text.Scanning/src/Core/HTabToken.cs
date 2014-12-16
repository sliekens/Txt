using System.Diagnostics.Contracts;

namespace Text.Core
{
    public class HTabToken : Token
    {
        public HTabToken(ITextContext context)
            : base("\t", context)
        {
            Contract.Requires(context != null);
        }
    }
}
