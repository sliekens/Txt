using System.Diagnostics;

namespace Text.Core
{
    public class HexDigToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DigitToken digitToken;

        public HexDigToken(DigitToken digitToken, ITextContext context)
            : base(digitToken.Data, context)
        {
            this.digitToken = digitToken;
        }

        public HexDigToken(string data, ITextContext context)
            : base(data, context)
        {
        }

        public DigitToken DigitToken
        {
            get { return digitToken; }
        }
    }
}
