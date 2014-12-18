namespace Text.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;

    public sealed class TextScanner : ITextScanner
    {
        private bool endOfInput;
        private char nextCharacter;
        private int offset = -1;
        private readonly Stack<char> buffer = new Stack<char>();
        private readonly TextReader textReader;

        public TextScanner(TextReader textReader)
        {
            Contract.Requires(textReader != null);
            this.textReader = textReader;
        }

        /// <inheritdoc />
        bool ITextScanner.EndOfInput
        {
            get
            {
                return this.endOfInput;
            }
        }

        /// <inheritdoc />
        char? ITextScanner.NextCharacter
        {
            get
            {
                if (this.endOfInput)
                {
                    return null;
                }

                return this.nextCharacter;
            }
        }

        /// <inheritdoc />
        int ITextContext.Offset
        {
            get
            {
                return this.offset;
            }
        }

        /// <inheritdoc />
        void IDisposable.Dispose()
        {
            ((IDisposable)this.textReader).Dispose();
        }

        /// <inheritdoc />
        ITextContext ITextScanner.GetContext()
        {
            return new TextContext(this.offset);
        }

        /// <inheritdoc />
        void ITextScanner.PutBack(char c)
        {
            if (this.endOfInput)
            {
                this.endOfInput = false;
            }
            else
            {
                this.buffer.Push(this.nextCharacter);
            }

            this.offset--;
            this.nextCharacter = c;
        }

        /// <inheritdoc />
        bool ITextScanner.Read()
        {
            if (this.endOfInput)
            {
                return false;
            }

            if (this.buffer.Any())
            {
                this.nextCharacter = this.buffer.Pop();
                this.offset++;
            }
            else
            {
                lock (this.textReader)
                {
                    this.nextCharacter = (char)this.textReader.Read();
                    this.offset++;
                }
            }

            if (this.nextCharacter == char.MaxValue)
            {
                this.endOfInput = true;
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        bool ITextScanner.TryMatch(char c)
        {
            if (this.endOfInput)
            {
                return false;
            }

            if (this.nextCharacter != c)
            {
                return false;
            }

            ITextScanner self = this;
            self.Read();

            return true;
        }
    }
}