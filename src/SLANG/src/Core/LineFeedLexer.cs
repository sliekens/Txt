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

    public partial class LineFeedLexer : Lexer<LineFeed>
    {
        private readonly ILexer<Element> lineFeedTerminalLexer;

        public LineFeedLexer(ILexer<Element> lineFeedTerminalLexer)
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
            if (this.lineFeedTerminalLexer.TryRead(scanner, out terminal))
            {
                element = new LineFeed(terminal);
                return true;
            }

            element = default(LineFeed);
            return false;
        }
    }

    public partial class LineFeedLexer
    {
        public class LineFeedTerminalLexer : TerminalsLexer
        {
            public LineFeedTerminalLexer()
                : base('\x0A')
            {
            }
        }
    }
}