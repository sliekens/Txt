// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleQuoteLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary></summary>
    public class DoubleQuoteLexer : Lexer<DoubleQuote>
    {
        /// <summary>Initializes a new instance of the <see cref="DoubleQuoteLexer"/> class.</summary>
        public DoubleQuoteLexer()
            : base("DQUOTE")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out DoubleQuote element)
        {
            Element terminal;
            if (!TryReadTerminal(scanner, '\x22', out terminal))
            {
                element = default(DoubleQuote);
                return false;
            }

            element = new DoubleQuote(terminal);
            return true;
        }
    }
}