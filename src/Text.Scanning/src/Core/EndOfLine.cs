// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLine.cs" company="Steven Liekens">
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

    /// <summary>Represents the CRLF rule: 1 CR character followed by 1 LF character. Unicode: U+000D U+000A.</summary>
    public class EndOfLine : Element
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CarriageReturn carriageReturn;

        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LineFeed lineFeed;

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.EndOfLine"/> class with the specified
        /// characters and context.</summary>
        /// <param name="carriageReturn">The 'CR' component.</param>
        /// <param name="lineFeed">The 'LF' component.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public EndOfLine(CarriageReturn carriageReturn, LineFeed lineFeed, ITextContext context)
            : base(string.Concat(carriageReturn, lineFeed), context)
        {
            Contract.Requires(carriageReturn != null);
            Contract.Requires(lineFeed != null);
            Contract.Requires(context != null);
            Contract.Requires(lineFeed.Offset == carriageReturn.Offset + 1);
            this.carriageReturn = carriageReturn;
            this.lineFeed = lineFeed;
        }

        /// <summary>Gets the 'CR' component.</summary>
        public CarriageReturn CarriageReturn
        {
            get
            {
                return this.carriageReturn;
            }
        }

        /// <summary>Gets the 'LF' component.</summary>
        public LineFeed LineFeed
        {
            get
            {
                return this.lineFeed;
            }
        }

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.carriageReturn != null);
            Contract.Invariant(this.lineFeed != null);
        }
    }
}