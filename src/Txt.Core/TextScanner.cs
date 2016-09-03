using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class TextScanner : ITextScanner
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITextSource textSource;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private bool disposed;

        public TextScanner([NotNull] ITextSource textSource)
        {
            if (textSource == null)
            {
                throw new ArgumentNullException(nameof(textSource));
            }
            this.textSource = textSource;
        }

        public int Column => textSource.Column;

        public Encoding Encoding => textSource.Encoding;

        public int Line => textSource.Line;

        public long Offset => textSource.Offset;

        /// <summary>This method calls <see cref="Dispose(bool)" />, specifying <c>true</c> to release all resources.</summary>
        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ITextContext GetContext()
        {
            return new Bookmark(Offset, Line, Column);
        }

        public int Peek()
        {
            return textSource.Peek();
        }

        public int Read()
        {
            return textSource.Read();
        }

        public int Read(char[] buffer, int startIndex, int maxCount)
        {
            return textSource.Read(buffer, startIndex, maxCount);
        }

        public Task<int> ReadAsync(char[] buffer, int startIndex, int maxCount)
        {
            return textSource.ReadAsync(buffer, startIndex, maxCount);
        }

        public int ReadBlock(char[] buffer, int startIndex, int maxCount)
        {
            return textSource.ReadBlock(buffer, startIndex, maxCount);
        }

        public Task<int> ReadBlockAsync(char[] buffer, int startIndex, int maxCount)
        {
            return textSource.ReadBlockAsync(buffer, startIndex, maxCount);
        }

        public void Seek(long offset)
        {
            textSource.Seek(offset);
        }

        public long StartRecording()
        {
            return textSource.StartRecording();
        }

        public void StopRecording()
        {
            textSource.StopRecording();
        }

        /// <summary>
        ///     Advances the scanner's position if the next available character matches the conditions defined by the specified
        ///     predicate.
        /// </summary>
        /// <param name="predicate">The <see cref="Predicate{T}" /> that defines the conditions of the character to match.</param>
        /// <returns>
        ///     A value container that contains the next available character and another value indicating whether it matches
        ///     the conditions defined by the predicate.
        /// </returns>
        public ScanResult TryMatch([NotNull] Predicate<char> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            var head = textSource.Peek();
            if (head == -1)
            {
                return new ScanResult(true, false, string.Empty, string.Empty);
            }
            var next = (char)head;
            var text = char.ToString(next);
            if (!predicate(next))
            {
                return new ScanResult(false, false, text, string.Empty);
            }
            textSource.Read();
            return new ScanResult(false, true, text, text);
        }

        public virtual ScanResult TryMatch(string s)
        {
            return TryMatch(s, StringComparer.Ordinal);
        }

        public ScanResult TryMatch(string s, IEqualityComparer<string> comparer)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentException($"Value cannot be empty. Test {nameof(Peek)} for -1 instead.", nameof(s));
            }
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            var offset = StartRecording();
            try
            {
                var buffer = new char[s.Length];
                var len = textSource.ReadBlock(buffer, 0, buffer.Length);
                if (len == 0)
                {
                    return ScanResult.FromEndOfInput(s);
                }
                var next = new string(buffer, 0, len);
                if (comparer.Equals(s, next))
                {
                    return ScanResult.FromMatch(next, s);
                }
                Seek(offset);
                return ScanResult.FromMismatch(next, s);
            }
            finally
            {
                StopRecording();
            }
        }

        /// <inheritdoc />
        public virtual ScanResult TryMatch(char c)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TextScanner));
            }
            var head = textSource.Peek();
            var expected = char.ToString(c);
            if (head == -1)
            {
                return ScanResult.FromEndOfInput(expected);
            }
            var next = (char)head;
            var text = char.ToString(next);
            if (c != next)
            {
                return ScanResult.FromMismatch(text, expected);
            }
            textSource.Read();
            return ScanResult.FromMatch(text, expected);
        }

        void IDisposable.Dispose()
        {
            Close();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">
        ///     <c>true</c> to clean up both managed and unmanaged resources; otherwise, <c>false</c> to clean up only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;
        }
    }
}
