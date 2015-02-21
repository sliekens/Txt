// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LWspToken.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the LWSP rule: any linear white space. The LWSP rule permits lines containing only white space.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents the LWSP rule: any linear white space. The LWSP rule permits lines containing only white space.</summary>
    public class LWspToken : Token
    {
        /// <summary>The lwsp.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<Token> lwsp;

        /// <summary>Initializes a new instance of the <see cref="LWspToken"/> class. Creates a new instance of the <see cref="T:Text.Scanning.Core.LWspToken"/> class with the specified
        /// characters and context.</summary>
        /// <param name="data">The collection of 'LWSP' components.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public LWspToken(IList<CrLfWSpPair> data, ITextContext context)
            : this(Linearize(data), context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
        }

        /// <summary>Initializes a new instance of the <see cref="LWspToken"/> class.</summary>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
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

        /// <summary>The linearize.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The <see cref="IList"/>.</returns>
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

        /// <summary>The cr lf w sp pair.</summary>
        public class CrLfWSpPair
        {
            /// <summary>The cr lf.</summary>
            private readonly CrLfToken crLf;

            /// <summary>The wsp.</summary>
            private readonly WSpToken wsp;

            /// <summary>Initializes a new instance of the <see cref="CrLfWSpPair"/> class.</summary>
            /// <param name="wsp">The wsp.</param>
            public CrLfWSpPair(WSpToken wsp)
            {
                Contract.Requires(wsp != null);
                this.wsp = wsp;
            }

            /// <summary>Initializes a new instance of the <see cref="CrLfWSpPair"/> class.</summary>
            /// <param name="crLf">The cr lf.</param>
            /// <param name="wsp">The wsp.</param>
            public CrLfWSpPair(CrLfToken crLf, WSpToken wsp)
            {
                Contract.Requires(crLf != null);
                Contract.Requires(wsp != null);
                Contract.Requires(wsp.Offset == crLf.Offset + 2);
                this.crLf = crLf;
                this.wsp = wsp;
            }

            /// <summary>Gets the cr lf.</summary>
            public CrLfToken CrLf
            {
                get
                {
                    return this.crLf;
                }
            }

            /// <summary>Gets the wsp.</summary>
            public WSpToken Wsp
            {
                get
                {
                    Contract.Ensures(Contract.Result<WSpToken>() != null);
                    return this.wsp;
                }
            }

            /// <summary>The object invariant.</summary>
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.wsp != null);
                Contract.Invariant(this.crLf == null || this.wsp.Offset == this.crLf.Offset + 2);
            }
        }
    }
}