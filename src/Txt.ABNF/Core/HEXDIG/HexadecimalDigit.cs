// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigit : Alternation
    {
        public HexadecimalDigit([NotNull] Alternation element)
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
