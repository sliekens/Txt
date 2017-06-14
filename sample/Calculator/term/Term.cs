using System.Collections.Generic;
using Txt.Core;

namespace Calculator.term
{
    public class Term : Element
    {
        public Term(Element element)
            : base(element)
        {
        }

        public Term(string terminals, ITextContext context)
            : base(terminals, context)
        {
        }

        public Term(string sequence, IList<Element> elements, ITextContext context)
            : base(sequence, elements, context)
        {
        }
    }
}
