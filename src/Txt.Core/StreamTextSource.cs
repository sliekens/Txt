using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Txt.Core
{
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                binaryReader.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override int PeekImpl()
        {
            return binaryReader.PeekChar();
        }

        protected override int ReadImpl()
        {
            return binaryReader.Read();
        }

        protected override int ReadImpl(char[] buffer, int offset, int count)
        {
            return binaryReader.Read(buffer, offset, count);
        }

        protected override void UnreadImpl(char c)
        {
            Unread(new[] { c }, 0, 1);
        }

        protected override void UnreadImpl(char[] buffer, int offset, int count)
        {
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
    }
}
