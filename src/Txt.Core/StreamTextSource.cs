using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class StreamTextSource : TextSource
    {
        private readonly BinaryReader reader;

        private bool disposed;

        public StreamTextSource([NotNull] Stream input, int capacity = 2048)
            : base(capacity)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            reader = new BinaryReader(input);
        }

        public StreamTextSource([NotNull] Stream input, [NotNull] Encoding encoding, int capacity = 2048)
            : base(capacity)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            reader = new BinaryReader(input, encoding);
        }

        public StreamTextSource(
            [NotNull] Stream input,
            [NotNull] Encoding encoding,
            bool leaveOpen,
            int capacity = 2048)
            : base(capacity)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            reader = new BinaryReader(input, encoding, leaveOpen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                reader.Dispose();
            }
            base.Dispose(disposing);
            disposed = true;
        }

        protected override int ReadImpl(char[] buffer, int startIndex, int maxCount)
        {
            return reader.Read(buffer, startIndex, maxCount);
        }
    }
}
