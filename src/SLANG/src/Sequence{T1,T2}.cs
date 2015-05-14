// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sequence{T1,T2}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents a sequence of two elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>Represents a sequence of two elements.</summary>
    /// <typeparam name="T1">The type of the first element in the sequence.</typeparam>
    /// <typeparam name="T2">The type of the second element in the sequence.</typeparam>
    public class Sequence<T1, T2> : Element
        where T1 : Element
        where T2 : Element
    {
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T1 element1;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly T2 element2;

        /// <summary>Initializes a new instance of the <see cref="Sequence{T1,T2}"/> class with a specified sequence of elements.</summary>
        /// <param name="element1">The first element in the sequence.</param>
        /// <param name="element2">The second element in the sequence.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Sequence(T1 element1, T2 element2, ITextContext context)
            : base(string.Concat(element1, element2), context)
        {
            if (element1 == null)
            {
                throw new ArgumentNullException("element1", "Precondition: element1 != null");
            }

            if (element2 == null)
            {
                throw new ArgumentNullException("element2", "Precondition: element2 != null");
            }

            this.element1 = element1;
            this.element2 = element2;
        }

        /// <summary>Gets the first element in the sequence.</summary>
        public T1 Element1
        {
            get
            {
                Debug.Assert(this.element1 != null, "this.element1 != null");
                return this.element1;
            }
        }

        /// <summary>Gets the second element in the sequence.</summary>
        public T2 Element2
        {
            get
            {
                Debug.Assert(this.element2 != null, "this.element2 != null");
                return this.element2;
            }
        }
    }
}