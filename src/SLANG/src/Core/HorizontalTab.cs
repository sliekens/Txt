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
    using System.Diagnostics.Contracts;

    /// <summary>Represents the HTAB rule: 1 horizontal tab. Unicode: U+0009.</summary>
    public class HorizontalTab : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.HorizontalTab"/> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HorizontalTab(ITextContext context)
            : base("\u0009", context)
        {
            Contract.Requires(context != null);
        }
    }
}