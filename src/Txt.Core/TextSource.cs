using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Txt.Core
{
    public abstract class TextSource : ITextSource
    {
        /// <summary>
        ///     The zero-based index into the text source. This is not the index of the next unread character in
        ///     <see cref="data" />, that's <see cref="dataIndex" />.
        /// </summary>
        private long currentOffset;

        /// <summary>
        ///     This is the internal character buffer.
        ///     This buffer is not meant to improve performance. Instead, its intended use is to provide a sliding window over a
        ///     text source.
        ///     Within this window, consumer code can seek in either direction.
        /// </summary>
        /// <remarks>
        ///     Typically, the buffer is overwritten with every read or seek operation. However, when <see cref="StartRecording" />
        ///     is
        ///     called, we'll append characters instead of overwriting them, until <see cref="StopRecording" /> is called.
        /// </remarks>
        private char[] data;

        private int dataColumn = 1;

        /// <summary>
        ///     The index of the next unread character in <c>data</c>. Its range is 0..(<c>dataLength-1</c>).
        /// </summary>
        /// <remarks>
        ///     Typicaelly, the index is reset to 0 with every read or seek operation. However, when <see cref="StartRecording" />
        ///     is
        ///     called, index is increased to prevent overwriting recorded characters in <see cref="data" />, until
        ///     <see cref="StopRecording" /> is
        ///     called.
        /// </remarks>
        private int dataIndex;

        /// <summary>
        ///     The number of buffered characters in <see cref="data" />. Its range is 0..(<c>data.Length</c>).
        /// </summary>
        private int dataLength;

        private int dataLine = 1;

        /// <summary>The zero-based index into the text source at which <see cref="data" /> begins.</summary>
        private long dataOffset;

        private bool disposed;

        /// <summary>
        ///     A value indicating how many consumers expect to be able to seek to a previous offset. Do not reset the internal
        ///     buffer while this value is greater than 0.
        /// </summary>
        private int watchers;

        protected TextSource([NotNull] char[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            this.data = data;
            dataLength = this.data.Length;
        }

        protected TextSource([NotNull] char[] data, int startIndex)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(startIndex),
                    startIndex,
                    $"Precondition: {nameof(startIndex)} >= 0");
            }
            if (startIndex > data.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(startIndex),
                    startIndex,
                    $"Precondition: {nameof(startIndex)} <= {nameof(data)}::{nameof(data.Length)}");
            }
            dataLength = data.Length - startIndex;
            if (startIndex == 0)
            {
                this.data = data;
            }
            else
            {
                this.data = new char[dataLength];
                Array.Copy(data, startIndex, this.data, 0, dataLength);
            }
        }

        protected TextSource([NotNull] char[] data, int startIndex, int length)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(startIndex),
                    startIndex,
                    $"Precondition: {nameof(startIndex)} >= 0");
            }
            if (startIndex > data.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(startIndex),
                    startIndex,
                    $"Precondition: {nameof(startIndex)} <= {nameof(data)}::{nameof(data.Length)}");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length),
                    length,
                    $"Precondition: {nameof(length)} >= 0");
            }
            if (length > data.Length - startIndex)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length),
                    length,
                    $"Precondition: {nameof(length)} <= {nameof(data)}::{nameof(data.Length)} - {nameof(startIndex)}");
            }
            dataLength = length;
            if (startIndex == 0)
            {
                this.data = data;
            }
            else
            {
                this.data = new char[dataLength];
                Array.Copy(data, startIndex, this.data, 0, dataLength);
            }

        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextSource" /> class with a specified initial capacity for its
        ///     internal buffer.
        /// </summary>
        /// <param name="capacity">The inital capacity of the internal buffer.</param>
        protected TextSource(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
            data = new char[capacity];
        }

        public int Column { get; private set; } = 1;

        public int Line { get; private set; } = 1;

        /// <summary>
        ///     Gets the zero-based position within the current text source.
        /// </summary>
        public long Offset => currentOffset;

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Gets the next available character without changing the current <see cref="Offset" />.
        /// </summary>
        /// <returns>-1 if no characters are available, or a value that can be cast to <see cref="char" />.</returns>
        public int Peek()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            var unreadCount = FillBuffer(1);
            if (unreadCount == 0)
            {
                return -1;
            }
            return data[dataIndex];
        }

        /// <summary>
        ///     Gets the next available character and advances the current <see cref="Offset" /> by one character.
        /// </summary>
        /// <returns>-1 if no characters are available, or a value that can be cast to <see cref="char" />.</returns>
        public int Read()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            var unreadCount = FillBuffer(1);
            if (unreadCount == 0)
            {
                return -1;
            }
            var c = data[dataIndex];
            dataIndex++;
            currentOffset++;
            switch (c)
            {
                case '\r':
                    Column = 1;
                    break;
                case '\n':
                    Line++;
                    if (Column != 1)
                    {
                        Column = 1;
                    }
                    break;
                default:
                    Column++;
                    break;
            }
            return c;
        }

        /// <summary>
        ///     Reads between 0 and a specified maximum number of characters from the current text source into a buffer, beginning
        ///     at the specified index, and advances the current <see cref="Offset" /> by the effective number of buffered
        ///     characters.
        /// </summary>
        /// <param name="buffer">The buffer that will contain the characters.</param>
        /// <param name="startIndex">The index of <paramref name="buffer" /> at which to start copying.</param>
        /// <param name="maxCount">The maximum number of characters to read.</param>
        /// <returns>A value indicating the number of buffered characters.</returns>
        public int Read(char[] buffer, int startIndex, int maxCount)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Precondition: startIndex >= 0");
            }
            if (maxCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCount), "Precondition: maxCount >= 0");
            }
            if (startIndex + maxCount > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxCount),
                    "Precondition: startIndex + maxCount <= buffer.Length");
            }
            if (maxCount == 0)
            {
                return 0;
            }
            var unreadCount = FillBuffer(maxCount);
            maxCount = Math.Min(maxCount, unreadCount);
            Array.Copy(data, dataIndex, buffer, startIndex, maxCount);
            for (var i = dataIndex; i < dataIndex + maxCount; i++)
            {
                switch (data[i])
                {
                    case '\r':
                        Column = 1;
                        break;
                    case '\n':
                        Line++;
                        if (Column != 1)
                        {
                            Column = 1;
                        }
                        break;
                    default:
                        Column++;
                        break;
                }
            }
            dataIndex += maxCount;
            currentOffset += maxCount;
            return maxCount;
        }

        public async Task<int> ReadAsync(char[] buffer, int startIndex, int maxCount)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Precondition: startIndex >= 0");
            }
            if (maxCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCount), "Precondition: maxCount >= 0");
            }
            if (startIndex + maxCount > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxCount),
                    "Precondition: startIndex + maxCount <= buffer.Length");
            }
            if (maxCount == 0)
            {
                return 0;
            }
            var unreadCount = await FillBufferAsync(maxCount).ConfigureAwait(false);
            maxCount = Math.Min(maxCount, unreadCount);
            Array.Copy(data, dataIndex, buffer, startIndex, maxCount);
            dataIndex += maxCount;
            currentOffset += maxCount;
            return maxCount;
        }

        public int ReadBlock(char[] buffer, int startIndex, int maxCount)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            int i, n = 0;
            do
            {
                n += i = Read(buffer, startIndex + n, maxCount - n);
            } while ((i > 0) && (n < maxCount));
            return n;
        }

        public async Task<int> ReadBlockAsync(char[] buffer, int startIndex, int maxCount)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Precondition: startIndex >= 0");
            }
            if (maxCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCount), "Precondition: maxCount >= 0");
            }
            if (startIndex + maxCount > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxCount),
                    "Precondition: startIndex + maxCount <= buffer.Length");
            }
            if (maxCount == 0)
            {
                return 0;
            }
            int length, totalLength = 0;
            do
            {
                var currentOffset = startIndex + totalLength;
                var currentCount = maxCount - totalLength;
                length = await ReadAsync(buffer, currentOffset, currentCount).ConfigureAwait(false);
                totalLength += length;
            } while ((length > 0) && (totalLength < maxCount));
            return totalLength;
        }

        /// <summary>
        ///     Sets the character position within the current text source.
        /// </summary>
        /// <param name="offset">A character offset relative to the beginning of the current text source.</param>
        public void Seek(long offset)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (offset < dataOffset)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            if (offset == currentOffset)
            {
                return;
            }
            if (offset == dataOffset)
            {
                currentOffset = dataOffset;
                Line = dataLine;
                Column = dataColumn;
                dataIndex = 0;
                return;
            }
            if (offset < currentOffset)
            {
                Seek(dataOffset);
            }
            var lookahead = (int)(offset - currentOffset);
            var available = FillBuffer(lookahead);
            var seek = Math.Min(available, lookahead);
            for (var i = dataIndex; i < dataIndex + seek; i++)
            {
                switch (data[i])
                {
                    case '\r':
                        Column = 1;
                        break;
                    case '\n':
                        Line++;
                        if (Column != 1)
                        {
                            Column = 1;
                        }
                        break;
                    default:
                        Column++;
                        break;
                }
            }
            dataIndex += seek;
            currentOffset += seek;
            if (watchers == 0)
            {
                ResetBuffer();
            }
        }

        /// <summary>
        ///     Start recording characters into an internal buffer. Calling this method ensures that
        ///     <see cref="Seek" /> will not throw an exception when called with an offset that is equal or greater than the
        ///     current value of <see cref="Offset" />.
        /// </summary>
        /// <remarks>
        ///     Consumers must take responsibility of calling <see cref="StopRecording" /> when they no longer intend to reset the
        ///     current offset.
        /// </remarks>
        public long StartRecording()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (watchers == 0)
            {
                ResetBuffer();
            }
            watchers++;
            return currentOffset;
        }

        /// <summary>
        ///     Stop recording characters and clear the internal buffer.
        /// </summary>
        /// <remarks>
        ///     When <see cref="StartRecording" /> is called n times where n is n>=1, only the n-th call to
        ///     <see cref="StopRecording" /> will cause the internal buffer to be cleared.
        /// </remarks>
        public void StopRecording()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (watchers == 0)
            {
                return;
            }
            watchers--;
            if (watchers == 0)
            {
                ResetBuffer();
            }
        }

        /// <summary>
        ///     Releases unmanaged resources used by the current text source and optionally releases its managed resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether to dispose managed resources in addition to unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;
        }

        protected virtual Task<int> ReadAsyncImpl(char[] buffer, int startIndex, int maxCount)
        {
            Debug.Assert(buffer != null, "buffer != null");
            Debug.Assert(startIndex >= 0, "startIndex >= 0");
            Debug.Assert(maxCount > 0, "maxCount > 0");
            Debug.Assert(startIndex + maxCount <= buffer.Length, "startIndex + maxCount <= buffer.Length");
            return Task<int>.Factory.StartNew(() => ReadImpl(buffer, startIndex, maxCount));
        }

        protected abstract int ReadImpl([NotNull] char[] buffer, int startIndex, int maxCount);

        private int FillBuffer(int count)
        {
            if (watchers == 0)
            {
                ResetBuffer();
            }

            // Just return already if there are enough characters in the buffer
            var unreadCount = dataLength - dataIndex;
            if (unreadCount >= count)
            {
                return unreadCount;
            }

            // Resize the buffer if it is too small to hold the needed number of characters
            var need = count - unreadCount;
            var unusedCapacity = data.Length - dataLength - dataIndex;
            if (need > unusedCapacity)
            {
                Array.Resize(ref data, data.Length + need - unusedCapacity);
            }
            var length = ReadImpl(data, dataIndex, need);
            dataLength += length;
            return length + unreadCount;
        }

        private async Task<int> FillBufferAsync(int count)
        {
            if (watchers == 0)
            {
                ResetBuffer();
            }

            // Just return already if there are enough characters in the buffer
            var unreadCount = dataLength - dataIndex;
            if (unreadCount >= count)
            {
                return unreadCount;
            }

            // Resize the buffer if it is too small to hold the needed number of characters
            var need = count - unreadCount;
            var unusedCapacity = data.Length - dataLength - dataIndex;
            if (need > unusedCapacity)
            {
                Array.Resize(ref data, data.Length + need - unusedCapacity);
            }
            var length = await ReadAsyncImpl(data, dataIndex, need).ConfigureAwait(false);
            dataLength += length;
            return length + unreadCount;
        }

        /// <summary>
        ///     Removes <see cref="dataIndex" /> characters from the start of the buffer.
        /// </summary>
        private void ResetBuffer()
        {
            if (dataIndex == 0)
            {
                return;
            }
            var unreadCount = dataLength - dataIndex;
            if (unreadCount != 0)
            {
                Array.Copy(data, dataIndex, data, 0, unreadCount);
            }
            dataLength = unreadCount;
            dataIndex = 0;
            dataOffset = currentOffset;
            dataLine = Line;
            dataColumn = Column;
        }
    }
}
