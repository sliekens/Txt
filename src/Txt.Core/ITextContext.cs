namespace Txt.Core
{
    /// <summary>Provides the interface for types that provide contextual information about their source data.</summary>
    public interface ITextContext
    {
        /// <summary>Gets the column number, relative to the beginning of the <see cref="Line" />.</summary>
        int Column { get; }

        /// <summary>Gets the line number, relative to the beginning of the text.</summary>
        int Line { get; }

        /// <summary>Gets the character position, relative to the beginning of the text.</summary>
        long Offset { get; }
    }
}
