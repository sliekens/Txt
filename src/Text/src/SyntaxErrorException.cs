using System;
using System.Diagnostics.Contracts;

namespace Text
{
    public class SyntaxErrorException : Exception, ITextContext
    {
        private readonly int offset;

        public SyntaxErrorException(ITextContext context)
        {
            Contract.Requires(context != null);
            this.offset = context.Offset;
        }

        public SyntaxErrorException(string message, ITextContext context)
            : base(message)
        {
            Contract.Requires(context != null);
            this.offset = context.Offset;
        }

        public SyntaxErrorException(string message, Exception inner, ITextContext context)
            : base(message, inner)
        {
            Contract.Requires(context != null);
            this.offset = context.Offset;
        }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public int Offset
        {
            get
            {
                return offset;
            }
        }
    }
}
