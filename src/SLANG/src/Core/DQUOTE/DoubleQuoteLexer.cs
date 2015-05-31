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

    public class DoubleQuoteLexer : Lexer<DoubleQuote>
    {
        private readonly ILexer doubleQuoteTerminalLexer;

        public DoubleQuoteLexer(ILexer doubleQuoteTerminalLexer)
            : base("DQUOTE")
        {
            if (doubleQuoteTerminalLexer == null)
            {
                throw new ArgumentNullException("doubleQuoteTerminalLexer", "Precondition: doubleQuoteTerminalLexer != null");
            }

            this.doubleQuoteTerminalLexer = doubleQuoteTerminalLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out DoubleQuote element)
        {
            Element terminal;
            if (this.doubleQuoteTerminalLexer.TryReadElement(scanner, out terminal))
            {
                element = new DoubleQuote(terminal);
                return true;
            }

            element = default(DoubleQuote);
            return false;
        }
    }
}