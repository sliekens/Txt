// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineFeedLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class LineFeedLexer : Lexer<LineFeed>
    {
        private readonly ILexer lineFeedTerminalLexer;

        public LineFeedLexer(ILexer lineFeedTerminalLexer)
            : base("LF")
        {
            if (lineFeedTerminalLexer == null)
            {
                throw new ArgumentNullException("lineFeedTerminalLexer", "Precondition: lineFeedTerminalLexer != null");
            }

            this.lineFeedTerminalLexer = lineFeedTerminalLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LineFeed element)
        {
            Element terminal;
            if (this.lineFeedTerminalLexer.TryReadElement(scanner, out terminal))
            {
                element = new LineFeed(terminal);
                return true;
            }

            element = default(LineFeed);
            return false;
        }
    }
}