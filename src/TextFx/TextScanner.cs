﻿namespace TextFx
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    public class TextScanner : ITextScanner
    {
        private readonly ITextSource textSource;

        private bool disposed;

        private bool endOfInput;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                return this.endOfInput;
            }
        }

        /// <inheritdoc />
        public virtual int Offset
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                return this.offset;
            }
        }

        /// <summary>This method calls <see cref="Dispose(bool)" />, specifying <c>true</c> to release all resources.</summary>
        public void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public virtual ITextContext GetContext()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return new TextContext(this.offset);
        }

        public virtual bool TryMatch(string s, out string next)
        {
            return this.TryMatch(s, StringComparer.Ordinal, out next);
        }

        public bool TryMatch(string s, StringComparer comparer, out string next)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (s.Length == 0)
            {
                next = s;
                return true;
            }

            var buffer = new char[s.Length];
            var len = this.textSource.Read(buffer, 0, buffer.Length);
            next = new string(buffer, 0, len);
            if (len == 0)
            {
                this.endOfInput = true;
                next = string.Empty;
                return false;
            }

            if (!comparer.Equals(s, next))
            {
                this.textSource.Unread(buffer, 0, len);
                return false;
            }

            Interlocked.Add(ref this.offset, len);
            return true;
        }

        /// <inheritdoc />
        public virtual bool TryMatch(char c, out char next)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            var head = this.textSource.Read();
            if (head == -1)
            {
                this.endOfInput = true;
                next = default(char);
                return false;
            }

            next = (char)head;
            if (c != next)
            {
                this.textSource.Unread(next);
                return false;
            }

            Interlocked.Increment(ref this.offset);
            return true;
        }

        public void Unread(string s)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (this.offset < s.Length)
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
                Interlocked.Decrement(ref this.offset);
                this.textSource.Unread(s[0]);
            }
            else
            {
                var buffer = s.ToCharArray();
                Interlocked.Add(ref this.offset, -buffer.Length);
                this.textSource.Unread(buffer, 0, buffer.Length);
            }

            this.endOfInput = false;
        }

        /// <inheritdoc />
        void IDisposable.Dispose()
        {
            this.Close();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">
        ///     <c>true</c> to clean up both managed and unmanaged resources; otherwise, <c>false</c> to clean up only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
        }
    }
}