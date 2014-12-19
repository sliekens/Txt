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
            Contract.Ensures(this.Offset == Contract.OldValue(this.Offset) - 1);
            Contract.EnsuresOnThrow<ObjectDisposedException>(this.Offset == Contract.OldValue(this.Offset));
            throw new NotImplementedException();
        }

        public bool Read()
        {
            Contract.Ensures(Contract.Result<bool>() == false ||
                             Contract.Result<bool>() && this.Offset == Contract.OldValue(this.Offset) + 1);
            Contract.EnsuresOnThrow<ObjectDisposedException>(this.Offset == Contract.OldValue(this.Offset));
            throw new NotImplementedException();
        }

        public bool TryMatch(char c)
        {
            Contract.Ensures(Contract.Result<bool>() == false ||
                             Contract.Result<bool>() && this.Offset == Contract.OldValue(this.Offset) + 1);
            Contract.EnsuresOnThrow<ObjectDisposedException>(this.Offset == Contract.OldValue(this.Offset));
            throw new NotImplementedException();
        }
    }
}