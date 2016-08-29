using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public abstract class TextSource2 : ITextSource2
    {
        private readonly char[] data;

        private int dataIndex;

        private int dataLength;

        private bool disposed;

        private int watchers;

        private long offset;

        protected TextSource2([NotNull] char[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            dataLength = data.Length;
            this.data = data;
        }

        protected TextSource2(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
            data = new char[capacity];
        }

        public long Offset => offset;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Peek()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            var unread = FillBuffer(1);
            if (unread == 0)
            {
                return -1;
            }
            return data[dataIndex];
        }

        public int Read()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            var unread = FillBuffer(1);
            if (unread == 0)
            {
                return -1;
            }
            var c = data[dataIndex];
            this.dataIndex++;
            this.offset++;
            return c;
        }

        public int Read(char[] buffer, int offset, int count)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Precondition: offset >= 0");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: count >= 0");
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: offset + count <= buffer.Length");
            }
            if (count == 0)
            {
                return 0;
            }
            var unread = FillBuffer(count);
            count = Math.Min(count, unread);
            Array.Copy(data, dataIndex, buffer, offset, count);
            this.dataIndex += count;
            this.offset += count;
            return count;
        }

        public void Seek(long offset)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (this.offset == offset)
            {
                return;
            }
            var diff = Math.Abs(this.offset - offset);
            if (this.offset > offset)
            {
                // backtrack
                if (diff > dataIndex)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }
                dataIndex -= (int)diff;
            }
            else
            {
                // lookahead
                if (diff > (data.Length - dataIndex))
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }
                var unread = FillBuffer((int)diff);
                dataIndex += (int)Math.Min(diff, unread);

            }
            this.offset = offset;
            if (watchers == 0)
            {
                ResetBuffer();
            }
        }

        public void StartRecording()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            watchers++;
        }

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

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;
        }

        protected abstract int ReadImpl([NotNull] char[] buffer, int offset, int count);

        private int FillBuffer(int count)
        {
            if (data.Length == 0)
            {
                return 0;
            }
            if (watchers == 0)
            {
                ResetBuffer();
            }
            if (dataLength == data.Length)
            {
                if (dataIndex == dataLength)
                {
                    throw new InvalidOperationException(
                        @"Buffer has reached maximum capacity. Consider increasing the buffer size.");
                }
                return dataLength - dataIndex;
            }
            var buffered = dataLength - dataIndex;
            count = Math.Max(count - buffered, 0);
            if (count == 0)
            {
                return buffered;
            }
            var length = ReadImpl(data, dataIndex, count);
            dataLength += length;
            return length + buffered;
        }

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
        }
    }
}
