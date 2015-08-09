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
    public sealed class BufferedTextScanner : TextScannerBase
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stream inputStream;

        private bool disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BufferedTextScanner" /> class that reads characters from a
        ///     specified input stream, using the US-ASCII character encoding.
        /// </summary>
        /// <param name="pushbackInputStream">The <see cref="PushbackInputStream" /> to read data from.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="pushbackInputStream" /> is a null reference.</exception>
        public BufferedTextScanner(PushbackInputStream pushbackInputStream)
            : this(pushbackInputStream, Encoding.GetEncoding("us-ascii"))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BufferedTextScanner" /> class that reads characters from a
        ///     specified input stream, using the specified character encoding.
        /// </summary>
        /// <param name="pushbackInputStream">The <see cref="PushbackInputStream" /> to read data from.</param>
        /// <param name="encoding">The character encoding to use when converting between binary data and text.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of <paramref name="pushbackInputStream" /> or
        ///     <paramref name="encoding" /> is a null reference.
        /// </exception>
        public BufferedTextScanner(PushbackInputStream pushbackInputStream, Encoding encoding)
            : base(encoding)
        {
            if (pushbackInputStream == null)
            {
                throw new ArgumentNullException("pushbackInputStream");
            }

            this.inputStream = pushbackInputStream;
        }

        protected override void Dispose(bool disposing)
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
            base.Dispose(disposing);
        }

        protected override bool ReadImpl(out char c)
        {
            var nextByte = this.inputStream.ReadByte();
            if (nextByte == -1)
            {
                c = default(char);
                return false;
            }

            c = (char)nextByte;
            return true;
        }

        protected override void UnreadImpl(char[] values)
        {
            if (values.Length == 0)
            {
                return;
            }

            if (this.inputStream.CanSeek)
            {
                var length = this.Encoding.GetByteCount(values);
                this.inputStream.Seek(-length, SeekOrigin.Current);
            }
            else
            {
                var pushbackBuffer = this.Encoding.GetBytes(values);
                this.inputStream.Write(pushbackBuffer, 0, pushbackBuffer.Length);
            }
        }
    }
}