using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLine : Element
    {
        public NewLine([NotNull] Element element)
            : base(element)
        {
        }

        public NewLine([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public NewLine([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
