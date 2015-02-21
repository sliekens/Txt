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
    using System.Diagnostics.Contracts;

    /// <summary>Represents a choice of two alternative elements.</summary>
    /// <typeparam name="T1">The type of the first alternative element.</typeparam>
    /// <typeparam name="T2">The type of the second alternative element.</typeparam>
    public class Alternative<T1, T2> : Element
        where T1 : Element where T2 : Element
    {
        /// <summary>The alternative element.</summary>
        private readonly Element element;

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2}"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Alternative(T1 element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            this.element = element;
        }

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2}"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Alternative(T2 element, ITextContext context)
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
    }
}