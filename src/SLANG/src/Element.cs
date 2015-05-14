namespace SLANG
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class Element : ITextContext
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string data;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int offset;

        public Element(Element element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element", "Precondition: element != null");
            }

            this.data = element.data;
            this.offset = element.offset;
        }

        public Element(char data, ITextContext context)
            : this(char.ToString(data), context)
        {
            Contract.Requires(context != null);
        }

        public Element(string data, ITextContext context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.data = data;
            this.offset = context.Offset;
        }

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

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.data != null);
        }
    }
}