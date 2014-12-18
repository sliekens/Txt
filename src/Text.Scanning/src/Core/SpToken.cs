namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class SpToken : Token
    {
        public SpToken(ITextContext context)
            : base('\u0020', context)
        {
            Contract.Requires(context != null);
        }
    }
}