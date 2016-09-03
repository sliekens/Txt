using JetBrains.Annotations;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpace : Alternation
    {
        public WhiteSpace([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
