// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core.HEXDIG
{
    public class HexadecimalDigit : Alternative
    {
        public HexadecimalDigit(Alternative element)
            : base(element)
        {
        }

        public override string GetWellFormedText()
        {
            // Well-formed HEXDIG uses upper case letters
            return this.Values.ToUpperInvariant();
        }
    }
}