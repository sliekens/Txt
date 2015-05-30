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

    public class EndOfLineLexer : Lexer<EndOfLine>
    {
        private readonly ILexer<Sequence> endOfLineSequenceLexer;

        public EndOfLineLexer(ILexer<Sequence> endOfLineSequenceLexer)
            : base("CRLF")
        {
            if (endOfLineSequenceLexer == null)
            {
                throw new ArgumentNullException("endOfLineSequenceLexer", "Precondition: endOfLineSequenceLexer != null");
            }

            this.endOfLineSequenceLexer = endOfLineSequenceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out EndOfLine element)
        {
            Sequence sequence;
            if (this.endOfLineSequenceLexer.TryRead(scanner, out sequence))
            {
                element = new EndOfLine(sequence);
                return true;
            }

            element = default(EndOfLine);
            return false;
        }
    }
}