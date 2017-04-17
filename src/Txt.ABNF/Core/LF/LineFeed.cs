using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public class LineFeed : Element
    {
        public LineFeed([NotNull] Element element)
            : base(element)
        {
        }

        public LineFeed([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public LineFeed([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
