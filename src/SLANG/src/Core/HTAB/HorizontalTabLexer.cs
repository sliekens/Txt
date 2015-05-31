// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalTabLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class HorizontalTabLexer : Lexer<HorizontalTab>
    {
        private readonly ILexer horizontalTabTerminalLexer;

        /// <summary>Initializes a new instance of the <see cref="HorizontalTabLexer"/> class.</summary>
        public HorizontalTabLexer(ILexer horizontalTabTerminalLexer)
            : base("HTAB")
        {
            if (horizontalTabTerminalLexer == null)
            {
                throw new ArgumentNullException("horizontalTabTerminalLexer", "Precondition: horizontalTabTerminalLexer != null");
            }

            this.horizontalTabTerminalLexer = horizontalTabTerminalLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HorizontalTab element)
        {
            Element terminal;
            if (this.horizontalTabTerminalLexer.TryReadElement(scanner, out terminal))
            {
                element = new HorizontalTab(terminal);
                return true;
            }
            
            element = default(HorizontalTab);
            return false;
        }
    }
}