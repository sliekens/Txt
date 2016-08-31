using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Txt.Core
{
    public abstract class TextSource : ITextSource
    {
        private bool disposed;

        ~TextSource()
        {
            Dispose(false);
        }

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
            return PeekImpl();
        }

        public int Read()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            return ReadImpl();
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
            return ReadImpl(buffer, offset, count);
        }

        public Task<int> ReadAsync(char[] buffer, int offset, int count)
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
                return Task.FromResult(0);
            }
            return ReadAsyncImpl(buffer, offset, count, CancellationToken.None);
        }

        public int ReadBlock(char[] buffer, int offset, int count)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            return ReadBlockImpl(buffer, offset, count);
        }

        public Task<int> ReadBlockAsync(char[] buffer, int offset, int count)
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
                return Task.FromResult(0);
            }
            return ReadBlockAsyncImpl(buffer, offset, count, CancellationToken.None);
        }

        public void Unread(char c)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            UnreadImpl(c);
        }

        public void Unread(char[] buffer, int offset, int count)
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
                return;
            }
            UnreadImpl(buffer, offset, count);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }

        protected abstract int PeekImpl();

        protected virtual Task<int> ReadAsyncImpl(
            char[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken)
        {
            Debug.Assert(buffer != null, "buffer != null");
            Debug.Assert(offset >= 0);
            Debug.Assert(count > 0);
            Debug.Assert(offset + count <= buffer.Length);
            return Task<int>.Factory.StartNew(
                                DelegatedRead,
                                new InputOutputState
                                {
                                    TextSource = this,
                                    Buffer = buffer,
                                    Offset = offset,
                                    Count = count,
                                    CancellationToken = cancellationToken
                                },
                                cancellationToken);
        }

        protected virtual async Task<int> ReadBlockAsyncImpl(
            char[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken)
        {
            Debug.Assert(buffer != null, "buffer != null");
            Debug.Assert(offset >= 0, "offset >= 0");
            Debug.Assert(count > 0, "count > 0");
            Debug.Assert(offset + count <= buffer.Length);
            int length, totalLength = 0;
            do
            {
                var currentOffset = offset + totalLength;
                var currentCount = count - totalLength;
                length =
                    await
                        ReadAsyncImpl(buffer, currentOffset, currentCount, cancellationToken).ConfigureAwait(false);
                totalLength += length;
            } while ((length > 0) && (totalLength < count));
            return totalLength;
        }

        protected virtual int ReadBlockImpl(char[] buffer, int offset, int count)
        {
            int i, n = 0;
            do
            {
                n += i = Read(buffer, offset + n, count - n);
            } while ((i > 0) && (n < count));
            return n;
        }

        protected abstract int ReadImpl();

        protected abstract int ReadImpl(char[] buffer, int offset, int count);

        protected abstract void UnreadImpl(char c);

        protected abstract void UnreadImpl(char[] buffer, int offset, int count);

        private static int DelegatedRead(object o)
        {
            Debug.Assert(o is InputOutputState, "o is InputOutputState");
            var state = (InputOutputState)o;
            return state.TextSource.ReadImpl(state.Buffer, state.Offset, state.Count);
        }

        private class InputOutputState
        {
            public char[] Buffer { get; set; }

            public CancellationToken CancellationToken { get; set; }

            public int Count { get; set; }

            public int Offset { get; set; }

            public TextSource TextSource { get; set; }
        }
    }
}
