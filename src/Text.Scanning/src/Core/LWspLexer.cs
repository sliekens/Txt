namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class LWspLexer : Lexer<LWspToken>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<CrLfToken> crLfLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<WSpToken> wSpLexer;

        public LWspLexer()
            : this(new CrLfLexer(), new WSpLexer())
        {
        }

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
                WSpToken spToken;
                if (this.crLfLexer.TryRead(scanner, out crLfToken))
                {
                    if (!scanner.EndOfInput && this.wSpLexer.TryRead(scanner, out spToken))
                    {
                        data.Add(new LWspToken.CrLfWSpPair(crLfToken, spToken));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(scanner, crLfToken);
                        break;
                    }
                }
                else if (this.wSpLexer.TryRead(scanner, out spToken))
                {
                    data.Add(new LWspToken.CrLfWSpPair(spToken));
                }
                else
                {
                    break;
                }
            }

            token = new LWspToken(data, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.wSpLexer != null);
            Contract.Invariant(this.crLfLexer != null);
        }
    }
}