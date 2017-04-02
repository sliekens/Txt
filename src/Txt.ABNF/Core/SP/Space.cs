using JetBrains.Annotations;

namespace Txt.ABNF.Core.SP
{
    public class Space : Terminal
    {
        public Space([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
