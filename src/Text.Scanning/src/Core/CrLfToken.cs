// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrLfToken.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the CRLF rule: 1 CR character followed by 1 LF character. Unicode: U+000D U+000A.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CRLF rule: 1 CR character followed by 1 LF character. Unicode: U+000D U+000A.</summary>
    public class CrLfToken : Token
    {
        /// <summary>The cr token.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CrToken crToken;

        /// <summary>The lf token.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LfToken lfToken;

        /// <summary>Initializes a new instance of the <see cref="CrLfToken"/> class. Creates a new instance of the <see cref="T:Text.Scanning.Core.CrLfToken"/> class with the specified
        /// characters and context.</summary>
        /// <param name="crToken">The 'CR' component.</param>
        /// <param name="lfToken">The 'LF' component.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public CrLfToken(CrToken crToken, LfToken lfToken, ITextContext context)
            : base(string.Concat(crToken, lfToken), context)
        {
            Contract.Requires(crToken != null);
            Contract.Requires(lfToken != null);
            Contract.Requires(context != null);
            Contract.Requires(lfToken.Offset == crToken.Offset + 1);
            this.crToken = crToken;
            this.lfToken = lfToken;
        }

        /// <summary>Gets the 'CR' component.</summary>
        public CrToken Cr
        {
            get
            {
                return this.crToken;
            }
        }

        /// <summary>Gets the 'LF' component.</summary>
        public LfToken Lf
        {
            get
            {
                return this.lfToken;
            }
        }

        /// <summary>The object invariant.</summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.crToken != null);
            Contract.Invariant(this.lfToken != null);
        }
    }
}