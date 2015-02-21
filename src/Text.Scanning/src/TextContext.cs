namespace Text.Scanning
{
    /// <summary>Represents an immutable implementation of the <see cref="ITextContext" /> interface</summary>
    public struct TextContext : ITextContext
    {
        private readonly int offset;

        /// <summary>Initializes a new instance of the <see cref="TextContext" /> struct with a specified offset.</summary>
        /// <param name="offset">The position, relative to the beginning of the data source.</param>
        public TextContext(int offset)
            : this()
        {
            this.offset = offset;
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