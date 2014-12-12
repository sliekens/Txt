namespace Text
{
    public struct TextContext : ITextContext
    {
        private readonly int offset;

        public TextContext(int offset) : this()
        {
            this.offset = offset;
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
