using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class CrLfToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LfToken lfToken;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CrToken crToken;

        public CrLfToken(CrToken crToken, LfToken lfToken, ITextContext context)
            : base("\r\n", context)
        {
            Contract.Requires(crToken != null);
            Contract.Requires(lfToken != null);
            Contract.Requires(context != null);
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