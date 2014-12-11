namespace Text
{
    /// <summary>Provides the interface for types that provide contextual information about their source data.</summary>
    public interface ITextContext
    {
        /// <summary>Gets the current column number of the current <see cref="Line" />.</summary>
        int Column { get; }

        /// <summary>Gets the current line number.</summary>
        int Line { get; }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        int Offset { get; }
    }
}