using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Jetbrains.Annotations;

namespace Txt
{
    /// <summary>
    ///     Wraps an input stream and adds the ability to push data to the beginning of the stream. This is useful for
    ///     programs that require rewindable streams, regardless of whether the underlying stream supports seeking.
    /// </summary>
    public class PushbackInputStream : Stream
    {
        /// <summary>The current pushback buffer.</summary>
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly Stack<byte> pushback;

        /// <summary>The input stream that is being wrapped.</summary>
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly Stream stream;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PushbackInputStream" /> class that wraps a given <see cref="Stream" />
        ///     object.
        /// </summary>
        /// <param name="stream">The stream object to wrap.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="stream" /> is a null reference.</exception>
        /// <exception cref="ArgumentException">The value of <see cref="Stream.CanRead" /> is <c>false</c>.</exception>
        public PushbackInputStream([NotNull] Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (!stream.CanRead)
            {
                throw new ArgumentException("Stream is closed or does not support reading", nameof(stream));
            }
            this.stream = stream;
            pushback = new Stack<byte>();
        }

        /// <summary>Gets a value indicating whether the current stream supports reading.</summary>
        /// <returns><c>false</c> if the stream is closed; otherwise, <c>true</c>.</returns>
        public override bool CanRead
        {
            get
            {
                Debug.Assert(stream.CanRead, "this.stream.CanRead");
                return stream.CanRead;
            }
        }

        /// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
        /// <returns>true if the stream supports seeking; otherwise, false.</returns>
        public override bool CanSeek => stream.CanSeek;

        /// <summary>Gets a value indicating whether the current stream supports writing.</summary>
        /// <returns><c>true</c> if the stream supports pushing back bytes; <c>false</c> if the stream supports seeking.</returns>
        public override bool CanWrite => !stream.CanSeek;

        /// <summary>Gets the length in bytes of the underlying stream.</summary>
        /// <returns>A long value representing the length of the stream in bytes.</returns>
        /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Length => stream.Length;

        /// <summary>Gets or sets the position within the underlying stream.</summary>
        /// <returns>The current position within the stream.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Position
        {
            get
            {
                if (!CanSeek)
                {
                    throw new NotSupportedException("This stream does not support seek operations.");
                }
                return stream.Position;
            }
            set
            {
                if (!CanSeek)
                {
                    throw new NotSupportedException("This stream does not support seek operations.");
                }
                stream.Position = value;
            }
        }

        /// <summary>Clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public override void Flush()
        {
            stream.Flush();
            pushback.TrimExcess();
        }

        /// <summary>
        ///     Reads a sequence of bytes from the current stream and advances the position within the stream by the number of
        ///     bytes read.
        /// </summary>
        /// <returns>
        ///     The total number of bytes read into the buffer. This can be less than the number of bytes requested if that
        ///     many bytes are not currently available, or zero (0) if the end of the stream has been reached.
        /// </returns>
        /// <param name="buffer">
        ///     An array of bytes. When this method returns, the buffer contains the specified byte array with the
        ///     values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced
        ///     by the bytes read from the current source.
        /// </param>
        /// <param name="offset">
        ///     The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read
        ///     from the current stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to be read from the current stream. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     The sum of <paramref name="offset" /> and <paramref name="count" /> is
        ///     larger than the buffer length.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="buffer" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="offset" /> or <paramref name="count" /> is
        ///     negative.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int result;
            if (ReadFromBuffer(buffer, offset, count, out result))
            {
                return result;
            }
            return stream.Read(buffer, offset, count);
        }

        /// <summary>Sets the position within the current stream.</summary>
        /// <returns>The new position within the current stream.</returns>
        /// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter. </param>
        /// <param name="origin">
        ///     A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to
        ///     obtain the new position.
        /// </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The stream does not support seeking, such as if the stream is
        ///     constructed from a pipe or console output.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return stream.Seek(offset, origin);
        }

        /// <summary>Sets the length of the underlying stream.</summary>
        /// <param name="value">The desired length of the underlying stream in bytes. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The stream does not support both writing and seeking, such as if the
        ///     stream is constructed from a pipe or console output.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public override void SetLength(long value)
        {
            stream.SetLength(value);
        }

        /// <summary>
        ///     Pushes back a sequence of bytes to the current stream and moves back the current position within this stream
        ///     by the number of bytes written.
        /// </summary>
        /// <param name="buffer">
        ///     An array of bytes. This method copies <paramref name="count" /> bytes from
        ///     <paramref name="buffer" /> to the current stream.
        /// </param>
        /// <param name="offset">
        ///     The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the
        ///     current stream.
        /// </param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Precondition: offset >= 0");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: count >= 0");
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: offset + count <= buffer.Length");
            }
            if (count == 0)
            {
                return;
            }
            if (CanSeek)
            {
                throw new IOException(
                    $@"Pushing back bytes to a seekable stream is an invalid operation. Use 'Seek' instead.

if (stream.CanSeek)
{{
    stream.Seek(-{count}, SeekOrigin.Current)
}}");
            }
            for (var i = buffer.Length - 1; i >= offset && count != 0; i--, count--)
            {
                pushback.Push(buffer[i]);
            }
        }

        private bool ReadFromBuffer(byte[] buffer, int offset, int count, out int result)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Precondition: offset >= 0");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: count >= 0");
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: offset + count <= buffer.Length");
            }
            if (count == 0)
            {
                result = 0;
                return true;
            }
            if (pushback.Count == 0)
            {
                result = default(int);
                return false;
            }
            for (result = 0; result < count; result++, offset++)
            {
                if (pushback.Count == 0)
                {
                    break;
                }
                buffer[offset] = pushback.Pop();
            }
            return true;
        }
    }
}
