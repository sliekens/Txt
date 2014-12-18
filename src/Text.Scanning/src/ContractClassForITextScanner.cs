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
                Contract.Ensures(Contract.Result<bool>() == false || this.NextCharacter == null);
                throw new NotImplementedException();
            }
        }

        public char? NextCharacter
        {
            get
            {
                Contract.Ensures(Contract.Result<char?>().HasValue || this.EndOfInput);
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
            throw new NotImplementedException();
        }

        public bool Read()
        {
            Contract.Ensures(Contract.Result<bool>() == false || this.Offset == Contract.OldValue(this.Offset) + 1);
            throw new NotImplementedException();
        }

        public bool TryMatch(char c)
        {
            Contract.Ensures(Contract.Result<bool>() == false || this.Offset == Contract.OldValue(this.Offset) + 1);
            throw new NotImplementedException();
        }
    }
}