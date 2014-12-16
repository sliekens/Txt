using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class CtlToken : Token
    {
        public CtlToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires((data >= 0x00 && data <= 0x1F) || data == 0x7F);
            Contract.Requires(context != null);
        }
    }
}
