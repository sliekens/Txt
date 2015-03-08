// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryTextReader.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>TODO </summary>
    public class BinaryTextReader : TextReader
    {
        /// <summary>TODO </summary>
        public const int DefaultBufferSize = 4096;

        /// <summary>TODO </summary>
        private readonly BinaryReader binaryReader;

        /// <summary>TODO </summary>
        private readonly int bufferSize;

        /// <summary>TODO </summary>
        private bool disposed;

        /// <summary>Initializes a new instance of the <see cref="BinaryTextReader"/> class that reads from a given instance of the <see cref="BinaryReader"/> class.</summary>
        /// <param name="binaryReader">The binary reader to read from.</param>
        public BinaryTextReader(BinaryReader binaryReader)
            : this(binaryReader, DefaultBufferSize)
        {
            Contract.Requires(binaryReader != null);
        }

        /// <summary>Initializes a new instance of the <see cref="BinaryTextReader"/> class that reads from a given instance of the <see cref="BinaryReader"/> class, using a specified buffer size.</summary>
        /// <param name="binaryReader">The binary reader to read from.</param>
        /// <param name="bufferSize">The size of the internal buffer.</param>
        public BinaryTextReader(BinaryReader binaryReader, int bufferSize)
        {
            Contract.Requires(binaryReader != null);
            Contract.Requires(bufferSize > 0);
            this.binaryReader = binaryReader;
            this.bufferSize = bufferSize;
        }

        /// <inheritdoc />
        public override int Peek()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return this.binaryReader.PeekChar();
        }

        /// <inheritdoc />
        public override int Read()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return this.binaryReader.Read();
        }

        /// <inheritdoc />
        public override int Read(char[] buffer, int index, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return this.binaryReader.Read(buffer, index, count);
        }

        /// <inheritdoc />
        public override Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override int ReadBlock(char[] buffer, int index, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            int read;
            int totalRead = 0;
            do
            {
                read = this.Read(buffer, index, count);
                if (read != 0)
                {
                    index += read;
                    count -= read;
                    totalRead += read;
                }
            }
            while (count > 0 && read > 0);

            return totalRead;
        }

        /// <inheritdoc />
        public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override string ReadLine()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Task<string> ReadLineAsync()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override string ReadToEnd()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            using (var textWriter = new StringWriter())
            {
                var buffer = new char[this.bufferSize];
                int read;
                do
                {
                    read = this.Read(buffer, 0, buffer.Length);
                    Contract.Assume(buffer.Length >= read);
                    if (read > 0)
                    {
                        textWriter.Write(buffer, 0, read);
                    }
                }
                while (read > 0);

                return textWriter.ToString();
            }
        }

        /// <inheritdoc />
        public override Task<string> ReadToEndAsync()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.binaryReader.Dispose();
            }

            this.disposed = true;
        }

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.binaryReader != null);
            Contract.Invariant(this.bufferSize > 0);
        }
    }
}