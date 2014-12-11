using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace Text
{
    public sealed class TextScanner : ITextScanner
    {
        private const char Cr = '\r';
        private const char Lf = '\n';
        private int offset = -1;
        private readonly EndOfLine endOfLine;
        private readonly TextReader textReader;
        private bool endOfInput = false;
        private int line;
        private char nextCharacter;

        public TextScanner(TextReader textReader, EndOfLine endOfLine = EndOfLine.CrLf)
        {
            Contract.Requires(textReader != null);
            Contract.Requires(Enum.IsDefined(typeof(EndOfLine), endOfLine));
            this.textReader = textReader;
            this.endOfLine = endOfLine;
        }

        public int Column { get; private set; }

        public bool EndOfInput
        {
            get
            {
                return endOfInput;
            }
        }

        public int Line
        {
            get { return line; }
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
            return new TextContext(Column, Line, offset);
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
                if (character == Cr)
                {
                    Column = 0;
                    if (endOfLine == EndOfLine.Cr)
                    {
                        this.line++;
                    }
                }
                else if (character == Lf)
                {
                    this.line++;
                    if (endOfLine == EndOfLine.Lf)
                    {
                        Column = 0;
                    }
                }
                else
                {
                    Column++;
                }

                this.nextCharacter = (char)character;
                return true;
            }
        }
    }
}