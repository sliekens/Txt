// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repetition.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>Represents repeating elements.</summary>
    /// <typeparam name="T">The type of the repeating element.</typeparam>
    public class Repetition<T> : Element
        where T : Element
    {
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
            Justification = "Reviewed. Suppression is OK here.")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<T> elements;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
            Justification = "Reviewed. Suppression is OK here.")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int lowerBound;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int upperBound;

        /// <summary>Initializes a new instance of the <see cref="Repetition{T}"/> class with zero or more occurrences.</summary>
        /// <param name="elements">Every occurrence of the element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Repetition(IList<T> elements, ITextContext context)
            : base(string.Concat(elements), context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(Contract.ForAll(elements, element => element != null));
            Contract.Requires(context != null);
            this.lowerBound = 0;
            this.upperBound = int.MaxValue;
            this.elements = new ReadOnlyCollection<T>(elements);
        }

        /// <summary>Initializes a new instance of the <see cref="Repetition{T}"/> class with a number of occurrences greater or equal to a specified lower bound.</summary>
        /// <param name="elements">Every occurrence of the element.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Repetition(IList<T> elements, int lowerBound, ITextContext context)
            : base(string.Concat(elements), context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(Contract.ForAll(elements, element => element != null));
            Contract.Requires(elements.Count >= lowerBound);
            Contract.Requires(context != null);
            this.lowerBound = lowerBound;
            this.upperBound = int.MaxValue;
            this.elements = new ReadOnlyCollection<T>(elements);
        }

        /// <summary>Initializes a new instance of the <see cref="Repetition{T}"/> class with a number of occurrences between a specified lower and upper bound (both inclusive).</summary>
        /// <param name="elements">Every occurrence of the element.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences.</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Repetition(IList<T> elements, int lowerBound, int upperBound, ITextContext context)
            : base(string.Concat(elements), context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(Contract.ForAll(elements, element => element != null));
            Contract.Requires(elements.Count >= lowerBound || elements.Count <= upperBound);
            Contract.Requires(context != null);
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
            this.elements = new ReadOnlyCollection<T>(elements);
        }

        /// <summary>Gets all occurrences of the element.</summary>
        public IList<T> Elements
        {
            get
            {
                return this.elements;
            }
        }

        /// <summary>Gets the minimum number of occurrences.</summary>
        public int LowerBound
        {
            get
            {
                return this.lowerBound;
            }
        }

        /// <summary>Gets the maximum number of occurrences.</summary>
        public int UpperBound
        {
            get
            {
                return this.upperBound;
            }
        }

        [ContractInvariantMethod]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.elements != null);
        }
    }
}