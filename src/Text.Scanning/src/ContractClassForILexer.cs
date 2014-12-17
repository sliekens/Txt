using System;
using System.Diagnostics.Contracts;

namespace Text.Scanning
{
    [ContractClassFor(typeof(ILexer<>))]
    internal abstract class ContractClassForILexer<TToken> : ILexer<TToken> where TToken : Token
    {
        public TToken Read()
        {
            Contract.Ensures(Contract.Result<TToken>() != null);
            throw new NotImplementedException();
        }

        public bool TryRead(out TToken token)
        {
            Contract.Ensures(Contract.ValueAtReturn(out token) != null && Contract.Result<bool>() == true || Contract.Result<bool>() == false);
            throw new NotImplementedException();
        }

        public void PutBack(TToken token)
        {
            throw new NotImplementedException();
        }
    }
}
