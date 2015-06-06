namespace SLANG
{
    using System;
    using System.Diagnostics;

    public abstract class Element : ITextContext
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string values;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int offset;

        protected Element(Element element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element", "Precondition: element != null");
            }

            this.values = element.values;
            this.offset = element.offset;
        }

        protected Element(char value, ITextContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "Precondition: context != null");
            }

            this.values = char.ToString(value);
            this.offset = context.Offset;
        }

        protected Element(string values, ITextContext context)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values", "Precondition: values != null");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context", "Precondition: context != null");
            }

            this.values = values;
            this.offset = context.Offset;
        }

        public string Values
        {
            get
            {
                Debug.Assert(this.values != null);
                return this.values;
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

        /// <summary>
        ///     Gets a well-formed string that represents the current element. This is useful for elements that are
        ///     technically valid, but contain formatting errors or other inpurities. For example: mixed upper and lower case
        ///     characters where only lower case is well-formed. Unless overridden, the default return value is the value of
        ///     <see cref="Values" />.
        /// </summary>
        public virtual string GetWellFormedData()
        {
            return this.Values;
        }

        /// <inheritdoc />
        public override sealed string ToString()
        {
            return this.Values;
        }
    }
}