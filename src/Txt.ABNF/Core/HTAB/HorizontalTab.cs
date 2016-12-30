using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    public class HorizontalTab : Terminal
    {
        public HorizontalTab([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
