using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class HexDigToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DigitToken digitToken;

        public HexDigToken(DigitToken digitToken, ITextContext context)
            : base(digitToken.Data, context)
        {
            Contract.Requires(digitToken != null);
            Contract.Requires(context != null);
            this.digitToken = digitToken;
        }

        public HexDigToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires((data >= '0' && data <= '9') ||(data >= 'A' && data <= 'F') || (data >= 'a' && data <= 'f'));
            Contract.Requires(context != null);
        }

        public DigitToken DigitToken
        {
            get { return digitToken; }
        }
    }
}
