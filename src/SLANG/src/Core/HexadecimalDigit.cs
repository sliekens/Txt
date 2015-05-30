// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>
    /// Represents the HEXDIG rule: 1 hexadecimal digit (case-insensitive). Unicode: U+0030 - U+0039, U+0041 - U+0046,
    /// U+0061 - U+0066.
    /// </summary>
    public class HexadecimalDigit : Element
    {
        public HexadecimalDigit(Element element)
            : base(element)
        {
        }
    }
}