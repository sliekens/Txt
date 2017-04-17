using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpace : Element
    {
        public WhiteSpace([NotNull] Element element)
            : base(element)
        {
        }

        public WhiteSpace([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public WhiteSpace([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
