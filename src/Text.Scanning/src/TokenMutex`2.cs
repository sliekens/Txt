// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenMutex`2.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The token mutex.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics.Contracts;

    /// <summary>The token mutex.</summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class TokenMutex<T1, T2>
        where T1 : Token where T2 : Token
    {
        /// <summary>The token 1.</summary>
        private readonly T1 token1;

        /// <summary>The token 2.</summary>
        private readonly T2 token2;

        /// <summary>Initializes a new instance of the <see cref="TokenMutex{T1,T2}"/> class.</summary>
        /// <param name="token">The token.</param>
        public TokenMutex(T1 token)
        {
            Contract.Requires(token != null);
            this.token1 = token;
        }

        /// <summary>Initializes a new instance of the <see cref="TokenMutex{T1,T2}"/> class.</summary>
        /// <param name="token">The token.</param>
        public TokenMutex(T2 token)
        {
            Contract.Requires(token != null);
            this.token2 = token;
        }

        /// <summary>Gets the token.</summary>
        public Token Token
        {
            get
            {
                return this.token1 as Token ?? this.token2;
            }
        }

        /// <summary>The object invariant.</summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.token1 == null || this.token2 == null);
        }
    }
}