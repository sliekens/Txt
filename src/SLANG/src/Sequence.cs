namespace SLANG
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Sequence : Element
    {
        private readonly IList<Element> elements;

        public Sequence(Sequence sequence)
            : base(sequence)
        {
            this.elements = sequence.elements;
        }

        public Sequence(IList<Element> elements, ITextContext context)
            : base(string.Concat(elements), context)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements", "Precondition: elements != null");
            }

            this.elements = elements;
        }

        public IList<Element> Elements
        {
            get
            {
                Debug.Assert(this.elements != null, "this.elements != null");
                return this.elements;
            }
        }

        public override string GetWellFormedText()
        {
            return string.Concat(this.Elements.Select(element => element.GetWellFormedText()));
        }
    }
}