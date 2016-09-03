using JetBrains.Annotations;

namespace Txt.ABNF.Core.ALPHA
{
    public class Alpha : Alternation
    {
        public Alpha([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
