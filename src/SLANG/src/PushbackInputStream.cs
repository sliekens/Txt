namespace SLANG
{
    using System;
    using System.Collections.Generic;
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

        public PushbackInputStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream", "Precondition: stream != null");
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Precondition: Stream.CanRead", "stream");
            }

            this.stream = stream;
            this.pushbackStack = new Stack<PushbackContext>();
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return this.stream.CanSeek;
            }
        }

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
                throw new ArgumentNullException("buffer", "Precondition: buffer != null");
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