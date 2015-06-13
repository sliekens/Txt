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
    public sealed class TextScanner : TextScannerBase
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stream inputStream;

        private bool disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextScanner" /> class that reads characters from a specified input
        ///     stream, using the US-ASCII character encoding. The stream must support seeking with a negative offset that is
        ///     relative to the
        ///     <see cref="SeekOrigin.Current" /> position within the stream. For streams that do not support
        ///     <see cref="Stream.Seek" />, use the <see cref="BufferedRextScanner" /> class instead.
        /// </summary>
        /// <param name="seekableInputStream">
        ///     The input stream to read from. The input stream must return <c>true</c> from
        ///     <see cref="Stream.CanSeek" />.
        /// </param>
        /// <exception cref="ArgumentException">The value of <see cref="Stream.CanSeek" /> is <c>false</c>.</exception>
        /// <exception cref="ArgumentNullException">The value of <paramref name="seekableInputStream" /> is a null reference.</exception>
        public TextScanner(Stream seekableInputStream)
            : this(seekableInputStream, Encoding.GetEncoding("us-ascii"))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextScanner" /> class that reads characters from a specified input
        ///     stream, using the specified character encoding. The stream must support seeking with a negative offset that is
        ///     relative to the
        ///     <see cref="SeekOrigin.Current" /> position within the stream. For streams that do not support
        ///     <see cref="Stream.Seek" />, use the <see cref="BufferedRextScanner" /> class instead.
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
            : base(encoding)
        {
            if (seekableInputStream == null)
            {
                throw new ArgumentNullException("seekableInputStream");
            }

            if (!seekableInputStream.CanSeek)
            {
                throw new ArgumentException("Precondition: Stream.CanSeek", "seekableInputStream");
            }

            this.inputStream = seekableInputStream;
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
            this.inputStream.Position -= this.Encoding.GetByteCount(values);
        }
    }
}