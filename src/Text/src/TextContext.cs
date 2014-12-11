namespace Text
{
    public struct TextContext : ITextContext
    {
        private readonly int column;
        private readonly int line;
        private readonly int offset;

        public TextContext(int column, int line, int offset) : this()
        {
            this.column = column;
            this.line = line;
            this.offset = offset;
        }

        /// <summary>Gets the current column number of the current <see cref="ITextContext.Line" />.</summary>
        public int Column
        {
            get
            {
                return column;
            }
        }

        /// <summary>Gets the current line number.</summary>
        public int Line
        {
            get
            {
                return line;
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
