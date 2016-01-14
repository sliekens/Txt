// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextContext.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx
{
    using System.Diagnostics;

    /// <summary>Represents an immutable implementation of the <see cref="ITextContext" /> interface</summary>
    public struct TextContext : ITextContext
    {
        /// <summary>Initializes a new instance of the <see cref="TextContext" /> struct with a specified offset.</summary>
        /// <param name="offset">The position, relative to the beginning of the data source.</param>
        public TextContext(int offset)
            : this()
        {
            Debug.Assert(offset >= 0, "offset >= 0");
            Offset = offset;
        }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public int Offset { get; }
    }
}
