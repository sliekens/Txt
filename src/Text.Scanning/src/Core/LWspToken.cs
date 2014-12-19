namespace Text.Scanning.Core
{
    using System;
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

        /// <summary>Gets the collection of 'LWSP' components.</summary>
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