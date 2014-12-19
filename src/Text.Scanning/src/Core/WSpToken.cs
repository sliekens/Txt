namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the WSP rule: 1 SP character -or- 1 HTAB character. Unicode: U+0020, U+0009.</summary>
    public class WSpToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HTabToken hTabToken;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpToken spToken;

        /// <summary>
        /// Creates a new instance of the <see cref="T:Text.Scanning.Core.CtlToken" /> class with a specified white space
        /// character and context.
        /// </summary>
        /// <param name="data">The space character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public WSpToken(SpToken data, ITextContext context)
            : base(data.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.spToken = data;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="T:Text.Scanning.Core.CtlToken" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="data">The horizontal tab character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public WSpToken(HTabToken data, ITextContext context)
            : base(data.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.hTabToken = data;
        }

        /// <summary>Gets the 'HTAB' component, or a <c>null</c> reference.</summary>
        public HTabToken HTab
        {
            get
            {
                return this.hTabToken;
            }
        }

        /// <summary>Gets the 'SP' component, or a <c>null</c> reference.</summary>
        public SpToken Sp
        {
            get
            {
                return this.spToken;
            }
        }
    }
}