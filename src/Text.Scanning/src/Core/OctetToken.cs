namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class OctetToken : Token
    {
        public OctetToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data <= (char)0xFF);
            Contract.Requires(context != null);
        }
    }
}