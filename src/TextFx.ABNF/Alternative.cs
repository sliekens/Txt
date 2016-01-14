namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.Annotations;

    public class Alternative : Element
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int ordinal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alternative"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="alternative" /> is a null reference.</exception>
        public Alternative([NotNull] Alternative alternative)
            : base(alternative)
        {
            ordinal = alternative.ordinal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="context"></param>
        /// <param name="ordinal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="elements" /> or <paramref name="context"/> is a null reference.</exception>
        public Alternative([NotNull][ItemNotNull] IList<Element> elements, [NotNull] ITextContext context, int ordinal)
            : base(elements, context)
        {
            if (elements.Count != 1)
            {
                throw new ArgumentException("Precondition: elements.Count == 1", nameof(elements));
            }
            this.ordinal = ordinal;
        }

        [NotNull]
        public Element Element
        {
            get
            {
                Debug.Assert(elements.Count == 1);
                return elements.Single();
            }
        }

        public int Ordinal
        {
            get
            {
                Debug.Assert(ordinal > 0, "this.ordinal > 0");
                return ordinal;
            }
        }
    }
}
