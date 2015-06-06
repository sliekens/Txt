// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLineLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.CRLF
{
    using System;

    public class EndOfLineLexer : Lexer<EndOfLine>
    {
        private readonly ILexer<Sequence> endOfLineSequenceLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endOfLineSequenceLexer">CR LF</param>
        public EndOfLineLexer(ILexer<Sequence> endOfLineSequenceLexer)
        {
            if (endOfLineSequenceLexer == null)
            {
                throw new ArgumentNullException("endOfLineSequenceLexer", "Precondition: endOfLineSequenceLexer != null");
            }

            this.endOfLineSequenceLexer = endOfLineSequenceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out EndOfLine element)
        {
            Sequence result;
            if (this.endOfLineSequenceLexer.TryRead(scanner, out result))
            {
                element = new EndOfLine(result);
                return true;
            }

            element = default(EndOfLine);
            return false;
        }
    }
}