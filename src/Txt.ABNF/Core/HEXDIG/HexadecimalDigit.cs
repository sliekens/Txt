using JetBrains.Annotations;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigit : Alternation
    {
        public HexadecimalDigit([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
