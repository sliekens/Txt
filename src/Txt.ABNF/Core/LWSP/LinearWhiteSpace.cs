using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    public class LinearWhiteSpace : Repetition
    {
        public LinearWhiteSpace([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
