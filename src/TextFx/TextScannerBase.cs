namespace TextFx
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public abstract class TextScannerBase : ITextScanner
    {
        /// <summary>Indicates whether this object has been disposed.</summary>
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

            // Special case: pushback string may be empty (no-op)
            if (s.Length == 0)
            {
                return;
            }

            if (!this.endOfInput)
            {
                s += this.nextCharacter;
            }

            this.nextCharacter = s[0];
            var values = s.ToCharArray(1, s.Length - 1);
            this.UnreadImpl(values);
            this.offset -= s.Length;
            this.endOfInput = false;
        }

        protected abstract void UnreadImpl(char[] values);

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
    }
}