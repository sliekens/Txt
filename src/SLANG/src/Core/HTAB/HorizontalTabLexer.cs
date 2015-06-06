// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalTabLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core.HTAB
{
    using System;

    [RuleName("HTAB")]
    public class HorizontalTabLexer : Lexer<HorizontalTab>
    {
        private readonly ILexer<Terminal> horizontalTabTerminalLexer;

        /// <summary>
        /// </summary>
        /// <param name="horizontalTabTerminalLexer">%x09</param>
        public HorizontalTabLexer(ILexer<Terminal> horizontalTabTerminalLexer)
        {
            if (horizontalTabTerminalLexer == null)
            {
                throw new ArgumentNullException(
                    "horizontalTabTerminalLexer",
                    "Precondition: horizontalTabTerminalLexer != null");
            }

            this.horizontalTabTerminalLexer = horizontalTabTerminalLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HorizontalTab element)
        {
            Terminal result;
            if (this.horizontalTabTerminalLexer.TryRead(scanner, out result))
            {
                element = new HorizontalTab(result);
                return true;
            }

            element = default(HorizontalTab);
            return false;
        }
    }
}