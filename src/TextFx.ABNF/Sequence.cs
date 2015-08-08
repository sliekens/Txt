using System.Collections.Generic;

namespace TextFx.ABNF
{
    public class Sequence : Element
    {
        public Sequence(Sequence sequence)
            : base(sequence)
        {
        }

        public Sequence(IList<Element> elements, ITextContext context)
            : base(elements, context)
        {
        }
    }
}