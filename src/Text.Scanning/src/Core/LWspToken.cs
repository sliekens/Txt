namespace Text.Scanning.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class LWspToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<Token> lwsp;

        public LWspToken(IList<Tuple<CrLfToken, WSpToken>> data, ITextContext context)
            : this(Linearize(data), context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
        }

        private LWspToken(IList<Token> data, ITextContext context)
            : base(string.Concat(data), context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
            this.lwsp = data;
        }

        public IList<Token> LWsp
        {
            get
            {
                return this.lwsp;
            }
        }

        private static IList<Token> Linearize(IList<Tuple<CrLfToken, WSpToken>> data)
        {
            var tokens = new List<Token>();
            foreach (var tuple in data)
            {
                var crLf = tuple.Item1;
                if (crLf != null)
                {
                    tokens.Add(crLf);
                }

                var sp = tuple.Item2;
                if (sp != null)
                {
                    tokens.Add(sp);
                }
            }

            return tokens;
        }
    }
}