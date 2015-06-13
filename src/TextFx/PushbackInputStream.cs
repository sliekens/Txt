namespace TextFx
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    ///     Wraps an input stream and adds the ability to push data to the beginning of the stream. This is useful for
    ///     programs that require rewindable streams, regardless of whether the underlying stream supports seeking.
    /// </summary>
    public class PushbackInputStream : Stream
    {
        /// <summary>Any previous pushback buffers.</summary>
        private readonly Stack<PushbackContext> pushbackStack;

        /// <summary>The input stream that is being wrapped.</summary>
        private readonly Stream stream;

        /// <summary>The current pushback buffer.</summary>
        private byte[] pushbackBuffer;

        /// <summary>The current position within the pushback buffer.</summary>
        private int pushbackOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="PushbackInputStream"/> class that wraps a given <see cref="Stream"/> object.
        /// </summary>
        /// <param name="stream">The stream object to wrap.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="stream"/> is a null reference.</exception>
        /// <exception cref="ArgumentException">The value of <see cref="Stream.CanRead"/> is <c>false</c>.</exception>
        public PushbackInputStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Precondition: Stream.CanRead", "stream");
            }

            this.stream = stream;
            this.pushbackStack = new Stack<PushbackContext>();
        }

        /// <summary>Gets a value indicating whether the current stream supports reading.</summary>
        /// <returns>Always returns <c>true</c>.</returns>
        public override bool CanRead
        {
            get
            {
                Debug.Assert(this.stream.CanRead, "this.stream.CanRead");
                return true;
            }
        }

        /// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
        /// <returns>true if the stream supports seeking; otherwise, false.</returns>
        public override bool CanSeek
        {
            get
            {
                return this.stream.CanSeek;
            }
        }

        /// <summary>Gets a value indicating whether the current stream supports writing.</summary>
        /// <returns><c>true</c> if the stream supports pushing back bytes; <c>false</c> if the stream supports seeking.</returns>
        public override bool CanWrite
        {
            get
            {
                // MEMO: 'Write' means 'Unread' -> if CanSeek, then the caller should Seek() instead of Write()
                return !this.stream.CanSeek;
            }
        }

        /// <summary>Gets the length in bytes of the underlying stream.</summary>
        /// <returns>A long value representing the length of the stream in bytes.</returns>
        /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Length
        {
            get
            {
                return this.stream.Length;
            }
        }

        /// <summary>Gets or sets the position within the underlying stream.</summary>
        /// <returns>The current position within the stream.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Position
        {
            get
            {
                if (!this.CanSeek)
                {
                    throw new NotSupportedException("This stream does not support seek operations.");
                }

                return this.stream.Position;
            }
            set
            {
                if (!this.CanSeek)
                {
                    throw new NotSupportedException("This stream does not support seek operations.");
                }

                this.stream.Position = value;
            }
        }

        /// <summary>Clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public override void Flush()
        {
            this.stream.Flush();
            this.pushbackStack.TrimExcess();
        }

        /// <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source. </param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream. </param>
        /// <param name="count">The maximum number of bytes to be read from the current stream. </param>
        /// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int result;
            if (this.ReadFromBuffer(buffer, offset, count, out result))
            {
                return result;
            }

            return this.stream.Read(buffer, offset, count);
        }

        /// <summary>Sets the position within the current stream.</summary>
        /// <returns>The new position within the current stream.</returns>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter. </param>
        /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        /// <summary>Sets the length of the underlying stream.</summary>
        /// <param name="value">The desired length of the underlying stream in bytes. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output.</exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        /// <summary>Pushes back a sequence of bytes to the current stream and moves back the current position within this stream by the number of bytes written.</summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "Precondition: offset >= 0");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "Precondition: count >= 0");
            }

            if (count > (buffer.Length - offset))
            {
                throw new ArgumentOutOfRangeException("count", "Precondition: count <= (buffer.Length - offset)");
            }

            if (this.CanSeek)
            {
                throw new IOException(string.Format(@"Pushing back bytes to a seekable stream is an invalid operation. Use 'Seek' instead.

if (stream.CanSeek)
{{
    stream.Seek(-{0}, SeekOrigin.Current)
}}", count));
            }

            // Push the current pushback buffer (if any) onto the stack
            var currentBuffer = this.pushbackBuffer;
            if (currentBuffer != null)
            {
                var position = this.pushbackOffset;
                if (position < currentBuffer.Length)
                {
                    this.pushbackStack.Push(new PushbackContext { Buffer = currentBuffer, Offset = position });
                }
            }

            // Replace the current pushback buffer
            var tmp = new byte[count];
            Buffer.BlockCopy(buffer, offset, tmp, 0, count);
            this.pushbackBuffer = tmp;
            this.pushbackOffset = 0;
        }

        private bool ReadFromBuffer(byte[] buffer, int offset, int count, out int result)
        {
            if (this.pushbackBuffer == null)
            {
                result = default(int);
                return false;
            }

            if (this.pushbackOffset == this.pushbackBuffer.Length)
            {
                if (this.pushbackStack.Count == 0)
                {
                    result = default(int);
                    return false;
                }

                var ctx = this.pushbackStack.Pop();
                this.pushbackBuffer = ctx.Buffer;
                this.pushbackOffset = ctx.Offset;
            }

            var currentPushbackBuffer = this.pushbackBuffer;
            var currentPushbackOffset = this.pushbackOffset;
            var available = currentPushbackBuffer.Length - currentPushbackOffset;
            if (count > available)
            {
                Buffer.BlockCopy(currentPushbackBuffer, currentPushbackOffset, buffer, offset, available);
                result = available;
            }
            else
            {
                Buffer.BlockCopy(currentPushbackBuffer, currentPushbackOffset, buffer, offset, count);
                result = count;
            }

            this.pushbackOffset += result;
            return true;
        }

        private class PushbackContext
        {
            public byte[] Buffer { get; set; }

            public int Offset { get; set; }
        }
    }
}