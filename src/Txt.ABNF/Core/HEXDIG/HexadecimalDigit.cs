using JetBrains.Annotations;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigit : Alternation
    {
        public HexadecimalDigit([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
