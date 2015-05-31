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
        private readonly ILexer<Element> lineFeedTerminalLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineFeedTerminalLexer">%x0A</param>
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
            Element result;
            if (this.lineFeedTerminalLexer.TryRead(scanner, out result))
            {
                element = new LineFeed(result);
                return true;
            }

            element = default(LineFeed);
            return false;
        }
    }
}