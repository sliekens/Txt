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
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(ILexer<>))]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    internal abstract class ContractClassForILexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        public string RuleName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void PutBack(ITextScanner scanner, TElement element)
        {
            Contract.Requires(scanner != null);
            Contract.Requires(element != null);
            throw new NotImplementedException();
        }

        public TElement Read(ITextScanner scanner)
        {
            Contract.Requires(scanner != null);
            Contract.Ensures(Contract.Result<TElement>() != null);
            throw new NotImplementedException();
        }

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