// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DQuoteToken.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the DQUOTE rule: 1 quotation mark. Unicode: U+0022.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the DQUOTE rule: 1 quotation mark. Unicode: U+0022.</summary>
    public class DQuoteToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="DQuoteToken"/> class. Creates a new instance of the <see cref="T:Text.Scanning.Core.DQuoteToken"/> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public DQuoteToken(ITextContext context)
            : base('\u0022', context)
        {
            Contract.Requires(context != null);
        }
    }
}