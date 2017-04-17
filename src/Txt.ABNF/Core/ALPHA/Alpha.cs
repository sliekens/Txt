using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class Alpha : Element
    {
        public Alpha([NotNull] Element element)
            : base(element)
        {
        }

        public Alpha([NotNull] string terminals, [NotNull] ITextContext context)
            : base(terminals, context)
        {
        }

        public Alpha([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
