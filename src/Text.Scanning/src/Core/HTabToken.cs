namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class HTabToken : Token
    {
        public HTabToken(ITextContext context)
            : base("\t", context)
        {
            Contract.Requires(context != null);
        }
    }
}