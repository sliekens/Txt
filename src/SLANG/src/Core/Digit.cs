// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Digit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the DIGIT rule: 1 digit. Unicode: U+0030 - U+0039.</summary>
    public class Digit : Alternative
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.Digit"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The digit.</param>
        public Digit(Element element)
            : base(element, '\x30', '\x39')
        {
        }
    }
}