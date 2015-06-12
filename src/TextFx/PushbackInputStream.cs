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
        /// <returns>Always returns <c>false</c>.</returns>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override long Length
        {
            get
            {
                return this.stream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return this.stream.Position;
            }
            set
            {
                this.stream.Position = value;
            }
        }

        public override void Flush()
        {
            this.stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int result;
            if (this.ReadFromBuffer(buffer, offset, count, out result))
            {
                return result;
            }

            return this.stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        /// <summary>Sets the length of the underlying stream.</summary>
        /// <param name="value">The desired length of the underlying stream in bytes. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        /// <summary>
        ///     Sets the capacity of the internal pushback buffer to the actual number of unread bytes if that number is less
        ///     than 90 percent of current capacity.
        /// </summary>
        public void Trim()
        {
            this.pushbackStack.TrimExcess();
        }

        public void Unread(byte[] buffer, int offset, int count)
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
                // MEMO: Seek() should be used when possible, because the position of the pushback buffer and the position of a seekable stream can become mismatched
                throw new IOException("Precondition: !Stream.CanSeek");
            }

            // Push the current pushbuffer (if any) onto the stack
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

        public void UnreadByte(byte b)
        {
            this.Unread(new[] { b }, 0, 1);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Precondition: Stream.CanWrite");
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