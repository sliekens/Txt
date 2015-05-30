// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleQuote.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    /// <summary>Represents the DQUOTE rule: 1 quotation mark. Unicode: U+0022.</summary>
    public class DoubleQuote : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.DoubleQuote"/> class with a specified context.</summary>
        /// <param name="element">The double quote element.</param>
        public DoubleQuote(Element element)
            : base(element)
        {
        }
    }
}