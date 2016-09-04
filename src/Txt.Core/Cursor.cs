using System.Diagnostics;

namespace Txt.Core
{
    /// <summary>Represents an immutable implementation of the <see cref="ITextContext" /> interface.</summary>
    public struct Cursor : ITextContext
    {
        /// <summary>Initializes a new instance of the <see cref="Cursor" /> class with a specified offset, line number and column number.</summary>
        /// <param name="offset">The character position, relative to the beginning of the text.</param>
        /// <param name="line">The line number, relative to the beginning of the text.</param>
        /// <param name="column">The column number, relative to the beginning of the <paramref name="line"/>.</param>
        public Cursor(long offset, int line, int column)
            : this()
        {
            Debug.Assert(offset >= 0, "offset >= 0");
            Debug.Assert(line >= 1, "line >= 1");
            Debug.Assert(column >= 1, "column >= 1");
            Offset = offset;
            Line = line;
            Column = column;
        }

        /// <summary>Gets the column number, relative to the beginning of the <see cref="Line" />.</summary>
        public int Column { get; }

        /// <summary>Gets the line number, relative to the beginning of the text.</summary>
        public int Line { get; }

        /// <summary>Gets the character position, relative to the beginning of the text.</summary>
        public long Offset { get; }
    }
}
