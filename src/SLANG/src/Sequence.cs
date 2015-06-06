namespace SLANG
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Sequence : Element
    {
        private readonly IList<Element> elements;

        public Sequence(Sequence repetition)
            : base(repetition)
        {
            this.elements = repetition.elements;
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

        public override string GetWellFormedData()
        {
            return string.Concat(this.Elements.Select(element => element.GetWellFormedData()));
        }
    }
}
