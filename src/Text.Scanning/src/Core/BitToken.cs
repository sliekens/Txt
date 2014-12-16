using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class BitToken : Token
    {
        public BitToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == '0' || data == '1');
            Contract.Requires(context != null);
        }
    }
}
