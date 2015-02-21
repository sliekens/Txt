namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(ITextScanner))]
    internal abstract class ContractClassForITextScanner : ITextScanner
    {
        public bool EndOfInput
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public char? NextCharacter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public abstract int Offset { get; }
        public abstract void Dispose();

        public ITextContext GetContext()
        {
            Contract.Ensures(Contract.Result<ITextContext>() != null);
            throw new NotImplementedException();
        }

        public void PutBack(char c)
        {
            //Contract.Ensures(Contract.OldValue(this.Offset) == this.Offset + 1 || this.Offset == 0);
            throw new NotImplementedException();
        }

        public bool Read()
        {
            //Contract.Ensures(Contract.OldValue(this.Offset) == this.Offset - 1 || this.EndOfInput);
            throw new NotImplementedException();
        }

        public bool TryMatch(char c)
        {
            //Contract.Ensures(c != this.NextCharacter || Contract.OldValue(this.Offset) == this.Offset - 1 || this.EndOfInput);
            throw new NotImplementedException();
        }
    }
}