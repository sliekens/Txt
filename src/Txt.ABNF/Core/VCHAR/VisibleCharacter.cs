using JetBrains.Annotations;

namespace Txt.ABNF.Core.VCHAR
{
    public class VisibleCharacter : Terminal
    {
        public VisibleCharacter([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
