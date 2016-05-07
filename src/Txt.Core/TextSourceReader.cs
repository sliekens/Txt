using System;
using System.IO;
using System.Threading.Tasks;

namespace Txt.Core
{
    public class TextSourceReader : TextReader
    {
        private readonly ITextSource textSource;

        private bool disposed;

        public TextSourceReader(ITextSource textSource)
        {
            if (textSource == null)
            {
                throw new ArgumentNullException(nameof(textSource));
            }
            this.textSource = textSource;
        }

        public override int Peek()
        {
            return textSource.Peek();
        }

        public override int Read()
        {
            return textSource.Read();
        }

        public override int Read(char[] buffer, int index, int count)
        {
            return textSource.Read(buffer, index, count);
        }

        public override Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            return textSource.ReadAsync(buffer, index, count);
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            return textSource.ReadBlock(buffer, index, count);
        }

        public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            return textSource.ReadBlockAsync(buffer, index, count);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                textSource.Dispose();
            }
            disposed = true;
        }
    }
}
