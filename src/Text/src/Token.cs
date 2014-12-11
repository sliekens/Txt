namespace Text
{
    public abstract class Token : ITextContext
    {
        private readonly int column;
        private readonly int line;
        private readonly int offset;
        private readonly string data;

        protected Token(string data, ITextContext context)
        {
            this.data = data;
            this.line = context.Line;
            this.column = context.Column;
            this.offset = context.Offset;
        }

        public string Data
        {
            get { return this.data; }
        }

        public override string ToString()
        {
            return this.Data;
        }

        /// <summary>Gets the current column number of the current <see cref="ITextContext.Line" />.</summary>
        public int Column
        {
            get
            {
                return this.column;
            }
        }

        /// <summary>Gets the current line number.</summary>
        public int Line
        {
            get
            {
                return this.line;
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
    }
}