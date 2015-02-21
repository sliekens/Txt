// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContractClassForILexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The contract class for i lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>The contract class for i lexer.</summary>
    /// <typeparam name="TToken"></typeparam>
    [ContractClassFor(typeof(ILexer<>))]
    internal abstract class ContractClassForILexer<TToken> : ILexer<TToken>
        where TToken : Token
    {
        /// <summary>The put back.</summary>
        /// <param name="scanner">The scanner.</param>
        /// <param name="token">The token.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void PutBack(ITextScanner scanner, TToken token)
        {
            Contract.Requires(scanner != null);
            Contract.Requires(token != null);
            throw new NotImplementedException();
        }

        /// <summary>The read.</summary>
        /// <param name="scanner">The scanner.</param>
        /// <returns>The <see cref="TToken"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public TToken Read(ITextScanner scanner)
        {
            Contract.Requires(scanner != null);
            Contract.Ensures(Contract.Result<TToken>() != null);
            throw new NotImplementedException();
        }

        /// <summary>The try read.</summary>
        /// <param name="scanner">The scanner.</param>
        /// <param name="token">The token.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool TryRead(ITextScanner scanner, out TToken token)
        {
            Contract.Requires(scanner != null);
            Contract.Ensures(
                Contract.Result<bool>() && (Contract.ValueAtReturn(out token) != null)
                || Contract.Result<bool>() == false);
            throw new NotImplementedException();
        }
    }
}