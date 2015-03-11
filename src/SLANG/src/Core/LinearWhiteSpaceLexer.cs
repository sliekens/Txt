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

    public class LinearWhiteSpaceLexer : Lexer<LinearWhiteSpace>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<EndOfLine> endOfLineLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        /// <summary>Initializes a new instance of the <see cref="LinearWhiteSpaceLexer"/> class.</summary>
        public LinearWhiteSpaceLexer()
            : this(new EndOfLineLexer(), new WhiteSpaceLexer())
        {
        }

        public LinearWhiteSpaceLexer(ILexer<EndOfLine> endOfLineLexer, ILexer<WhiteSpace> whiteSpaceLexer)
            : base("LWSP")
        {
            Contract.Requires(endOfLineLexer != null);
            Contract.Requires(whiteSpaceLexer != null);
            this.endOfLineLexer = endOfLineLexer;
            this.whiteSpaceLexer = whiteSpaceLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LinearWhiteSpace element)
        {
            var context = scanner.GetContext();
            var data = new List<Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>>();

            // The program should eventually exit this loop, unless the source data is an infinite stream of linear whitespace
            while (!scanner.EndOfInput)
            {
                var innerContext = scanner.GetContext();
                EndOfLine endOfLine;
                WhiteSpace whiteSpace;
                if (this.endOfLineLexer.TryRead(scanner, out endOfLine))
                {
                    if (this.whiteSpaceLexer.TryRead(scanner, out whiteSpace))
                    {
                        var sequence = new Sequence<EndOfLine, WhiteSpace>(endOfLine, whiteSpace, innerContext);
                        var alternative = new Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>(sequence, innerContext);
                        data.Add(alternative);
                    }
                    else
                    {
                        scanner.PutBack(endOfLine.Data);
                        break;
                    }
                }
                else if (this.whiteSpaceLexer.TryRead(scanner, out whiteSpace))
                {
                    data.Add(new Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>(whiteSpace, innerContext));
                }
                else
                {
                    break;
                }
            }

            element = new LinearWhiteSpace(data, context);
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