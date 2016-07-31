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
        public Bookmark(int offset)
            : this()
        {
            Debug.Assert(offset >= 0, "offset >= 0");
            Offset = offset;
        }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public int Offset { get; }
    }
}
