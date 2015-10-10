namespace TextFx
{
    using System;
    using System.IO;
    using System.Text;

    public class StreamTextSource : ITextSource
    {
        private readonly BinaryReader binaryReader;

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
            this.Encoding = encoding;
            this.binaryReader = new BinaryReader(inputStream, encoding, true);
        }

        public Encoding Encoding { get; }

        public void Dispose()
        {
            this.binaryReader.Dispose();
        }

        public int Read()
        {
            return this.binaryReader.Read();
        }

        public int Read(char[] buffer, int index, int count)
        {
            return this.binaryReader.Read(buffer, index, count);
        }

        public void Unread(char c)
        {
            this.Unread(new[] { c }, 0, 1);
        }

        public void Unread(char[] buffer, int index, int count)
        {
            if (this.inputStream.CanSeek)
            {
                var length = this.Encoding.GetByteCount(buffer, index, count);
                this.inputStream.Seek(-length, SeekOrigin.Current);
            }
            else
            {
                var pushbackBuffer = this.Encoding.GetBytes(buffer, index, count);
                this.inputStream.Write(pushbackBuffer, 0, pushbackBuffer.Length);
            }
        }
    }
}