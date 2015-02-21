// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Element.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    public abstract class Element : ITextContext
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string data;

        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int offset;

        /// <summary>TODO </summary>
        /// <param name="data">TODO </param>
        /// <param name="context">TODO </param>
        protected Element(char data, ITextContext context)
            : this(char.ToString(data), context)
        {
            Contract.Requires(context != null);
        }

        /// <summary>TODO </summary>
        /// <param name="data">TODO </param>
        /// <param name="context">TODO </param>
        protected Element(string data, ITextContext context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.data = data;
            this.offset = context.Offset;
        }

        /// <summary>TODO </summary>
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

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.data != null);
        }
    }
}