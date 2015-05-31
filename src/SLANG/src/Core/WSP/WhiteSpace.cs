// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the WSP rule: 1 SP character -or- 1 HTAB character. Unicode: U+0020, U+0009.</summary>
    public class WhiteSpace : Element
    {
        public WhiteSpace(Element element)
            : base(element)
        {
        }
    }
}