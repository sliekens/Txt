// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLineLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;
    using System.Diagnostics;

    public class EndOfLineLexer : SequenceLexer<EndOfLine, CarriageReturn, LineFeed>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<CarriageReturn> carriageReturnLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<LineFeed> lineFeedLexer;

        public EndOfLineLexer()
            : this(new CarriageReturnLexer(), new LineFeedLexer())
        {
        }

        public EndOfLineLexer(ILexer<CarriageReturn> carriageReturnLexer, ILexer<LineFeed> lineFeedLexer)
            : base("CRLF")
        {
            if (carriageReturnLexer == null)
            {
                throw new ArgumentNullException("carriageReturnLexer", "Precondition: carriageReturnLexer != null");
            }

            if (lineFeedLexer == null)
            {
                throw new ArgumentNullException("lineFeedLexer", "Precondition: lineFeedLexer != null");
            }

            this.carriageReturnLexer = carriageReturnLexer;
            this.lineFeedLexer = lineFeedLexer;
        }

        protected override bool TryRead1(ITextScanner scanner, out CarriageReturn element)
        {
            return this.carriageReturnLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out LineFeed element)
        {
            return this.lineFeedLexer.TryRead(scanner, out element);
        }
    }
}