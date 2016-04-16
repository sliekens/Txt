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

        public event EventHandler<PositionChangedEventArgs> OnPositionChanged;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Read()
        {
            var n = ReadImpl();
            if (n != -1)
            {
                OnPositionChanged?.Invoke(this, new PositionChangedEventArgs { Offset = 1 });
            }
            return n;
        }

        public int Read(char[] buffer, int offset, int count)
        {
            var n = ReadImpl(buffer, offset, count);
            if (n != 0)
            {
                OnPositionChanged?.Invoke(this, new PositionChangedEventArgs { Offset = n });
            }
            return n;
        }

        public virtual async Task<int> ReadAsync(char[] buffer, int offset, int count)
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
            var n = await ReadAsyncImpl(buffer, offset, count, CancellationToken.None).ConfigureAwait(false);
            if (n != 0)
            {
                OnPositionChanged?.Invoke(this, new PositionChangedEventArgs { Offset = n });
            }
            return n;
        }

        public int ReadBlock(char[] buffer, int offset, int count)
        {
            int i, n = 0;
            do
            {
                n += i = ReadImpl(buffer, offset + n, count - n);
            } while ((i > 0) && (n < count));
            if (n != 0)
            {
                OnPositionChanged?.Invoke(this, new PositionChangedEventArgs { Offset = n });
            }
            return n;
        }

        public async Task<int> ReadBlockAsync(char[] buffer, int offset, int count)
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
            var n = await ReadBlockAsyncImpl(buffer, offset, count, CancellationToken.None).ConfigureAwait(false);
            if (n != 0)
            {
                OnPositionChanged?.Invoke(this, new PositionChangedEventArgs { Offset = n });
            }
            return n;
        }

        public void Unread(char c)
        {
            UnreadImpl(c);
        }

        protected abstract void UnreadImpl(char c);

        public void Unread(char[] buffer, int offset, int count)
        {
            UnreadImpl(buffer, offset, count);
            if (count != 0)
            {
                OnPositionChanged?.Invoke(this, new PositionChangedEventArgs { Offset = -count });
            }
        }

        public async Task UnreadAsync(char[] buffer, int offset, int count)
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
            await UnreadAsyncImpl(buffer, offset, count, CancellationToken.None).ConfigureAwait(false);
            if (count != 0)
            {
                OnPositionChanged?.Invoke(this, new PositionChangedEventArgs { Offset = -count });
            }
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        protected abstract int ReadImpl();

        protected abstract int ReadImpl(char[] buffer, int offset, int count);

        protected abstract void UnreadImpl(char[] buffer, int offset, int count);

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

        private Task<int> ReadAsyncImpl(char[] buffer, int offset, int count, CancellationToken cancellationToken)
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

        private async Task<int> ReadBlockAsyncImpl(
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
                    await ReadAsyncImpl(buffer, currentOffset, currentCount, cancellationToken).ConfigureAwait(false);
                totalLength += length;
            } while ((length > 0) && (totalLength < count));
            return totalLength;
        }

        private Task UnreadAsyncImpl(char[] buffer, int offset, int count, CancellationToken cancellationToken)
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
