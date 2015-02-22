// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    public class LinearWhiteSpaceLexer : Lexer<LinearWhiteSpace>
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<EndOfLine> crLfLexer;

        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<WhiteSpace> wSpLexer;

        /// <summary>Initializes a new instance of the <see cref="LinearWhiteSpaceLexer"/> class.</summary>
        public LinearWhiteSpaceLexer()
            : this(new EndOfLineLexer(), new WhiteSpaceLexer())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="LinearWhiteSpaceLexer"/> class.</summary>
        /// <param name="crLfLexer">TODO The cr lf lexer.</param>
        /// <param name="wSpLexer">TODO The w sp lexer.</param>
        public LinearWhiteSpaceLexer(ILexer<EndOfLine> crLfLexer, ILexer<WhiteSpace> wSpLexer)
            : base("LWSP")
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

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.wSpLexer != null);
            Contract.Invariant(this.crLfLexer != null);
        }
    }
}