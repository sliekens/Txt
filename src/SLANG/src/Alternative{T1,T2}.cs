// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alternative{T1,T2}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents a choice of two alternative elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>Represents a choice of two alternative elements.</summary>
    /// <typeparam name="T1">The type of the first alternative element.</typeparam>
    /// <typeparam name="T2">The type of the second alternative element.</typeparam>
    public class Alternative<T1, T2> : Element
        where T1 : Element
        where T2 : Element
    {
        /// <summary>The matched alternative element.</summary>
        private readonly Element element;

        /// <summary>The ordinal position of the matched alternative.</summary>
        private readonly int ordinal;

        /// <summary>Initializes a new instance of the <see cref="Alternative{T1,T2}"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="alternative">A number that indicates which alternative was matched.</param>
        public Alternative(Element element, int alternative)
            : base(element)
        {
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
                default:
                    throw new ArgumentOutOfRangeException("alternative", alternative, "Precondition: 1 <= alternative <= 2");
            }

            this.element = element;
            this.ordinal = alternative;
        }

        /// <summary>Gets the matched alternative element.</summary>
        public Element Element
        {
            get
            {
                Debug.Assert(this.element != null, "this.element != null");
                return this.element;
            }
        }

        /// <summary>Gets the ordinal position of the matched alternative.</summary>
        public int Ordinal
        {
            get
            {
                return this.ordinal;
            }
        }
    }
}