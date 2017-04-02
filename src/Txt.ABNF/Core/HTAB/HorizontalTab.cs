using JetBrains.Annotations;

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
