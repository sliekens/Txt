// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Character.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CHAR rule: 1 US-ASCII character, excluding NUL. Unicode: U+0001 - U+007F.</summary>
    public class Character : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.Character"/> class with a specified character
        /// and context.</summary>
        /// <param name="data">The character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Character(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\u0001' && data <= '\u007F');
            Contract.Requires(context != null);
        }
    }
}