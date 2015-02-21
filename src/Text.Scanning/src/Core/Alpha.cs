// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alpha.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the ALPHA rule: 1 letter of the alphabet (case-insensitive). Unicode: U+0041-U+005A, U+0061-U+007A.</summary>
    public class Alpha : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.Alpha"/> class with a specified character and context.</summary>
        /// <param name="data">The letter.</param>
        /// <param name="context">The object that describes the context in which the text
        /// appears.</param>
        public Alpha(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires((data >= '\u0041' && data <= '\u005A') || (data >= '\u0061' && data <= '\u007A'));
            Contract.Requires(context != null);
        }
    }
}