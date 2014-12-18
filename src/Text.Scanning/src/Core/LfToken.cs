namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class LfToken : Token
    {
        public LfToken(ITextContext context)
            : base("\n", context)
        {
            Contract.Requires(context != null);
        }
    }
}