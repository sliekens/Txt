// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sequence{T1,T2,T3,T4,T5,T6,T7,T8}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents a sequence of eight elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>Represents a sequence of eight elements.</summary>
    /// <typeparam name="T1">The type of the first element in the sequence.</typeparam>
    /// <typeparam name="T2">The type of the second element in the sequence.</typeparam>
    /// <typeparam name="T3">The type of the third element in the sequence.</typeparam>
    /// <typeparam name="T4">The type of the fourth element in the sequence.</typeparam>
    /// <typeparam name="T5">The type of the fifth element in the sequence.</typeparam>
    /// <typeparam name="T6">The type of the sixth element in the sequence.</typeparam>
    /// <typeparam name="T7">The type of the seventh element in the sequence.</typeparam>
    /// <typeparam name="T8">The type of the eighth element in the sequence.</typeparam>
    public class Sequence<T1, T2, T3, T4, T5, T6, T7, T8> : Element
        where T1 : Element
        where T2 : Element
        where T3 : Element
        where T4 : Element
        where T5 : Element
        where T6 : Element
        where T7 : Element
        where T8 : Element
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

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T4 element4;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T5 element5;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T6 element6;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T7 element7;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T8 element8;

        /// <summary>Initializes a new instance of the <see cref="Sequence{T1,T2,T3,T4,T5,T6,T7,T8}"/> class with a specified sequence of elements.</summary>
        /// <param name="element1">The first element in the sequence.</param>
        /// <param name="element2">The second element in the sequence.</param>
        /// <param name="element3">The third element in the sequence.</param>
        /// <param name="element4">The fourth element in the sequence.</param>
        /// <param name="element5">The fifth element in the sequence.</param>
        /// <param name="element6">The sixth element in the sequence.</param>
        /// <param name="element7">The seventh element in the sequence.</param>
        /// <param name="element8">The eighth element in the sequence.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Sequence(
            T1 element1, 
            T2 element2, 
            T3 element3, 
            T4 element4, 
            T5 element5, 
            T6 element6, 
            T7 element7, 
            T8 element8, 
            ITextContext context)
            : base(
                string.Concat(
                    element1.Data, 
                    element2.Data, 
                    element3.Data, 
                    element4.Data, 
                    element5.Data, 
                    element6.Data, 
                    element7.Data), 
                context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(element3 != null);
            Contract.Requires(element4 != null);
            Contract.Requires(element5 != null);
            Contract.Requires(element6 != null);
            Contract.Requires(element7 != null);
            Contract.Requires(element8 != null);
            Contract.Requires(context != null);
            this.element1 = element1;
            this.element2 = element2;
            this.element3 = element3;
            this.element4 = element4;
            this.element5 = element5;
            this.element6 = element6;
            this.element7 = element7;
            this.element8 = element8;
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

        /// <summary>Gets the fourth element in the sequence.</summary>
        public T4 Element4
        {
            get
            {
                return this.element4;
            }
        }

        /// <summary>Gets the fifth element in the sequence.</summary>
        public T5 Element5
        {
            get
            {
                return this.element5;
            }
        }

        /// <summary>Gets the sixth element in the sequence.</summary>
        public T6 Element6
        {
            get
            {
                return this.element6;
            }
        }

        /// <summary>Gets the seventh element in the sequence.</summary>
        public T7 Element7
        {
            get
            {
                return this.element7;
            }
        }

        /// <summary>Gets the eighth element in the sequence.</summary>
        public T8 Element8
        {
            get
            {
                return this.element8;
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
            Contract.Invariant(this.element4 != null);
            Contract.Invariant(this.element5 != null);
            Contract.Invariant(this.element6 != null);
            Contract.Invariant(this.element7 != null);
            Contract.Invariant(this.element8 != null);
        }
    }
}