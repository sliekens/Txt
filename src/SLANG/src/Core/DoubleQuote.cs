// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleQuote.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the DQUOTE rule: 1 quotation mark. Unicode: U+0022.</summary>
    public class DoubleQuote : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.DoubleQuote"/> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public DoubleQuote(ITextContext context)
            : base('\x22', context)
        {
            Contract.Requires(context != null);
        }
    }
}