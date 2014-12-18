namespace Text.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;

    public sealed class TextScanner : ITextScanner
    {
        /// <summary>Indicates whether this object has been disposed.</summary>
        private bool disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool endOfInput;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private char nextCharacter;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int offset = -1;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stack<char> buffer = new Stack<char>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TextReader textReader;

        public TextScanner(TextReader textReader)
        {
            Contract.Requires(textReader != null);
            this.textReader = textReader;
        }

        /// <inheritdoc />
        bool ITextScanner.EndOfInput
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
        char? ITextScanner.NextCharacter
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                if (this.offset == -1)
                {
                    return null;
                }

                if (this.endOfInput)
                {
                    return null;
                }

                return this.nextCharacter;
            }
        }

        /// <inheritdoc />
        int ITextContext.Offset
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
        void IDisposable.Dispose()
        {
            this.Close();
        }

        /// <inheritdoc />
        ITextContext ITextScanner.GetContext()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return new TextContext(this.offset);
        }

        /// <inheritdoc />
        void ITextScanner.PutBack(char c)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.endOfInput)
            {
                this.endOfInput = false;
            }
            else
            {
                this.buffer.Push(this.nextCharacter);
            }

            this.offset--;
            this.nextCharacter = c;
        }

        /// <inheritdoc />
        bool ITextScanner.Read()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.endOfInput)
            {
                return false;
            }

            if (this.buffer.Any())
            {
                this.nextCharacter = this.buffer.Pop();
                this.offset++;
            }
            else
            {
                lock (this.textReader)
                {
                    this.nextCharacter = (char)this.textReader.Read();
                    this.offset++;
                }
            }

            if (this.nextCharacter == char.MaxValue)
            {
                this.endOfInput = true;
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        bool ITextScanner.TryMatch(char c)
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

            ITextScanner self = this;
            self.Read();

            return true;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">
        /// <c>true</c> to clean up both managed and unmanaged resources; otherwise, <c>false</c> to clean
        /// up only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.textReader.Dispose();
            }


            this.disposed = true;
        }
    }
}