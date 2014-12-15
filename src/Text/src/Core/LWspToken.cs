using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Text.Core
{
    public class LWspToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<Token> lwsp;

        public LWspToken(IList<Tuple<CrLfToken, SpToken>> data, ITextContext context)
            : this(Linearize(data), context)
        {

        }

        private LWspToken(IList<Token> data, ITextContext context)
            : base(string.Concat(data), context)
        {
            this.lwsp = data;
        }

        public IList<Token> LWsp
        {
            get
            {
                return this.lwsp;
            }
        }

        private static IList<Token> Linearize(IList<Tuple<CrLfToken, SpToken>> data)
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
