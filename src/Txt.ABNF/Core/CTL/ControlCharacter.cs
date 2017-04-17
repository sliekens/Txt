using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacter : Element
    {
        public ControlCharacter([NotNull] Element element)
            : base(element)
        {
        }

        public ControlCharacter([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public ControlCharacter(
            [NotNull] string sequence,
            [NotNull] IList<Element> elements,
            [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
