using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpace : Alternation
    {
        public WhiteSpace([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
