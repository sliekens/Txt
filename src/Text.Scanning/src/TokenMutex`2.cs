using System.Diagnostics.Contracts;

namespace Text.Scanning
{
    public class TokenMutex<T1, T2>
        where T1 : Token
        where T2 : Token
    {
        private readonly T1 token1;
        private readonly T2 token2;

        public TokenMutex(T1 token)
        {
            Contract.Requires(token != null);
            token1 = token;
        }

        public TokenMutex(T2 token)
        {
            Contract.Requires(token != null);
            token2 = token;
        }

        public Token Token
        {
            get { return token1 as Token ?? token2; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(token1 == null || token2 == null);
        }
    }
}