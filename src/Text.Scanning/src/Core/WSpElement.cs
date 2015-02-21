namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the WSP rule: 1 SP character -or- 1 HTAB character. Unicode: U+0020, U+0009.</summary>
    public class WSpElement : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HTabElement hTabElement;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpElement spElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Text.Scanning.Core.CtlElement" /> class with a specified white space
        /// character and context.
        /// </summary>
        /// <param name="data">The space character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public WSpElement(SpElement data, ITextContext context)
            : base(data.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.spElement = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Text.Scanning.Core.CtlElement" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="data">The horizontal tab character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public WSpElement(HTabElement data, ITextContext context)
            : base(data.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.hTabElement = data;
        }

        /// <summary>Gets the 'HTAB' component, or a <c>null</c> reference.</summary>
        public HTabElement HTab
        {
            get
            {
                return this.hTabElement;
            }
        }

        /// <summary>Gets the 'SP' component, or a <c>null</c> reference.</summary>
        public SpElement Sp
        {
            get
            {
                return this.spElement;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.spElement != null ^ this.hTabElement != null);
        }
    }
}