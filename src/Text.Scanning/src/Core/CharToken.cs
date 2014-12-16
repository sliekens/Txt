using System.Diagnostics.Contracts;

namespace Text.Core
{
    /// <summary>\u0001 - \u007F</summary>
    public class CharToken : Token
    {
        public CharToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= 0x01 && data <= 0x7F);
            Contract.Requires(context != null);
        }
    }
}
