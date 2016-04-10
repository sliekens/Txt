// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Jetbrains.Annotations;

namespace Text.ABNF.Core.HEXDIG
{
    public class HexadecimalDigit : Alternative
    {
        public HexadecimalDigit([NotNull] Alternative element)
            : base(element)
        {
        }

        public override string GetWellFormedText()
        {
            // Well-formed HEXDIG uses upper case letters
            return Text.ToUpperInvariant();
        }
    }
}
