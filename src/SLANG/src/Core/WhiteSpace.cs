// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the WSP rule: 1 SP character -or- 1 HTAB character. Unicode: U+0020, U+0009.</summary>
    public class WhiteSpace : Alternative<Space, HorizontalTab>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.WhiteSpace"/> class with a specified white space
        /// character and context.</summary>
        /// <param name="element">The space character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public WhiteSpace(Space element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.WhiteSpace"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The horizontal tab character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public WhiteSpace(HorizontalTab element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}