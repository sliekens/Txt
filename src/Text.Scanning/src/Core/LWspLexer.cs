// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LWspLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The l wsp lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>The l wsp lexer.</summary>
    public class LWspLexer : Lexer<LWspToken>
    {
        /// <summary>The cr lf lexer.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<CrLfToken> crLfLexer;

        /// <summary>The w sp lexer.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<WSpToken> wSpLexer;

        /// <summary>Initializes a new instance of the <see cref="LWspLexer"/> class.</summary>
        public LWspLexer()
            : this(new CrLfLexer(), new WSpLexer())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="LWspLexer"/> class.</summary>
        /// <param name="crLfLexer">The cr lf lexer.</param>
        /// <param name="wSpLexer">The w sp lexer.</param>
        public LWspLexer(ILexer<CrLfToken> crLfLexer, ILexer<WSpToken> wSpLexer)
        {
            Contract.Requires(crLfLexer != null);
            Contract.Requires(wSpLexer != null);
            this.crLfLexer = crLfLexer;
            this.wSpLexer = wSpLexer;
        }

        /// <inheritdoc />
        public override LWspToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            var data = new List<LWspToken.CrLfWSpPair>();

            // The program should eventually exit this loop, unless the source data is an infinite stream of linear whitespace
            while (!scanner.EndOfInput)
            {
                CrLfToken crLfToken;
                WSpToken wSpToken;
                if (this.wSpLexer.TryRead(scanner, out wSpToken))
                {
                    data.Add(new LWspToken.CrLfWSpPair(wSpToken));
                }
                else if (this.crLfLexer.TryRead(scanner, out crLfToken))
                {
                    if (!scanner.EndOfInput && this.wSpLexer.TryRead(scanner, out wSpToken))
                    {
                        Contract.Assume(wSpToken.Offset == crLfToken.Offset + 2);
                        data.Add(new LWspToken.CrLfWSpPair(crLfToken, wSpToken));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(scanner, crLfToken);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return new LWspToken(data, context);
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LWspToken token)
        {
            var context = scanner.GetContext();
            var data = new List<LWspToken.CrLfWSpPair>();

            // The program should eventually exit this loop, unless the source data is an infinite stream of linear whitespace
            while (!scanner.EndOfInput)
            {
                CrLfToken crLfToken;
                WSpToken wSpToken;
                if (this.crLfLexer.TryRead(scanner, out crLfToken))
                {
                    if (!scanner.EndOfInput && this.wSpLexer.TryRead(scanner, out wSpToken))
                    {
                        Contract.Assume(wSpToken.Offset == crLfToken.Offset + 2);
                        data.Add(new LWspToken.CrLfWSpPair(crLfToken, wSpToken));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(scanner, crLfToken);
                        break;
                    }
                }
                else if (this.wSpLexer.TryRead(scanner, out wSpToken))
                {
                    data.Add(new LWspToken.CrLfWSpPair(wSpToken));
                }
                else
                {
                    break;
                }
            }

            token = new LWspToken(data, context);
            return true;
        }

        /// <summary>The object invariant.</summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.wSpLexer != null);
            Contract.Invariant(this.crLfLexer != null);
        }
    }
}