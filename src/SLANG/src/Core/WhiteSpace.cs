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
    /// <summary>Represents the WSP rule: 1 SP character -or- 1 HTAB character. Unicode: U+0020, U+0009.</summary>
    public class WhiteSpace : Alternative<Space, HorizontalTab>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.WhiteSpace"/> class with a specified white space
        /// character and context.</summary>
        /// <param name="element">The space character.</param>
        public WhiteSpace(Space element)
            : base(element, 1)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.WhiteSpace"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The horizontal tab character.</param>
        public WhiteSpace(HorizontalTab element)
            : base(element, 2)
        {
        }
    }
}