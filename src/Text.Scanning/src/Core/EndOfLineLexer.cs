// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLineLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    public class EndOfLineLexer : Lexer<EndOfLine>
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<CarriageReturn> crLexer;

        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<LineFeed> lfLexer;

        /// <summary>Initializes a new instance of the <see cref="EndOfLineLexer"/> class.</summary>
        public EndOfLineLexer()
            : this(new CarriageReturnLexer(), new LineFeedLexer())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="EndOfLineLexer"/> class.</summary>
        /// <param name="crLexer">TODO The cr lexer.</param>
        /// <param name="lfLexer">TODO The lf lexer.</param>
        public EndOfLineLexer(ILexer<CarriageReturn> crLexer, ILexer<LineFeed> lfLexer)
            : base("CRLF")
        {
            Contract.Requires(crLexer != null);
            Contract.Requires(lfLexer != null);
            this.crLexer = crLexer;
            this.lfLexer = lfLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out EndOfLine element)
        {
            var context = scanner.GetContext();
            CarriageReturn carriageReturn;
            if (scanner.EndOfInput || !this.crLexer.TryRead(scanner, out carriageReturn))
            {
                element = default(EndOfLine);
                return false;
            }

            LineFeed lineFeed;
            if (scanner.EndOfInput || !this.lfLexer.TryRead(scanner, out lineFeed))
            {
                scanner.PutBack(carriageReturn.Data);
                element = default(EndOfLine);
                return false;
            }

            Contract.Assume(lineFeed.Offset == carriageReturn.Offset + 1);
            element = new EndOfLine(carriageReturn, lineFeed, context);
            return true;
        }

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.crLexer != null);
            Contract.Invariant(this.lfLexer != null);
        }
    }
}