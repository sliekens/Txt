namespace TextFx
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public class StreamTextSource : TextSource
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly BinaryReader binaryReader;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly PushbackInputStream inputStream;

        public StreamTextSource(PushbackInputStream inputStream, Encoding encoding)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            this.inputStream = inputStream;
            Encoding = encoding;
            binaryReader = new BinaryReader(inputStream, encoding, true);
        }

        public Encoding Encoding { get; }

        public override int Read()
        {
            return binaryReader.Read();
        }

        public override int Read(char[] buffer, int offset, int count)
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
                return 0;
            }
            return binaryReader.Read(buffer, offset, count);
        }

        public override void Unread(char[] buffer, int offset, int count)
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
            if (inputStream.CanSeek)
            {
                var length = Encoding.GetByteCount(buffer, offset, count);
                inputStream.Seek(-length, SeekOrigin.Current);
            }
            else
            {
                var pushbackBuffer = Encoding.GetBytes(buffer, offset, count);
                inputStream.Write(pushbackBuffer, 0, pushbackBuffer.Length);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                binaryReader.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
