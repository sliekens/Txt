// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextScanner.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary>
    ///     Represents a text scanner that gets text from an instance of the <see cref="T:TextFx.TextScanner" />
    ///     class.
    /// </summary>
    public sealed class TextScanner : ITextScanner
    {
        public static readonly Encoding DefaultEncoding = Encoding.GetEncoding("us-ascii");

        private readonly Encoding encoding;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stream inputStream;

        /// <summary>Indicates whether this object has been disposed.</summary>
        private bool disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool endOfInput;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private char nextCharacter;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int offset = -1;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextScanner" /> class that reads characters from a specified input
        ///     stream, using the US-ASCII character encoding. The stream must support seeking with a negative offset that is
        ///     relative to the
        ///     <see cref="SeekOrigin.Current" /> position within the stream. For streams that do not support
        ///     <see cref="Stream.Seek" />, use the constructor overload that accepts an instance of the
        ///     <see cref="PushbackInputStream" /> class.
        /// </summary>
        /// <param name="seekableInputStream">
        ///     The input stream to read from. The input stream must return <c>true</c> from
        ///     <see cref="Stream.CanSeek" />.
        /// </param>
        /// <exception cref="ArgumentException">The value of <see cref="Stream.CanSeek" /> is <c>false</c>.</exception>
        /// <exception cref="ArgumentNullException">The value of <paramref name="seekableInputStream" /> is a null reference.</exception>
        public TextScanner(Stream seekableInputStream)
            : this(seekableInputStream, DefaultEncoding)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextScanner" /> class that reads characters from a specified input
        ///     stream, using the specified character encoding. The stream must support seeking with a negative offset that is
        ///     relative to the
        ///     <see cref="SeekOrigin.Current" /> position within the stream. For streams that do not support
        ///     <see cref="Stream.Seek" />, use the constructor overload that accepts an instance of the
        ///     <see cref="PushbackInputStream" /> class.
        /// </summary>
        /// <param name="seekableInputStream">
        ///     The input stream to read from. The input stream must return <c>true</c> from
        ///     <see cref="Stream.CanSeek" />.
        /// </param>
        /// <param name="encoding">The character encoding to use when converting between binary data and text.</param>
        /// <exception cref="ArgumentException">The value of <see cref="Stream.CanSeek" /> is <c>false</c>.</exception>
        /// <exception cref="ArgumentNullException">
        ///     The value of <paramref name="seekableInputStream" /> or
        ///     <paramref name="encoding" /> is a null reference.
        /// </exception>
        public TextScanner(Stream seekableInputStream, Encoding encoding)
        {
            if (seekableInputStream == null)
            {
                throw new ArgumentNullException("seekableInputStream");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            if (!seekableInputStream.CanSeek)
            {
                throw new ArgumentException("Precondition: Stream.CanSeek", "seekableInputStream");
            }

            this.inputStream = seekableInputStream;
            this.encoding = encoding;
        }

        /// <summary>Initializes a new instance of the <see cref="TextScanner" /> class that reads characters from a specified input stream, using the US-ASCII character encoding.</summary>
        /// <param name="pushbackInputStream">The <see cref="PushbackInputStream" /> to read data from.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="pushbackInputStream" /> is a null reference.</exception>
        public TextScanner(PushbackInputStream pushbackInputStream)
            : this(pushbackInputStream, DefaultEncoding)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TextScanner" /> class that reads characters from a specified input stream, using the specified character encoding.</summary>
        /// <param name="pushbackInputStream">The <see cref="PushbackInputStream" /> to read data from.</param>
        /// <param name="encoding">The character encoding to use when converting between binary data and text.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of <paramref name="pushbackInputStream" /> or
        ///     <paramref name="encoding" /> is a null reference.
        /// </exception>
        public TextScanner(PushbackInputStream pushbackInputStream, Encoding encoding)
        {
            if (pushbackInputStream == null)
            {
                throw new ArgumentNullException("pushbackInputStream");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            this.inputStream = pushbackInputStream;
            this.encoding = encoding;
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

        /// <summary>This method calls <see cref="Dispose(bool)" />, specifying <c>true</c> to release all resources.</summary>
        public void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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
        public void PutBack(string s)
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
                var pushbackLength = this.encoding.GetByteCount(pushbackCharArray);
                this.inputStream.Seek(-pushbackLength, SeekOrigin.Current);
            }
            else
            {
                var pushbackBuffer = this.encoding.GetBytes(pushbackCharArray);
                this.inputStream.Write(pushbackBuffer, 0, pushbackBuffer.Length);
            }

            this.offset -= s.Length;
            this.nextCharacter = firstcharacter;
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
                throw new InvalidOperationException("Precondition: Offset > 0");
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
                    var buffer = this.encoding.GetBytes(new[] { c });
                    this.inputStream.Write(buffer, 0, buffer.Length);
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
    }
}