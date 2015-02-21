// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alternative{T1,T2,T3,T4}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents a choice of four alternative elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>Represents a choice of four alternative elements.</summary>
    /// <typeparam name="T1">The type of the first alternative element.</typeparam>
    /// <typeparam name="T2">The type of the second alternative element.</typeparam>
    /// <typeparam name="T3">The type of the third alternative element.</typeparam>
    /// <typeparam name="T4">The type of the fourth alternative element.</typeparam>
    public class Alternative<T1, T2, T3, T4> : Element
        where T1 : Element where T2 : Element where T3 : Element where T4 : Element
    {
        /// <summary>The alternative element.</summary>
        private readonly Element element;

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2,T3,T4}"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Alternative(T1 element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            this.element = element;
        }

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2,T3,T4}"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Alternative(T2 element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            this.element = element;
        }

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2,T3,T4}"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Alternative(T3 element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            this.element = element;
        }

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2,T3,T4}"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Alternative(T4 element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            this.element = element;
        }

        /// <summary>Gets the alternative element.</summary>
        public Element Element
        {
            get
            {
                return this.element;
            }
        }

        [ContractInvariantMethod]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element != null);
        }
    }
}