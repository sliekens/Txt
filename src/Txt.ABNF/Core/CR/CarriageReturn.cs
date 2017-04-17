using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturn : Element
    {
        public CarriageReturn([NotNull] Element element)
            : base(element)
        {
        }

        public CarriageReturn([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public CarriageReturn(
            [NotNull] string sequence,
            [NotNull] IList<Element> elements,
            [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
