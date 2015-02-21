// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharToken.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the CHAR rule: 1 US-ASCII character, excluding NUL. Unicode: U+0001 - U+007F.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CHAR rule: 1 US-ASCII character, excluding NUL. Unicode: U+0001 - U+007F.</summary>
    public class CharToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="CharToken"/> class. Creates a new instance of the <see cref="T:Text.Scanning.Core.CharToken"/> class with a specified character
        /// and context.</summary>
        /// <param name="data">The character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public CharToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\u0001' && data <= '\u007F');
            Contract.Requires(context != null);
        }
    }
}