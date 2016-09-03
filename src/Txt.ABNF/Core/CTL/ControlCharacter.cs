using JetBrains.Annotations;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacter : Alternation
    {
        public ControlCharacter([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
