using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    public class VisibleCharacter : Element
    {
        public VisibleCharacter([NotNull] Element element)
            : base(element)
        {
        }

        public VisibleCharacter([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public VisibleCharacter(
            [NotNull] string sequence,
            [NotNull] IList<Element> elements,
            [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
