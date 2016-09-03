using JetBrains.Annotations;

namespace Txt.ABNF.Core.BIT
{
    public class Bit : Alternation
    {
        public Bit([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
