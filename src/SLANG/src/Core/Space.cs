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
    /// <summary>Represents the SP rule: 1 space. Unicode: U+0020.</summary>
    public class Space : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.Space"/> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Space(ITextContext context)
            : base('\x20', context)
        {
        }
    }
}