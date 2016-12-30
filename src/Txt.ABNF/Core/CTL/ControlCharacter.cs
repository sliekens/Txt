using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacter : Alternation
    {
        public ControlCharacter([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
