using System;

namespace Txt.Core
{
    public class FakeLexer<T> : ILexer<T>
        where T : Element
    {
        public delegate bool FakeTryRead(ITextScanner scanner, out T element);

        public delegate bool FakeTryReadElement(ITextScanner scanner, out Element element);

        public Func<ITextScanner, T> OnRead { get; set; }

        public Func<ITextScanner, Element> OnReadElement { get; set; }

        public FakeTryRead OnTryRead { get; set; }

        public FakeTryReadElement OnTryReadElement { get; set; }

        public IReadResult<T> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}