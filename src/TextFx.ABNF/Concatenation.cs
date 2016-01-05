using System.Collections.Generic;

namespace TextFx.ABNF
{
    public class Concatenation : Element
    {
        public Concatenation(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public Concatenation(IList<Element> elements, ITextContext context)
            : base(elements, context)
        {
        }
    }
}