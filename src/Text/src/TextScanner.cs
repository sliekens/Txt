using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace Text
{
    public sealed class TextScanner : ITextScanner
    {
        private int offset = -1;
        private readonly TextReader textReader;
        private bool endOfInput;
        private int line;
        private char nextCharacter;

        public TextScanner(TextReader textReader)
        {
            Contract.Requires(textReader != null);
            this.textReader = textReader;
        }

        public bool EndOfInput
        {
            get
            {
                return endOfInput;
            }
        }

        public char NextCharacter
        {
            get { return nextCharacter; }
        }

        public int Offset
        {
            get { return offset; }
        }

        public void Dispose()
        {
            ((IDisposable)textReader).Dispose();
        }

        public ITextContext GetContext()
        {
            return new TextContext(offset);
        }

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

        public bool Read()
        {
            lock (textReader)
            {
                var character = textReader.Read();
                if (character == -1)
                {
                    this.endOfInput = true;
                    return false;
                }

                offset++;
                this.nextCharacter = (char)character;
                return true;
            }
        }
    }
}