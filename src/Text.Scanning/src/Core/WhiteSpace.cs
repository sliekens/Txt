// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the WSP rule: 1 SP character -or- 1 HTAB character. Unicode: U+0020, U+0009.</summary>
    public class WhiteSpace : Element
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HorizontalTab horizontalTab;

        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Space space;

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.WhiteSpace"/> class with a specified white space
        /// character and context.</summary>
        /// <param name="data">The space character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public WhiteSpace(Space data, ITextContext context)
            : base(data.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.space = data;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.WhiteSpace"/> class with a specified character
        /// and context.</summary>
        /// <param name="data">The horizontal tab character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public WhiteSpace(HorizontalTab data, ITextContext context)
            : base(data.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.horizontalTab = data;
        }

        /// <summary>Gets the 'HTAB' component, or a <c>null</c> reference.</summary>
        public HorizontalTab HorizontalTab
        {
            get
            {
                return this.horizontalTab;
            }
        }

        /// <summary>Gets the 'SP' component, or a <c>null</c> reference.</summary>
        public Space Sp
        {
            get
            {
                return this.space;
            }
        }

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.space != null ^ this.horizontalTab != null);
        }
    }
}