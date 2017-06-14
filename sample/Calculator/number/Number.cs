using System.Collections.Generic;
using Txt.Core;

namespace Calculator.number
{
    public class Number : Element
    {
        public Number(Element element)
            : base(element)
        {
        }

        public Number(string terminals, ITextContext context)
            : base(terminals, context)
        {
        }

        public Number(string sequence, IList<Element> elements, ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
