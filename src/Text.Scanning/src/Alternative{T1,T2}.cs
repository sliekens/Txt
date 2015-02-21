// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alternative{T1,T2}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents a choice of two alternative elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>Represents a choice of two alternative elements.</summary>
    /// <typeparam name="T1">The type of the first alternative element.</typeparam>
    /// <typeparam name="T2">The type of the second alternative element.</typeparam>
    public class Alternative<T1, T2>
        where T1 : Element where T2 : Element
    {
        /// <summary>The first alternative element, or a default value.</summary>
        private readonly T1 element1;

        /// <summary>The second alternative element, or a default value.</summary>
        private readonly T2 element2;

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2}"/> class.</summary>
        /// <param name="element">The alternative element.</param>
        public Alternative(T1 element)
        {
            Contract.Requires(element != null);
            this.element1 = element;
        }

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2}"/> class.</summary>
        /// <param name="element">The alternative element.</param>
        public Alternative(T2 element)
        {
            Contract.Requires(element != null);
            this.element2 = element;
        }

        /// <summary>Gets the alternative element.</summary>
        public Element Element
        {
            get
            {
                if (this.element1 != default(T1))
                {
                    return this.element1;
                }

                return this.element2;
            }
        }

        [ContractInvariantMethod]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element1 == null || this.element2 == null);
        }
    }
}