using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public interface ITextSource2 : IDisposable
    {
        /// <summary>
        ///     Gets the zero-based position within the current text source.
        /// </summary>
        long Offset { get; }

        /// <summary>
        ///     Gets the next available character without changing the current <see cref="Offset" />.
        /// </summary>
        /// <returns>-1 if no characters are available, or a value that can be cast to <see cref="char" />.</returns>
        int Peek();

        /// <summary>
        ///     Gets the next available character and advances the current <see cref="Offset" /> by one character.
        /// </summary>
        /// <returns>-1 if no characters are available, or a value that can be cast to <see cref="char" />.</returns>
        int Read();

        /// <summary>
        ///     Reads between 0 and a specified maximum number of characters from the current text source into a buffer, beginning
        ///     at the specified index, and advances the current <see cref="Offset" /> by the effective number of buffered
        ///     characters.
        /// </summary>
        /// <param name="buffer">The buffer that will contain the characters.</param>
        /// <param name="startIndex">The index of <paramref name="buffer" /> at which to start copying.</param>
        /// <param name="maxCount">The maximum number of characters to read.</param>
        /// <returns>A value indicating the number of buffered characters.</returns>
        int Read([NotNull] char[] buffer, int startIndex, int maxCount);

        /// <summary>
        ///     Sets the character position within the current text source.
        /// </summary>
        /// <param name="offset">A character offset relative to the beginning of the current text source.</param>
        void Seek(long offset);
    }
}
