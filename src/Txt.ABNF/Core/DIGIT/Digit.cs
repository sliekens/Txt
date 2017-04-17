using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public class Digit : Element
    {
        public Digit([NotNull] Element element)
            : base(element)
        {
        }

        public Digit([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public Digit([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
