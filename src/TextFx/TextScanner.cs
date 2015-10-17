namespace TextFx
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public class TextScanner : ITextScanner
    {
        private readonly ITextSource textSource;

        private bool disposed;

        private bool endOfInput;

        private char nextCharacter;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int offset = -1;

        private readonly Encoding encoding;

        public TextScanner(ITextSource textSource)
        {
            if (textSource == null)
            {
                throw new ArgumentNullException(nameof(textSource));
            }

            this.textSource = textSource;
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
            int next;
            if ((next = this.textSource.Read()) == -1)
            {
                this.endOfInput = true;
                return false;
            }

            this.nextCharacter = (char)next;
            return true;
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

            if (s.Length == 1 && this.endOfInput)
            {
                // Special case: pusback string is the last character before EOF
                // -> take down EOF flag and reset position but push back nothing to the underlying text source
                this.offset -= 1;
                this.endOfInput = false;
            }
            else
            {
                if (this.endOfInput)
                {
                    var buffer = s.ToCharArray(1, s.Length - 1);
                    this.textSource.Unread(buffer, 0, s.Length - 1);
                }
                else
                {
                    var buffer = new char[s.Length];
                    s.CopyTo(1, buffer, 0, s.Length - 1);
                    buffer[s.Length - 1] = this.nextCharacter;
                    this.textSource.Unread(buffer, 0, buffer.Length);
                }

                this.offset -= s.Length;
                this.endOfInput = false;
            }

            this.nextCharacter = s[0];
        }

        /// <inheritdoc />
        public virtual bool TryMatch(char c, out char next)
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

            next = this.nextCharacter;
            if (next != c)
            {
                return false;
            }

            this.Read();
            return true;
        }

        /// <inheritdoc />
        public virtual bool TryMatchIgnoreCase(char c, out char next)
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

            next = this.nextCharacter;
            if (char.ToUpperInvariant(next) != char.ToUpperInvariant(c))
            {
                return false;
            }

            this.Read();
            return true;
        }

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
                this.textSource.Unread(this.nextCharacter);
            }

            this.endOfInput = default(bool);
            this.nextCharacter = default(char);
        }
    }
}