using System.Collections.Generic;
using Txt.Core;

namespace Calculator.factor
{
    public class Factor : Element
    {
        public Factor(Element element)
            : base(element)
        {
        }

        public Factor(string terminals, ITextContext context)
            : base(terminals, context)
        {
        }

        public Factor(string sequence, IList<Element> elements, ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
