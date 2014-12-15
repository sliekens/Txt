using System.Diagnostics.Contracts;

namespace Text.Core
{
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
