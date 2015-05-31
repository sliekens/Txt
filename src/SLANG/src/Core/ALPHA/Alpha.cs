// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alpha.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the ALPHA rule: 1 letter of the alphabet (case-insensitive). Unicode: U+0041-U+005A, U+0061-U+007A.</summary>
    public class Alpha : Element
    {
        public Alpha(Element element)
            : base(element)
        {
        }
    }
}