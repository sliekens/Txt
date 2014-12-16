using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class WSpToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpToken spToken;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HTabToken hTabToken;

        public WSpToken(SpToken data, ITextContext context)
            : base(data.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.spToken = data;
        }

        public WSpToken(HTabToken data, ITextContext context)
            : base(data.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.hTabToken = data;
        }

        public SpToken Sp
        {
            get
            {
                return this.spToken;
            }
        }

        public HTabToken HTab
        {
            get
            {
                return this.hTabToken;
            }
        }
    }
}
