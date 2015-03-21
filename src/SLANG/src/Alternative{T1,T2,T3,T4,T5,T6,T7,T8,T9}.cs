// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alternative{T1,T2,T3,T4,T5,T6,T7,T8,T9}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents a choice of nine alternative elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>Represents a choice of nine alternative elements.</summary>
    /// <typeparam name="T1">The type of the first alternative element.</typeparam>
    /// <typeparam name="T2">The type of the second alternative element.</typeparam>
    /// <typeparam name="T3">The type of the third alternative element.</typeparam>
    /// <typeparam name="T4">The type of the fourth alternative element.</typeparam>
    /// <typeparam name="T5">The type of the fifth alternative element.</typeparam>
    /// <typeparam name="T6">The type of the sixth alternative element.</typeparam>
    /// <typeparam name="T7">The type of the seventh alternative element.</typeparam>
    /// <typeparam name="T8">The type of the eighth alternative element.</typeparam>
    /// <typeparam name="T9">The type of the ninth alternative element.</typeparam>
    public class Alternative<T1, T2, T3, T4, T5, T6, T7, T8, T9> : Element
        where T1 : Element
        where T2 : Element
        where T3 : Element
        where T4 : Element
        where T5 : Element
        where T6 : Element
        where T7 : Element
        where T8 : Element
        where T9 : Element
    {
        /// <summary>The alternative element.</summary>
        private readonly Element element;

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="alternative">A number that indicates which alternative was matched.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Alternative(Element element, int alternative, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
            switch (alternative)
            {
                case 1:
                    if (false == element is T1)
                    {
                        throw new ArgumentException("Precondition: element is T1", "element");
                    }

                    break;
                case 2:
                    if (false == element is T2)
                    {
                        throw new ArgumentException("Precondition: element is T2", "element");
                    }

                    break;
                case 3:
                    if (false == element is T3)
                    {
                        throw new ArgumentException("Precondition: element is T3", "element");
                    }

                    break;
                case 4:
                    if (false == element is T4)
                    {
                        throw new ArgumentException("Precondition: element is T4", "element");
                    }

                    break;
                case 5:
                    if (false == element is T5)
                    {
                        throw new ArgumentException("Precondition: element is T5", "element");
                    }

                    break;
                case 6:
                    if (false == element is T6)
                    {
                        throw new ArgumentException("Precondition: element is T6", "element");
                    }

                    break;
                case 7:
                    if (false == element is T7)
                    {
                        throw new ArgumentException("Precondition: element is T7", "element");
                    }

                    break;
                case 8:
                    if (false == element is T8)
                    {
                        throw new ArgumentException("Precondition: element is T8", "element");
                    }

                    break;
                case 9:
                    if (false == element is T9)
                    {
                        throw new ArgumentException("Precondition: element is T9", "element");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("alternative");
            }

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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
            Justification = "Reviewed. Suppression is OK here.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element != null);
        }
    }
}