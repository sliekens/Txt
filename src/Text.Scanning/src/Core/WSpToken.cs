namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class WSpToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HTabToken hTabToken;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpToken spToken;

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

        public HTabToken HTab
        {
            get
            {
                return this.hTabToken;
            }
        }

        public SpToken Sp
        {
            get
            {
                return this.spToken;
            }
        }
    }
}