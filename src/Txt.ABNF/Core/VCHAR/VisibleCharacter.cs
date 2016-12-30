using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

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
