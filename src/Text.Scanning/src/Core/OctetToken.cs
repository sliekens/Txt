using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
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
