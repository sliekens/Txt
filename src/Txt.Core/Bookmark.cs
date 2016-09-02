// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bookmark.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;

namespace Txt.Core
{
    /// <summary>Represents an immutable implementation of the <see cref="ITextContext" /> interface</summary>
    public struct Bookmark : ITextContext
    {
        /// <summary>Initializes a new instance of the <see cref="Bookmark" /> struct with a specified offset.</summary>
        /// <param name="offset">The position, relative to the beginning of the data source.</param>
        /// <param name="line"></param>
        /// <param name="column"></param>
        public Bookmark(long offset, int line, int column)
            : this()
        {
            Debug.Assert(offset >= 0, "offset >= 0");
            Debug.Assert(line >= 1, "line >= 1");
            Debug.Assert(column >= 1, "column >= 1");
            Offset = offset;
            Line = line;
            Column = column;
        }

        public int Column { get; }

        public int Line { get; }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public long Offset { get; }
    }
}
