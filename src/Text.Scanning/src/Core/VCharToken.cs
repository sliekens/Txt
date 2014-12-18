namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class VCharToken : Token
    {
        public VCharToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\u0021' && data <= '\u007E');
            Contract.Requires(context != null);
        }
    }
}