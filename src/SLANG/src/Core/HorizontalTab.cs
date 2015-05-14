// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalTab.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the HTAB rule: 1 horizontal tab. Unicode: U+0009.</summary>
    public class HorizontalTab : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.HorizontalTab"/> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HorizontalTab(ITextContext context)
            : base("\x09", context)
        {
        }
    }
}