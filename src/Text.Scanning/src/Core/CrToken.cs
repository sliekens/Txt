namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class CrToken : Token
    {
        public CrToken(ITextContext context)
            : base('\r', context)
        {
            Contract.Requires(context != null);
        }
    }
}