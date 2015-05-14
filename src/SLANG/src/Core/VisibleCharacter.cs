// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacter.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the VCHAR rule: 1 visible (printing) character. Unicode: U+0041 - U+005A, U+0061 - U+007A.</summary>
    public class VisibleCharacter : Alternative
    {
        public VisibleCharacter(char data, ITextContext context)
            : base(data, '\x21', '\x7E', context)
        {
        }
    }
}