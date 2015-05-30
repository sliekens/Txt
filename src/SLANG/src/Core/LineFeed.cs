// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineFeed.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    /// <summary>Represents the LF rule: 1 line feed. Unicode: U+000A.</summary>
    public class LineFeed : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.LineFeed"/> class with a specified context.</summary>
        /// <param name="element">The line feed element.</param>
        public LineFeed(Element element)
            : base(element)
        {
        }
    }
}