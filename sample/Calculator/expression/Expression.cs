using System.Collections.Generic;
using Txt.Core;

namespace Calculator.expression
{
    public class Expression : Element
    {
        public Expression(Element element)
            : base(element)
        {
        }

        public Expression(string terminals, ITextContext context)
            : base(terminals, context)
        {
        }

        public Expression(string sequence, IList<Element> elements, ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
