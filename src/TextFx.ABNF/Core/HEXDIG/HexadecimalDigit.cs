// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using JetBrains.Annotations;

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
