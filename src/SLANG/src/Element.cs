namespace SLANG
{
    using System;
    using System.Diagnostics;

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
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "Precondition: context != null");
            }

            this.data = char.ToString(data);
            this.offset = context.Offset;
        }

        public Element(string data, ITextContext context)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "Precondition: data != null");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context", "Precondition: context != null");
            }

            this.data = data;
            this.offset = context.Offset;
        }

        public string Data
        {
            get
            {
                Debug.Assert(this.data != null);
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
        public override sealed string ToString()
        {
            return this.Data;
        }
    }
}