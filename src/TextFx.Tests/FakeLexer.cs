namespace TextFx.Tests
{
    using System;

    public class FakeLexer<T> : ILexer<T>
        where T : Element
    {
        public delegate bool FakeTryRead(ITextScanner scanner, out T element);

        public delegate bool FakeTryReadElement(ITextScanner scanner, out Element element);

        public Func<ITextScanner, T> OnRead { get; set; }

        public Func<ITextScanner, Element> OnReadElement { get; set; }

        public FakeTryRead OnTryRead { get; set; }

        public FakeTryReadElement OnTryReadElement { get; set; }

        public T Read(ITextScanner scanner)
        {
            if (this.OnRead == null)
            {
                throw new NotImplementedException();
            }

            return this.OnRead(scanner);
        }

        public Element ReadElement(ITextScanner scanner)
        {
            if (this.OnReadElement == null)
            {
                throw new NotImplementedException();
            }

            return this.OnReadElement(scanner);
        }

        public bool TryRead(ITextScanner scanner, out T element)
        {
            if (this.OnTryRead == null)
            {
                throw new NotImplementedException();
            }

            return this.OnTryRead(scanner, out element);
        }

        public bool TryReadElement(ITextScanner scanner, out Element element)
        {
            if (this.OnTryReadElement == null)
            {
                throw new NotImplementedException();
            }

            return this.OnTryReadElement(scanner, out element);
        }
    }
}