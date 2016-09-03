using System;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Txt.Core
{
    public interface ITextSource : IDisposable
    {
        /// <summary>
        ///     Gets the current position in the current <see cref="Line" />.
        /// </summary>
        int Column { get; }

        Encoding Encoding { get; }

        /// <summary>
        ///     Gets the current line number.
        /// </summary>
        int Line { get; }

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

        Task<int> ReadAsync([NotNull] char[] buffer, int startIndex, int maxCount);

        int ReadBlock([NotNull] char[] buffer, int startIndex, int maxCount);

        Task<int> ReadBlockAsync([NotNull] char[] buffer, int startIndex, int maxCount);

        /// <summary>
        ///     Sets the character position within the current text source.
        /// </summary>
        /// <param name="offset">A character offset relative to the beginning of the current text source.</param>
        void Seek(long offset);

        /// <summary>
        ///     Start recording characters into an internal buffer. Calling this method ensures that
        ///     <see cref="Seek" /> will not throw an exception when called with an offset that is equal or greater
        ///     than the
        ///     current value of <see cref="Offset" />.
        /// </summary>
        /// <remarks>
        ///     Consumers must take responsibility of calling <see cref="StopRecording" /> when they no longer intend to reset the
        ///     current offset.
        /// </remarks>
        /// <returns>The <see cref="Offset" /> at which recording begins.</returns>
        long StartRecording();

        /// <summary>
        ///     Stop recording characters and clear the internal buffer.
        /// </summary>
        /// <remarks>
        ///     When <see cref="StartRecording" /> is called n times where n is n>=1, only the n-th call to
        ///     <see cref="StopRecording" /> will cause the internal buffer to be cleared.
        /// </remarks>
        void StopRecording();
    }
}
