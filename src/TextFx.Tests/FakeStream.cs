namespace TextFx.Tests
{
    using System;
    using System.IO;

    internal class FakeStream : Stream
    {
        public override bool CanRead
        {
            get
            {
                if (this.OnCanReadGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnCanReadGet();
            }
        }

        public override bool CanSeek
        {
            get
            {
                if (this.OnCanSeekGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnCanSeekGet();
            }
        }

        public override bool CanWrite
        {
            get
            {
                if (this.OnCanWriteGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnCanWriteGet();
            }
        }

        public override long Length
        {
            get
            {
                if (this.OnLengthGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnLengthGet();
            }
        }

        public Func<bool> OnCanReadGet { get; set; }

        public Func<bool> OnCanSeekGet { get; set; }

        public Func<bool> OnCanWriteGet { get; set; }

        public Action OnFlush { get; set; }

        public Func<long> OnLengthGet { get; set; }

        public Func<long> OnPositionGet { get; set; }

        public Action<long> OnPositionSet { get; set; }

        public Func<byte[], int, int, int> OnRead { get; set; }

        public Func<long, SeekOrigin, long> OnSeek { get; set; }

        public Action<long> OnSetLength { get; set; }

        public Action<byte[], int, int> OnWrite { get; set; }

        public override long Position
        {
            get
            {
                if (this.OnPositionGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnPositionGet();
            }
            set
            {
                if (this.OnPositionSet == null)
                {
                    throw new NotImplementedException();
                }

                this.OnPositionSet(value);
            }
        }

        public override void Flush()
        {
            if (this.OnFlush == null)
            {
                throw new NotImplementedException();
            }

            this.OnFlush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.OnRead == null)
            {
                throw new NotImplementedException();
            }

            return this.OnRead(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (this.OnSeek == null)
            {
                throw new NotImplementedException();
            }

            return this.OnSeek(offset, origin);
        }

        public override void SetLength(long value)
        {
            if (this.OnSetLength == null)
            {
                throw new NotImplementedException();
            }

            this.OnSetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.OnWrite == null)
            {
                throw new NotImplementedException();
            }

            this.OnWrite(buffer, offset, count);
        }
    }
}