using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

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
