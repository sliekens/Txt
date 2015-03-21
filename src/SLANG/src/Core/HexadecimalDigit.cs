// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Represents the HEXDIG rule: 1 hexadecimal digit (case-insensitive). Unicode: U+0030 - U+0039, U+0041 - U+0046,
    /// U+0061 - U+0066.
    /// </summary>
    public class HexadecimalDigit : Alternative<Digit, Element>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.HexadecimalDigit"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The hexadecimal digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HexadecimalDigit(Digit element, ITextContext context)
            : base(element, 1, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.HexadecimalDigit"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The hexadecimal digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HexadecimalDigit(Element element, ITextContext context)
            : base(element, 2, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}