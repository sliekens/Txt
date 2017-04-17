using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    public class HorizontalTab : Element
    {
        public HorizontalTab([NotNull] Element element)
            : base(element)
        {
        }

        public HorizontalTab([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public HorizontalTab(
            [NotNull] string sequence,
            [NotNull] IList<Element> elements,
            [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
