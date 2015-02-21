namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the LWSP rule: any linear white space. The LWSP rule permits lines containing only white space.</summary>
    public class LWspToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<Token> lwsp;

        /// <summary>
        /// Creates a new instance of the <see cref="T:Text.Scanning.Core.LWspToken" /> class with the specified
        /// characters and context.
        /// </summary>
        /// <param name="data">The collection of 'LWSP' components.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public LWspToken(IList<CrLfWSpPair> data, ITextContext context)
            : this(Linearize(data), context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
        }

        private LWspToken(IList<Token> data, ITextContext context)
            : base(string.Concat(data) ?? string.Empty, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.lwsp = data;
        }

        /// <summary>Gets the collection of 'LWSP' components.</summary>
        public IList<Token> LWsp
        {
            get
            {
                return this.lwsp;
            }
        }

        [Pure]
        private static IList<Token> Linearize(IList<CrLfWSpPair> data)
        {
            Contract.Requires(data != null);
            Contract.Ensures(Contract.Result<IList<Token>>() != null);
            var tokens = new List<Token>();
            foreach (var pair in data)
            {
                if (pair == null)
                {
                    continue;
                }

                var crLf = pair.CrLf;
                if (crLf != null)
                {
                    tokens.Add(crLf);
                }

                tokens.Add(pair.Wsp);
            }

            return tokens;
        }

        public class CrLfWSpPair
        {
            private readonly CrLfToken crLf;
            private readonly WSpToken wsp;

            public CrLfWSpPair(WSpToken wsp)
            {
                Contract.Requires(wsp != null);
                this.wsp = wsp;
            }

            public CrLfWSpPair(CrLfToken crLf, WSpToken wsp)
            {
                Contract.Requires(crLf != null);
                Contract.Requires(wsp != null);
                Contract.Requires(wsp.Offset == crLf.Offset + 2);
                this.crLf = crLf;
                this.wsp = wsp;
            }

            public CrLfToken CrLf
            {
                get
                {
                    return this.crLf;
                }
            }

            public WSpToken Wsp
            {
                get
                {
                    Contract.Ensures(Contract.Result<WSpToken>() != null);
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