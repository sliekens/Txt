using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    public class Character : Element
    {
        public Character([NotNull] Element element)
            : base(element)
        {
        }

        public Character([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public Character([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
