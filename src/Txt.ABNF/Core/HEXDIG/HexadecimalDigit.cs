using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigit : Element
    {
        public HexadecimalDigit([NotNull] Element element)
            : base(element)
        {
        }

        public HexadecimalDigit([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public HexadecimalDigit(
            [NotNull] string sequence,
            [NotNull] IList<Element> elements,
            [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
