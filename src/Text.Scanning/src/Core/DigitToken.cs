namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    public class DigitToken : Token
    {
        public DigitToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '0' && data <= '9');
            Contract.Requires(context != null);
        }
    }
}