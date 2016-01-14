namespace TextFx
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public class TextScanner : ITextScanner
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITextSource textSource;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private bool disposed;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private bool endOfInput;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private int offset;

        public TextScanner(ITextSource textSource)
        {
            if (textSource == null)
            {
                throw new ArgumentNullException(nameof(textSource));
            }
            this.textSource = textSource;
        }

        /// <inheritdoc />
        public virtual bool EndOfInput
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().FullName);
                }
                return endOfInput;
            }
        }

        /// <inheritdoc />
        public virtual int Offset
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().FullName);
                }
                return offset;
            }
        }

        /// <summary>This method calls <see cref="Dispose(bool)" />, specifying <c>true</c> to release all resources.</summary>
        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public virtual ITextContext GetContext()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            return new TextContext(offset);
        }

        public virtual MatchResult TryMatch(string s)
        {
            return TryMatch(s, StringComparer.Ordinal);
        }

        public MatchResult TryMatch(string s, IEqualityComparer<string> comparer)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (s.Length == 0)
            {
                return MatchResult.FromMatch(string.Empty);
            }
            var buffer = new char[s.Length];
            var len = textSource.ReadBlock(buffer, 0, buffer.Length);
            var next = new string(buffer, 0, len);
            if (len == 0)
            {
                endOfInput = true;
                return MatchResult.FromEndOfInput();
            }
            if (!comparer.Equals(s, next))
            {
                textSource.Unread(buffer, 0, len);
                return MatchResult.FromMismatch(next);
            }
            Interlocked.Add(ref offset, len);
            return MatchResult.FromMatch(next);
        }

        /// <inheritdoc />
        public virtual MatchResult TryMatch(char c)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            var head = textSource.Read();
            if (head == -1)
            {
                endOfInput = true;
                return MatchResult.FromEndOfInput();
            }
            var next = (char) head;
            if (c != next)
            {
                textSource.Unread(next);
                return MatchResult.FromMismatch(char.ToString(next));
            }
            Interlocked.Increment(ref offset);
            return MatchResult.FromMatch(char.ToString(next));
        }

        public void Unread(string s)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (offset < s.Length)
            {
                throw new InvalidOperationException("Precondition: Offset >= s.Length");
            }

            // Special case: pushback string may be empty
            // -> no-op
            if (s.Length == 0)
            {
                return;
            }
            if (s.Length == 1)
            {
                Interlocked.Decrement(ref offset);
                textSource.Unread(s[0]);
            }
            else
            {
                var buffer = s.ToCharArray();
                Interlocked.Add(ref offset, -buffer.Length);
                textSource.Unread(buffer, 0, buffer.Length);
            }
            endOfInput = false;
        }

        public Task UnreadAsync(string s)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (offset < s.Length)
            {
                throw new InvalidOperationException("Precondition: Offset >= s.Length");
            }
            return UnreadAsyncImpl(s);
        }

        /// <inheritdoc />
        void IDisposable.Dispose()
        {
            Close();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">
        ///     <c>true</c> to clean up both managed and unmanaged resources; otherwise, <c>false</c> to clean up only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;
        }

        private async Task UnreadAsyncImpl(string s)
        {
            Debug.Assert(s != null, "s != null");
            Debug.Assert(offset < s.Length, "this.offset < s.Length");
            Debug.Assert(s.Length > 0);
            var buffer = s.ToCharArray();
            Interlocked.Add(ref offset, -buffer.Length);
            await textSource.UnreadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
        }
    }
}
