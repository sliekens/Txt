// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContractClassForILexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    /// <typeparam name="TElement"></typeparam>
    [ContractClassFor(typeof(ILexer<>))]
    internal abstract class ContractClassForILexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        /// <summary>TODO </summary>
        /// <param name="scanner">TODO </param>
        /// <param name="element">TODO </param>
        /// <exception cref="NotImplementedException"></exception>
        public void PutBack(ITextScanner scanner, TElement element)
        {
            Contract.Requires(scanner != null);
            Contract.Requires(element != null);
            throw new NotImplementedException();
        }

        /// <summary>TODO </summary>
        /// <param name="scanner">TODO </param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TElement Read(ITextScanner scanner)
        {
            Contract.Requires(scanner != null);
            Contract.Ensures(Contract.Result<TElement>() != null);
            throw new NotImplementedException();
        }

        /// <summary>TODO </summary>
        /// <param name="scanner">TODO </param>
        /// <param name="element">TODO </param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool TryRead(ITextScanner scanner, out TElement element)
        {
            Contract.Requires(scanner != null);
            Contract.Ensures(
                Contract.Result<bool>() && (Contract.ValueAtReturn(out element) != null)
                || Contract.Result<bool>() == false);
            throw new NotImplementedException();
        }
    }
}