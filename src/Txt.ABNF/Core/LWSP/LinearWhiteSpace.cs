using JetBrains.Annotations;

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
