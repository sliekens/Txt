using System;

namespace Text
{
    public class SyntaxErrorException : Exception, ITextContext
    {
        private readonly int line;
        private readonly int column;
        private readonly int offset;

        public SyntaxErrorException(ITextContext context)
        {
            this.line = context.Line;
            this.column = context.Column;
            this.offset = context.Offset;
        }

        public SyntaxErrorException(string message, ITextContext context)
            : base(message)
        {
            this.line = context.Line;
            this.column = context.Column;
            this.offset = context.Offset;
        }

        public SyntaxErrorException(string message, Exception inner, ITextContext context)
            : base(message, inner)
        {
            this.line = context.Line;
            this.column = context.Column;
            this.offset = context.Offset;
        }

        /// <summary>Gets the current line number.</summary>
        public int Line
        {
            get
            {
                return line;
            }
        }

        /// <summary>Gets the current column number of the current <see cref="ITextContext.Line" />.</summary>
        public int Column
        {
            get
            {
                return column;
            }
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
