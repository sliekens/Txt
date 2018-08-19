using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class TextSourceReader : TextReader, ITextSource
    {
        private readonly ITextSource textSource;

        private bool disposed;

        public TextSourceReader([NotNull] ITextSource textSource)
        {
            this.textSource = textSource ?? throw new ArgumentNullException(nameof(textSource));
        }

        public int Column => textSource.Column;

        public Encoding Encoding => textSource.Encoding;

        public int Line => textSource.Line;

        public long Offset => textSource.Offset;

        public override int Peek()
        {
            return textSource.Peek();
        }

        public override int Read()
        {
            return textSource.Read();
        }

        public override int Read(char[] buffer, int startIndex, int maxCount)
        {
            return textSource.Read(buffer, startIndex, maxCount);
        }

        public override Task<int> ReadAsync(char[] buffer, int startIndex, int maxCount)
        {
            return textSource.ReadAsync(buffer, startIndex, maxCount);
        }

        public override int ReadBlock(char[] buffer, int startIndex, int maxCount)
        {
            return textSource.ReadBlock(buffer, startIndex, maxCount);
        }

        public override Task<int> ReadBlockAsync(char[] buffer, int startIndex, int maxCount)
        {
            return textSource.ReadBlockAsync(buffer, startIndex, maxCount);
        }

        public void Seek(long offset)
        {
            textSource.Seek(offset);
        }

        public void StartRecording()
        {
            textSource.StartRecording();
        }

        public void StopRecording()
        {
            textSource.StopRecording();
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
            base.Dispose(disposing);
            disposed = true;
        }
    }
}
