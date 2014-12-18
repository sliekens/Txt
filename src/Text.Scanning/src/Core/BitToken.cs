namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

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