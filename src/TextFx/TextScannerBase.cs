using System.Collections.Generic;

namespace TextFx
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public abstract class TextScannerBase : ITextScanner
    {
        private bool disposed;

        private bool endOfInput;

        private char nextCharacter;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int offset = -1;

        private readonly Encoding encoding;

        protected TextScannerBase(Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            this.encoding = encoding;
        }

        /// <summary>This method calls <see cref="Dispose(bool)" />, specifying <c>true</c> to release all resources.</summary>
        public void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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

        /// <inheritdoc />
        public virtual char? NextCharacter
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                if (this.EndOfInput)
                {
                    return null;
                }

                return this.nextCharacter;
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
        public virtual bool Read()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.endOfInput)
            {
                return false;
            }

            this.offset += 1;

            if (!this.ReadImpl(out this.nextCharacter))
            {
                this.endOfInput = true;
                return false;
            }

            return true;
        }

        protected abstract bool ReadImpl(out char c);

        /// <inheritdoc />
        public virtual ITextContext GetContext()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return new TextContext(this.offset);
        }

        public Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
        }

        public void Unread(string s)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (s == null)
            {
                throw new ArgumentNullException("s");
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

            char[] unreadBuffer;
            if (this.endOfInput)
            {
                // Special case: pushback string may be the terminating character
                // -> take down EOF flag but push back nothing to the underlying stream
                unreadBuffer = s.Length == 1 ? null : s.ToCharArray(1, s.Length - 1);
            }
            else
            {
                unreadBuffer = new char[s.Length];
                int i;
                var count = s.Length - 1;
                for (i = 0; i < count; i++)
                {
                    unreadBuffer[i] = s[i + 1];
                }

                unreadBuffer[i] = this.nextCharacter;
            }

            this.nextCharacter = s[0];
            this.offset -= s.Length;
            this.endOfInput = false;
            if (unreadBuffer != null)
            {
                this.UnreadImpl(unreadBuffer);
            }
        }

        protected abstract void UnreadImpl(char[] values);

        protected abstract void UnreadImpl(byte[] values);

        /// <inheritdoc />
        public virtual bool TryMatch(char c)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.offset == -1)
            {
                throw new InvalidOperationException("No next character available: call 'Read()' to initialize.");
            }

            if (this.endOfInput)
            {
                throw new InvalidOperationException("No next character available: end of input has been reached.");
            }

            if (this.nextCharacter != c)
            {
                return false;
            }

            this.Read();
            return true;
        }

        /// <inheritdoc />
        public virtual bool TryMatchIgnoreCase(char c, out char match)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.offset == -1)
            {
                throw new InvalidOperationException("No next character available: call 'Read()' to initialize.");
            }

            if (this.endOfInput)
            {
                throw new InvalidOperationException("No next character available: end of input has been reached.");
            }

            if (char.ToUpperInvariant(this.nextCharacter) != char.ToUpperInvariant(c))
            {
                match = default(char);
                return false;
            }

            match = this.nextCharacter;
            this.Read();
            return true;
        }

        public Stream BaseStream
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                return this.GetBaseStreamImpl() ?? Stream.Null;
            }
        }

        protected abstract Stream GetBaseStreamImpl();

        /// <inheritdoc />
        public virtual void Reset()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            this.offset = -1;
            if (!this.endOfInput)
            {
                this.UnreadImpl(new[] { this.nextCharacter });
            }

            this.endOfInput = default(bool);
            this.nextCharacter = default(char);
        }
    }
}