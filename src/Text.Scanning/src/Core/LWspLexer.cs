using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Text.Scanning.Core
{
    public class LWspLexer : Lexer<LWspToken>
    {
        private readonly ILexer<CrLfToken> crLfLexer;
        private readonly ILexer<WSpToken> wSpLexer;

        public LWspLexer(ITextScanner scanner)
            : this(scanner, new CrLfLexer(scanner), new WSpLexer(scanner))
        {
            Contract.Requires(scanner != null);
        }

        public LWspLexer(ITextScanner scanner, ILexer<CrLfToken> crLfLexer, ILexer<WSpToken> wSpLexer)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
            Contract.Requires(crLfLexer != null);
            Contract.Requires(wSpLexer != null);
            this.crLfLexer = crLfLexer;
            this.wSpLexer = wSpLexer;
        }

        public override LWspToken Read()
        {
            var context = this.Scanner.GetContext();
            var data = new List<Tuple<CrLfToken, WSpToken>>();

            // Infinite loop: there are multiple break conditions
            // The program should eventually exit this loop, unless the source data is an infinite stream of linear whitespace
            while (true)
            {
                CrLfToken crLfToken;
                WSpToken wSpToken;
                if (this.wSpLexer.TryRead(out wSpToken))
                {
                    data.Add(new Tuple<CrLfToken, WSpToken>(null, wSpToken));
                }
                else if (this.crLfLexer.TryRead(out crLfToken))
                {
                    if (this.wSpLexer.TryRead(out wSpToken))
                    {
                        data.Add(new Tuple<CrLfToken, WSpToken>(crLfToken, wSpToken));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(crLfToken);
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

        public override bool TryRead(out LWspToken token)
        {
            var context = this.Scanner.GetContext();
            var data = new List<Tuple<CrLfToken, WSpToken>>();
            while (true)
            {
                CrLfToken crLfToken;
                WSpToken spToken;
                if (this.crLfLexer.TryRead(out crLfToken))
                {
                    if (this.wSpLexer.TryRead(out spToken))
                    {
                        data.Add(new Tuple<CrLfToken, WSpToken>(crLfToken, spToken));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(crLfToken);
                        token = new LWspToken(data, context);
                        return true;
                    }
                }
                else if (this.wSpLexer.TryRead(out spToken))
                {
                    data.Add(new Tuple<CrLfToken, WSpToken>(null, spToken));
                }
                else
                {
                    token = new LWspToken(data, context);
                    return true;
                }
            }
        }
    }
}
