// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Space.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    /// <summary>Represents the SP rule: 1 space. Unicode: U+0020.</summary>
    public class Space : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.Space"/> class with a specified context.</summary>
        /// <param name="element">The space element.</param>
        public Space(Element element)
            : base(element)
        {
            if (element.Data != "\x20")
            {
                throw new ArgumentOutOfRangeException("element", "Precondition: element.Data == \"\\x20\"");
            }
        }
    }
}