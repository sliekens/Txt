// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Character.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the CHAR rule: 1 US-ASCII character, excluding NUL. Unicode: U+0001 - U+007F.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the CHAR rule: 1 US-ASCII character, excluding NUL. Unicode: U+0001 - U+007F.</summary>
    public class Character : Alternative
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.Character"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The character.</param>
        public Character(Element element)
            : base(element, '\x01', '\x7F')
        {
        }
    }
}