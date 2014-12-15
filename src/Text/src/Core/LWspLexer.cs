using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Text.Core
{
    public class LWspLexer : Lexer<LWspToken>
    {
        private readonly ILexer<CrLfToken> crLfLexer;
        private readonly ILexer<SpToken> spLexer;

        public LWspLexer(ITextScanner scanner)
            : this(scanner, new CrLfLexer(scanner), new SpLexer(scanner))
        {
            Contract.Requires(scanner != null);
        }

        public LWspLexer(ITextScanner scanner, ILexer<CrLfToken> crLfLexer, ILexer<SpToken> spLexer)
            : base(scanner)
        {
            Contract.Requires(scanner != null);
            Contract.Requires(crLfLexer != null);
            Contract.Requires(spLexer != null);
            this.crLfLexer = crLfLexer;
            this.spLexer = spLexer;
        }

        public override LWspToken Read()
        {
            var context = this.Scanner.GetContext();
            var data = new List<Tuple<CrLfToken, SpToken>>();
            try
            {
                while (true)
                {
                    CrLfToken crLfToken;
                    SpToken spToken;
                    if (this.crLfLexer.TryRead(out crLfToken))
                    {
                        spToken = this.spLexer.Read();
                        data.Add(new Tuple<CrLfToken, SpToken>(crLfToken, spToken));
                    }
                    else if (this.spLexer.TryRead(out spToken))
                    {
                        data.Add(new Tuple<CrLfToken, SpToken>(null, spToken));
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
            var data = new List<Tuple<CrLfToken, SpToken>>();
            while (true)
            {
                CrLfToken crLfToken;
                SpToken spToken;
                if (this.crLfLexer.TryRead(out crLfToken))
                {
                    if (this.spLexer.TryRead(out spToken))
                    {
                        data.Add(new Tuple<CrLfToken, SpToken>(crLfToken, spToken));
                    }
                    else
                    {
                        // Possible BUG: when CRLF is NOT followed by SP, the caller might expect the next 2 characters to be CRLF. This is impossible without backtracking, which is not supported.
                        // For now, let's just set the token to an illegal value and then return 'false' to indicate an error. This allows the caller to use the token data to recover from errors caused by 'LWSP CRLF *(OCTET)' strings.
                        data.Add(new Tuple<CrLfToken, SpToken>(crLfToken, null));
                        token = new LWspToken(data, context);
                        return false;
                    }
                }
                else if (this.spLexer.TryRead(out spToken))
                {
                    data.Add(new Tuple<CrLfToken, SpToken>(null, spToken));
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
