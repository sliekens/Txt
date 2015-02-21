namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the LWSP rule: any linear white space. The LWSP rule permits lines containing only white space.</summary>
    public class LWspElement : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<Element> lwsp;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Text.Scanning.Core.LWspElement" /> class with the specified
        /// characters and context.
        /// </summary>
        /// <param name="data">The collection of 'LWSP' components.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public LWspElement(IList<CrLfWSpPair> data, ITextContext context)
            : this(Linearize(data), context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
        }

        private LWspElement(IList<Element> data, ITextContext context)
            : base(string.Concat(data) ?? string.Empty, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.lwsp = data;
        }

        /// <summary>Gets the collection of 'LWSP' components.</summary>
        public IList<Element> LWsp
        {
            get
            {
                return this.lwsp;
            }
        }

        [Pure]
        private static IList<Element> Linearize(IList<CrLfWSpPair> data)
        {
            Contract.Requires(data != null);
            Contract.Ensures(Contract.Result<IList<Element>>() != null);
            var elements = new List<Element>();
            foreach (var pair in data)
            {
                if (pair == null)
                {
                    continue;
                }

                var crLf = pair.CrLf;
                if (crLf != null)
                {
                    elements.Add(crLf);
                }

                elements.Add(pair.Wsp);
            }

            return elements;
        }

        public class CrLfWSpPair
        {
            private readonly CrLfElement crLf;
            private readonly WSpElement wsp;

            public CrLfWSpPair(WSpElement wsp)
            {
                Contract.Requires(wsp != null);
                this.wsp = wsp;
            }

            public CrLfWSpPair(CrLfElement crLf, WSpElement wsp)
            {
                Contract.Requires(crLf != null);
                Contract.Requires(wsp != null);
                Contract.Requires(wsp.Offset == crLf.Offset + 2);
                this.crLf = crLf;
                this.wsp = wsp;
            }

            public CrLfElement CrLf
            {
                get
                {
                    return this.crLf;
                }
            }

            public WSpElement Wsp
            {
                get
                {
                    Contract.Ensures(Contract.Result<WSpElement>() != null);
                    return this.wsp;
                }
            }

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.wsp != null);
                Contract.Invariant(this.crLf == null || this.wsp.Offset == this.crLf.Offset + 2);
            }
        }
    }
}