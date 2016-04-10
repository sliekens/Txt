using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Text
{
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

        public override bool CanTimeout
        {
            get
            {
                if (this.OnCanTimeoutGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnCanTimeoutGet();
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

        public Func<bool> OnCanTimeoutGet { get; set; }

        public Func<bool> OnCanWriteGet { get; set; }

        public Func<Stream, int, CancellationToken, Task> OnCopyToAsyncStreamInt32CancellationToken { get; set; }

        public Action<bool> OnDisposeBool { get; set; }

        public Action OnFlush { get; set; }

        public Func<CancellationToken, Task> OnFlushAsyncCancellationToken { get; set; }

        public Func<long> OnLengthGet { get; set; }

        public Func<long> OnPositionGet { get; set; }

        public Action<long> OnPositionSet { get; set; }

        public Func<byte[], int, int, CancellationToken, Task<int>> OnReadAsyncByteArrayInt32Int32CancellationToken {
            get; set; }

        public Func<int> OnReadByte { get; set; }

        public Func<byte[], int, int, int> OnReadByteArrayInt32Int32 { get; set; }

        public Func<int> OnReadTimeoutGet { get; set; }

        public Action<int> OnReadTimeoutSet { get; set; }

        public Func<long, SeekOrigin, long> OnSeekInt64SeekOrigin { get; set; }

        public Action<long> OnSetLength { get; set; }

        public Func<byte[], int, int, CancellationToken, Task> OnWriteAsyncByteArrayInt32Int32CancellationToken { get;
            set; }

        public Action<byte[], int, int> OnWriteByteArrayInt32Int32 { get; set; }

        public Action<byte> OnWriteByteByte { get; set; }

        public Func<int> OnWriteTimeoutGet { get; set; }

        public Action<int> OnWriteTimeoutSet { get; set; }

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

        public override int ReadTimeout
        {
            get
            {
                if (this.OnReadTimeoutGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnReadTimeoutGet();
            }
            set
            {
                if (this.OnReadTimeoutSet == null)
                {
                    throw new NotImplementedException();
                }

                this.OnReadTimeoutSet(value);
            }
        }

        public override int WriteTimeout
        {
            get
            {
                if (this.OnWriteTimeoutGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnWriteTimeoutGet();
            }
            set
            {
                if (this.OnWriteTimeoutSet == null)
                {
                    throw new NotImplementedException();
                }

                this.OnWriteTimeoutSet(value);
            }
        }

        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            if (this.OnCopyToAsyncStreamInt32CancellationToken == null)
            {
                throw new NotImplementedException();
            }
            return this.OnCopyToAsyncStreamInt32CancellationToken(destination, bufferSize, cancellationToken);
        }

        public override void Flush()
        {
            if (this.OnFlush == null)
            {
                throw new NotImplementedException();
            }

            this.OnFlush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            if (this.OnFlushAsyncCancellationToken == null)
            {
                throw new NotImplementedException();
            }

            return this.OnFlushAsyncCancellationToken(cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.OnReadByteArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            return this.OnReadByteArrayInt32Int32(buffer, offset, count);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (this.OnReadAsyncByteArrayInt32Int32CancellationToken == null)
            {
                throw new NotImplementedException();
            }

            return this.OnReadAsyncByteArrayInt32Int32CancellationToken(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            if (this.OnReadByte == null)
            {
                throw new NotImplementedException();
            }

            return this.OnReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (this.OnSeekInt64SeekOrigin == null)
            {
                throw new NotImplementedException();
            }

            return this.OnSeekInt64SeekOrigin(offset, origin);
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
            if (this.OnWriteByteArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            this.OnWriteByteArrayInt32Int32(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (this.OnWriteAsyncByteArrayInt32Int32CancellationToken == null)
            {
                throw new NotImplementedException();
            }

            return this.OnWriteAsyncByteArrayInt32Int32CancellationToken(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            if (this.OnWriteByteByte == null)
            {
                throw new NotImplementedException();
            }

            this.OnWriteByteByte(value);
        }

        protected override void Dispose(bool disposing)
        {
            this.OnDisposeBool?.Invoke(disposing);
            base.Dispose(disposing);
        }
    }
}