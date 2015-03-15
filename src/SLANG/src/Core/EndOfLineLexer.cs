// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLineLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

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
            Contract.Requires(carriageReturnLexer != null);
            Contract.Requires(lineFeedLexer != null);
            this.carriageReturnLexer = carriageReturnLexer;
            this.lineFeedLexer = lineFeedLexer;
        }

        protected override EndOfLine CreateInstance(CarriageReturn element1, LineFeed element2, ITextContext context)
        {
            return new EndOfLine(element1, element2, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out CarriageReturn element)
        {
            return this.carriageReturnLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out LineFeed element)
        {
            return this.lineFeedLexer.TryRead(scanner, out element);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.carriageReturnLexer != null);
            Contract.Invariant(this.lineFeedLexer != null);
        }
    }
}