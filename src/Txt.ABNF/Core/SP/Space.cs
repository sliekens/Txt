using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

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
