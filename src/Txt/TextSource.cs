using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Txt
{
    public abstract class TextSource : ITextSource
    {
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
            return PeekImpl();
        }

        protected abstract int PeekImpl();

        public int Read()
        {
            return ReadImpl();
        }

        protected abstract int ReadImpl();

        public int Read(char[] buffer, int offset, int count)
        {
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

        protected abstract int ReadImpl(char[] buffer, int offset, int count);

        public Task<int> ReadAsync(char[] buffer, int offset, int count)
        {
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
            return ReadBlockImpl(buffer, offset, count);
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

        public Task<int> ReadBlockAsync(char[] buffer, int offset, int count)
        {
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
            UnreadImpl(c);
        }

        protected abstract void UnreadImpl(char c);

        public void Unread(char[] buffer, int offset, int count)
        {
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
        protected abstract void UnreadImpl(char[] buffer, int offset, int count);

        public Task UnreadAsync(char[] buffer, int offset, int count)
        {
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
                return TaskHelper.CompletedTask;
            }
            return UnreadAsyncImpl(buffer, offset, count, CancellationToken.None);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        private static int DelegatedRead(object o)
        {
            Debug.Assert(o is InputOutputState, "o is InputOutputState");
            var state = (InputOutputState)o;
            return state.TextSource.ReadImpl(state.Buffer, state.Offset, state.Count);
        }

        private static void DelegatedUnread(object o)
        {
            Debug.Assert(o is InputOutputState, "o is InputOutputState");
            var state = (InputOutputState)o;
            state.TextSource.UnreadImpl(state.Buffer, state.Offset, state.Count);
        }

        protected virtual Task<int> ReadAsyncImpl(char[] buffer, int offset, int count, CancellationToken cancellationToken)
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

        protected virtual Task UnreadAsyncImpl(char[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Debug.Assert(buffer != null, "buffer != null");
            Debug.Assert(offset >= 0);
            Debug.Assert(count > 0);
            Debug.Assert(offset + count <= buffer.Length);
            return Task.Factory.StartNew(
                DelegatedUnread,
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
