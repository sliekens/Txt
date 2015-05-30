// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalTabLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class HorizontalTabLexer : Lexer<HorizontalTab>
    {
        /// <summary>Initializes a new instance of the <see cref="HorizontalTabLexer"/> class.</summary>
        public HorizontalTabLexer()
            : base("HTAB")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HorizontalTab element)
        {
            Element terminal;
            if (!TryReadTerminal(scanner, '\x09', out terminal))
            {
                element = default(HorizontalTab);
                return false;
            }

            element = new HorizontalTab(terminal);
            return true;
        }
    }
}