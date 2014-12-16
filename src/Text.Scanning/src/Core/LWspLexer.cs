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
            try
            {
                while (true)
                {
                    CrLfToken crLfToken;
                    WSpToken spToken;
                    if (this.crLfLexer.TryRead(out crLfToken))
                    {
                        spToken = this.wSpLexer.Read();
                        data.Add(new Tuple<CrLfToken, WSpToken>(crLfToken, spToken));
                    }
                    else if (this.wSpLexer.TryRead(out spToken))
                    {
                        data.Add(new Tuple<CrLfToken, WSpToken>(null, spToken));
                    }
                    else
                    {
                        return new LWspToken(data, context);
                    }
                }
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException("Expected 'LWSP'", syntaxErrorException, context);
            }
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
                        // Possible BUG: when CRLF is NOT followed by SP / HTAB, the caller might expect the next 2 characters to be CRLF. This is impossible without backtracking, which is not supported.
                        // For now, let's just set the token to an illegal value and then return 'false' to indicate an error. This allows the caller to use the token data to recover from errors caused by 'LWSP CRLF *(OCTET)' strings.
                        data.Add(new Tuple<CrLfToken, WSpToken>(crLfToken, null));
                        token = new LWspToken(data, context);
                        return false;
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
