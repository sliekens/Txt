namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class LinearWhiteSpaceLexer : Lexer<LinearWhiteSpace>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<EndOfLine> crLfLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<WhiteSpace> wSpLexer;

        public LinearWhiteSpaceLexer()
            : this(new EndOfLineLexer(), new WhiteSpaceLexer())
        {
        }

        public LinearWhiteSpaceLexer(ILexer<EndOfLine> crLfLexer, ILexer<WhiteSpace> wSpLexer)
        {
            Contract.Requires(crLfLexer != null);
            Contract.Requires(wSpLexer != null);
            this.crLfLexer = crLfLexer;
            this.wSpLexer = wSpLexer;
        }

        /// <inheritdoc />
        public override LinearWhiteSpace Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            var data = new List<LinearWhiteSpace.CrLfWSpPair>();

            // The program should eventually exit this loop, unless the source data is an infinite stream of linear whitespace
            while (!scanner.EndOfInput)
            {
                EndOfLine endOfLine;
                WhiteSpace whiteSpace;
                if (this.wSpLexer.TryRead(scanner, out whiteSpace))
                {
                    data.Add(new LinearWhiteSpace.CrLfWSpPair(whiteSpace));
                }
                else if (this.crLfLexer.TryRead(scanner, out endOfLine))
                {
                    if (!scanner.EndOfInput && this.wSpLexer.TryRead(scanner, out whiteSpace))
                    {
                        Contract.Assume(whiteSpace.Offset == endOfLine.Offset + 2);
                        data.Add(new LinearWhiteSpace.CrLfWSpPair(endOfLine, whiteSpace));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(scanner, endOfLine);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return new LinearWhiteSpace(data, context);
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LinearWhiteSpace element)
        {
            var context = scanner.GetContext();
            var data = new List<LinearWhiteSpace.CrLfWSpPair>();

            // The program should eventually exit this loop, unless the source data is an infinite stream of linear whitespace
            while (!scanner.EndOfInput)
            {
                EndOfLine endOfLine;
                WhiteSpace whiteSpace;
                if (this.crLfLexer.TryRead(scanner, out endOfLine))
                {
                    if (!scanner.EndOfInput && this.wSpLexer.TryRead(scanner, out whiteSpace))
                    {
                        Contract.Assume(whiteSpace.Offset == endOfLine.Offset + 2);
                        data.Add(new LinearWhiteSpace.CrLfWSpPair(endOfLine, whiteSpace));
                    }
                    else
                    {
                        this.crLfLexer.PutBack(scanner, endOfLine);
                        break;
                    }
                }
                else if (this.wSpLexer.TryRead(scanner, out whiteSpace))
                {
                    data.Add(new LinearWhiteSpace.CrLfWSpPair(whiteSpace));
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
            Contract.Invariant(this.wSpLexer != null);
            Contract.Invariant(this.crLfLexer != null);
        }
    }
}