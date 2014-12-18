namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(ILexer<>))]
    internal abstract class ContractClassForILexer<TToken> : ILexer<TToken>
        where TToken : Token
    {
        public void PutBack(TToken token)
        {
            Contract.Requires(token != null);
            throw new NotImplementedException();
        }

        public TToken Read()
        {
            Contract.Ensures(Contract.Result<TToken>() != null);
            throw new NotImplementedException();
        }

        public bool TryRead(out TToken token)
        {
            Contract.Ensures(Contract.Result<bool>() && (Contract.ValueAtReturn(out token) != null) ||
                             Contract.Result<bool>() == false);
            throw new NotImplementedException();
        }
    }
}