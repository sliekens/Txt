// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the BIT rule: "0" / "1"</summary>
    public class Bit : Alternative
    {
        public Bit(Element element)
            : base(element)
        {
        }
    }
}