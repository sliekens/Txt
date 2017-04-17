using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    public class Space : Element
    {
        public Space([NotNull] Element element)
            : base(element)
        {
        }

        public Space([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public Space([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
