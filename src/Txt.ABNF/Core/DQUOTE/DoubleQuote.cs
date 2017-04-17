using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public class DoubleQuote : Element
    {
        public DoubleQuote([NotNull] Element element)
            : base(element)
        {
        }

        public DoubleQuote([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public DoubleQuote([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
