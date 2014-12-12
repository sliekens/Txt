using System.Diagnostics;

namespace Text.Core
{
    public class CrLfToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LfToken lfToken;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CrToken crToken;

        public CrLfToken(ITextContext context, CrToken crToken, LfToken lfToken)
            : base("\r\n", context)
        {
            this.crToken = crToken;
            this.lfToken = lfToken;
        }

        public CrToken Cr
        {
            get
            {
                return this.crToken;
            }
        }

        public LfToken Lf
        {
            get
            {
                return this.lfToken;
            }
        }
    }
}