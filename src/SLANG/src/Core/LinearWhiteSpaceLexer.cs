// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class LinearWhiteSpaceLexer : RepetitionLexer<LinearWhiteSpace, Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<EndOfLine> endOfLineLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        public LinearWhiteSpaceLexer()
            : this(new EndOfLineLexer(), new WhiteSpaceLexer())
        {
        }

        public LinearWhiteSpaceLexer(ILexer<EndOfLine> endOfLineLexer, ILexer<WhiteSpace> whiteSpaceLexer)
            : base("LWSP", 0, int.MaxValue)
        {
            Contract.Requires(endOfLineLexer != null);
            Contract.Requires(whiteSpaceLexer != null);
            this.endOfLineLexer = endOfLineLexer;
            this.whiteSpaceLexer = whiteSpaceLexer;
        }

        protected override LinearWhiteSpace CreateInstance(IList<Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>> elements, int lowerBound, int upperBound, ITextContext context)
        {
            return new LinearWhiteSpace(elements, context);
        }

        protected override bool TryRead(ITextScanner scanner, out Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>);
                return false;
            }

            var context = scanner.GetContext();
            WhiteSpace alternative1;
            if (this.whiteSpaceLexer.TryRead(scanner, out alternative1))
            {
                element = new Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>(alternative1, context);
                return true;
            }

            Sequence<EndOfLine, WhiteSpace> alternative2;
            if (this.TryReadEndOfLineWhiteSpaceSequence(scanner, out alternative2))
            {
                element = new Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>(alternative2, context);
                return true;
            }

            element = default(Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>);
            return false;
        }

        private bool TryReadEndOfLineWhiteSpaceSequence(ITextScanner scanner, out Sequence<EndOfLine, WhiteSpace> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<EndOfLine, WhiteSpace>);
                return false;
            }

            var context = scanner.GetContext();
            EndOfLine element1;
            if (!this.endOfLineLexer.TryRead(scanner, out element1))
            {
                element = default(Sequence<EndOfLine, WhiteSpace>);
                return false;
            }

            WhiteSpace element2;
            if (!this.whiteSpaceLexer.TryRead(scanner, out element2))
            {
                scanner.PutBack(element1.Data);
                element = default(Sequence<EndOfLine, WhiteSpace>);
                return false;
            }

            element = new Sequence<EndOfLine, WhiteSpace>(element1, element2, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.whiteSpaceLexer != null);
            Contract.Invariant(this.endOfLineLexer != null);
        }
    }
}