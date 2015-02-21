namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CRLF rule: 1 CR character followed by 1 LF character. Unicode: U+000D U+000A.</summary>
    public class CrLfElement : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CrElement crElement;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LfElement lfElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Text.Scanning.Core.CrLfElement" /> class with the specified
        /// characters and context.
        /// </summary>
        /// <param name="crElement">The 'CR' component.</param>
        /// <param name="lfElement">The 'LF' component.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public CrLfElement(CrElement crElement, LfElement lfElement, ITextContext context)
            : base(string.Concat(crElement, lfElement), context)
        {
            Contract.Requires(crElement != null);
            Contract.Requires(lfElement != null);
            Contract.Requires(context != null);
            Contract.Requires(lfElement.Offset == crElement.Offset + 1);
            this.crElement = crElement;
            this.lfElement = lfElement;
        }

        /// <summary>Gets the 'CR' component.</summary>
        public CrElement Cr
        {
            get
            {
                return this.crElement;
            }
        }

        /// <summary>Gets the 'LF' component.</summary>
        public LfElement Lf
        {
            get
            {
                return this.lfElement;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.crElement != null);
            Contract.Invariant(this.lfElement != null);
        }
    }
}