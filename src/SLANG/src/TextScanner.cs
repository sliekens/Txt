// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextScanner.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Represents a text scanner that gets text from an instance of the <see cref="T:SLANG.TextScanner" />
    /// class.
    /// </summary>
    public sealed class TextScanner : ITextScanner
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PushbackInputStream inputStream;

        /// <summary>Indicates whether this object has been disposed.</summary>
        private bool disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool endOfInput;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private char nextCharacter;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int offset = -1;

        /// <summary>Initializes a new instance of the <see cref="T:SLANG.TextScanner"/> class for the given data source.</summary>
        /// <param name="inputStream">The <see cref="PushbackInputStream"/> to read data from.</param>
        public TextScanner(PushbackInputStream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream", "Precondition: inputStream != null");
            }

            this.inputStream = inputStream;
        }

        /// <inheritdoc />
        public int Offset
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
        public bool EndOfInput
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
        public char? NextCharacter
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
        public void PutBack(string s)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (s == null)
            {
                throw new ArgumentNullException("s", "Precondition: s != null");
            }

            if (this.offset < s.Length)
            {
                throw new InvalidOperationException("Precondition failed: Offset >= s.Length");
            }

            // Special case: pushback string may be empty (no-op)
            if (s.Length == 0)
            {
                return;
            }

            var firstcharacter = s[0];

            // Common case: pushback string may contain only 1 character
            if (s.Length == 1)
            {
                this.PutBack(firstcharacter);
                return;
            }

            // Special case: pushback string may contain many characters
            var pushbackCharArray = s.ToCharArray(1, s.Length - 1);
            if (this.endOfInput)
            {
                this.endOfInput = false;
            }
            else
            {
                Array.Resize(ref pushbackCharArray, pushbackCharArray.Length + 1);
                pushbackCharArray[pushbackCharArray.Length - 1] = this.nextCharacter;
            }

            if (this.inputStream.CanSeek)
            {
                var pushbackLength = Encoding.UTF8.GetByteCount(pushbackCharArray);
                this.inputStream.Seek(-pushbackLength, SeekOrigin.Current);
            }
            else
            {
                var pushbackBuffer = Encoding.UTF8.GetBytes(pushbackCharArray);
                this.inputStream.Unread(pushbackBuffer, 0, pushbackBuffer.Length);
            }

            this.offset -= s.Length;
            this.nextCharacter = firstcharacter;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing"><c>true</c> to clean up both managed and unmanaged resources; otherwise, <c>false</c> to clean up only unmanaged
        /// resources.</param>
        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.inputStream.Dispose();
            }

            this.disposed = true;
        }

        /// <inheritdoc />
        public ITextContext GetContext()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return new TextContext(this.offset);
        }

        /// <inheritdoc />
        public void PutBack(char c)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.offset == 0)
            {
                throw new InvalidOperationException("Precondition failed: Offset > 0");
            }

            if (this.endOfInput)
            {
                this.endOfInput = false;
            }
            else
            {
                if (this.inputStream.CanSeek)
                {
                    this.inputStream.Seek(-1, SeekOrigin.Current);
                }
                else
                {
                    this.inputStream.UnreadByte(Convert.ToByte(this.nextCharacter));
                }
            }

            this.offset -= 1;
            this.nextCharacter = c;
        }

        /// <inheritdoc />
        public bool Read()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.endOfInput)
            {
                return false;
            }

            lock (this.inputStream)
            {
                this.nextCharacter = (char)this.inputStream.ReadByte();
            }

            this.offset += 1;
            if (this.nextCharacter == char.MaxValue)
            {
                this.endOfInput = true;
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public bool TryMatch(char c)
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

        public bool TryMatchIgnoreCase(char c, out char match)
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