using System;
using System.Diagnostics.Contracts;

namespace Text.Scanning
{
    [ContractClassFor(typeof(ITextScanner))]
    internal abstract class ContractClassForITextScanner : ITextScanner
    {
        public char NextCharacter
        {
            get { throw new NotImplementedException(); }
        }

        public bool EndOfInput
        {
            get { throw new NotImplementedException(); }
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool TryMatch(char c)
        {
            throw new NotImplementedException();
        }

        public ITextContext GetContext()
        {
            Contract.Ensures(Contract.Result<ITextContext>() != null);
            throw new NotImplementedException();
        }

        public abstract void Dispose();
        public abstract int Offset { get; }
    }
}
