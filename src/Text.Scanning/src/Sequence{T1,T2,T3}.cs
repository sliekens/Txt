// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sequence{T1,T2,T3}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents a sequence of three elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>Represents a sequence of three elements.</summary>
    /// <typeparam name="T1">The type of the first element in the sequence.</typeparam>
    /// <typeparam name="T2">The type of the second element in the sequence.</typeparam>
    /// <typeparam name="T3">The type of the third element in the sequence.</typeparam>
    public class Sequence<T1, T2, T3> : Element
        where T1 : Element
        where T2 : Element
        where T3 : Element
    {
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T1 element1;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T2 element2;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T3 element3;

        /// <summary>Initializes a new instance of the <see cref="Sequence{T1,T2,T3}"/> class with a specified sequence of elements.</summary>
        /// <param name="element1">The first element in the sequence.</param>
        /// <param name="element2">The second element in the sequence.</param>
        /// <param name="element3">The third element in the sequence.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Sequence(T1 element1, T2 element2, T3 element3, ITextContext context)
            : base(string.Concat(element1.Data, element2.Data, element3.Data), context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(element3 != null);
            Contract.Requires(context != null);
            this.element1 = element1;
            this.element2 = element2;
            this.element3 = element3;
        }

        /// <summary>Gets the first element in the sequence.</summary>
        public T1 Element1
        {
            get
            {
                return this.element1;
            }
        }

        /// <summary>Gets the second element in the sequence.</summary>
        public T2 Element2
        {
            get
            {
                return this.element2;
            }
        }

        /// <summary>Gets the third element in the sequence.</summary>
        public T3 Element3
        {
            get
            {
                return this.element3;
            }
        }

        [ContractInvariantMethod]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element1 != null);
            Contract.Invariant(this.element2 != null);
            Contract.Invariant(this.element3 != null);
        }
    }
}