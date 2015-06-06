// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleQuoteLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core
{
    using System;

    [RuleName("DQUOTE")]
    public class DoubleQuoteLexer : Lexer<DoubleQuote>
    {
        private readonly ILexer<Terminal> doubleQuoteTerminalLexer;

        /// <summary>
        /// </summary>
        /// <param name="doubleQuoteTerminalLexer">%x22</param>
        public DoubleQuoteLexer(ILexer<Terminal> doubleQuoteTerminalLexer)
        {
            if (doubleQuoteTerminalLexer == null)
            {
                throw new ArgumentNullException(
                    "doubleQuoteTerminalLexer",
                    "Precondition: doubleQuoteTerminalLexer != null");
            }

            this.doubleQuoteTerminalLexer = doubleQuoteTerminalLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out DoubleQuote element)
        {
            Terminal result;
            if (this.doubleQuoteTerminalLexer.TryRead(scanner, out result))
            {
                element = new DoubleQuote(result);
                return true;
            }

            element = default(DoubleQuote);
            return false;
        }
    }
}