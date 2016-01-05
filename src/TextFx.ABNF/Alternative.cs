using System;
using System.Collections.Generic;
using System.Linq;

namespace TextFx.ABNF
{
    using System.Diagnostics;

    public class Alternative : Element
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int ordinal;

        public Alternative(Alternative alternative)
            : base(alternative)
        {
            this.ordinal = alternative.ordinal;
        }

        public Alternative(IList<Element> elements, ITextContext context, int ordinal)
            : base(elements, context)
        {
            if (elements.Count != 1)
            {
                throw new ArgumentException("Precondition: elements.Count == 1", nameof(elements));
            }

            this.ordinal = ordinal;
        }

        public Element Element
        {
            get
            {
                Debug.Assert(this.elements.Count == 1);
                return this.elements.Single();
            }
        }

        public int Ordinal
        {
            get
            {
                Debug.Assert(this.ordinal > 0, "this.ordinal > 0");
                return this.ordinal;
            }
        }
    }
}