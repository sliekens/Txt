using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace Text.Scanning
{
    public sealed class TextScanner : ITextScanner
    {
        private int offset = -1;

        private readonly TextReader textReader;

        private bool endOfInput;

        private char nextCharacter;

        private readonly Stack<char> buffer = new Stack<char>();

        public TextScanner(TextReader textReader)
        {
            Contract.Requires(textReader != null);
            this.textReader = textReader;
        }

        /// <inheritdoc />
        public bool EndOfInput
        {
            get
            {
                return this.endOfInput;
            }
        }

        /// <inheritdoc />
        public char? NextCharacter
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
        public int Offset
        {
            get
            {
                return this.offset;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ((IDisposable)this.textReader).Dispose();
        }

        /// <inheritdoc />
        public ITextContext GetContext()
        {
            return new TextContext(offset);
        }

        /// <inheritdoc />
        public void PutBack(char c)
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
        public bool TryMatch(char c)
        {
            if (this.EndOfInput)
            {
                return false;
            }

            if (this.nextCharacter != c)
            {
                return false;
            }

            this.Read();

            return true;
        }

        /// <inheritdoc />
        public bool Read()
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
                lock (textReader)
                {
                    this.nextCharacter = (char)textReader.Read();
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
    }
}