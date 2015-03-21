// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the BIT rule: 0 or 1. Unicode: U+0030 / U+0031.</summary>
    public class Bit : Alternative<Element, Element>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.Bit"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The bit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Bit(Element element, int alternative, ITextContext context)
            : base(element, alternative, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}