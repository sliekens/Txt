// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Token.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The token.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>The token.</summary>
    public abstract class Token : ITextContext
    {
        /// <summary>The data.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string data;

        /// <summary>The offset.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int offset;

        /// <summary>Initializes a new instance of the <see cref="Token"/> class.</summary>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
        protected Token(char data, ITextContext context)
            : this(char.ToString(data), context)
        {
            Contract.Requires(context != null);
        }

        /// <summary>Initializes a new instance of the <see cref="Token"/> class.</summary>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
        protected Token(string data, ITextContext context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.data = data;
            this.offset = context.Offset;
        }

        /// <summary>Gets the data.</summary>
        public string Data
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.data;
            }
        }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public int Offset
        {
            get
            {
                return this.offset;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Data;
        }

        /// <summary>The object invariant.</summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.data != null);
        }
    }
}