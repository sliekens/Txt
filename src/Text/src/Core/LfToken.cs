using System.Diagnostics.Contracts;

namespace Text.Core
{
    public class LfToken : Token
    {
        public LfToken(ITextContext context)
            : base("\n", context)
        {
            Contract.Requires(context != null);
        }
    }
}
