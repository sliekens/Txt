using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Txt.Core
{
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

        public TextScanner([NotNull] ITextSource textSource)
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
                    throw new ObjectDisposedException(nameof(TextScanner));
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
                    throw new ObjectDisposedException(nameof(TextScanner));
                }
                return offset;
            }
        }

        public int Line
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Column
        {
            get
            {
                throw new NotImplementedException();
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
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            return new Bookmark(offset);
        }

        public int Peek()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            var peek = textSource.Peek();
            if (peek == -1)
            {
                endOfInput = true;
            }
            return peek;
        }

        public int Read()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            var read = textSource.Read();
            if (read == -1)
            {
                endOfInput = true;
                return -1;
            }
            Interlocked.Increment(ref offset);
            return read;
        }

        /// <summary>
        ///     Advances the scanner's position if the next available character matches the conditions defined by the specified
        ///     predicate.
        /// </summary>
        /// <param name="predicate">The <see cref="Predicate{T}" /> that defines the conditions of the character to match.</param>
        /// <returns>
        ///     A value container that contains the next available character and another value indicating whether it matches
        ///     the conditions defined by the predicate.
        /// </returns>
        public MatchResult TryMatch([NotNull] Predicate<char> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            var head = textSource.Peek();
            if (head == -1)
            {
                endOfInput = true;
                return new MatchResult(true, false, string.Empty, string.Empty);
            }

            var next = (char)head;
            var text = char.ToString(next);
            if (!predicate(next))
            {
                return new MatchResult(false, false, text, string.Empty);
            }

            textSource.Read();
            Interlocked.Increment(ref offset);
            return new MatchResult(false, true, text, text);
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
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            if (s.Length == 0)
            {
                if (textSource.Peek() == -1)
                {
                    return MatchResult.FromEndOfInput(s);
                }
                return MatchResult.FromMatch(string.Empty, s);
            }
            var buffer = new char[s.Length];
            var len = textSource.ReadBlock(buffer, 0, buffer.Length);
            var next = new string(buffer, 0, len);
            if (len == 0)
            {
                endOfInput = true;
                return MatchResult.FromEndOfInput(s);
            }
            if (!comparer.Equals(s, next))
            {
                textSource.Unread(buffer, 0, len);
                return MatchResult.FromMismatch(next, s);
            }
            Interlocked.Add(ref offset, len);
            return MatchResult.FromMatch(next, s);
        }

        /// <inheritdoc />
        public virtual MatchResult TryMatch(char c)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            var head = textSource.Peek();
            var expected = char.ToString(c);
            if (head == -1)
            {
                endOfInput = true;
                return MatchResult.FromEndOfInput(expected);
            }
            var next = (char)head;
            var text = char.ToString(next);
            if (c != next)
            {
                return MatchResult.FromMismatch(text, expected);
            }
            textSource.Read();
            Interlocked.Increment(ref offset);
            return MatchResult.FromMatch(text, expected);
        }

        public void Unread(string s)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TextScanner));
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
                throw new ObjectDisposedException(nameof(TextScanner));
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
