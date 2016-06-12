// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigitLexer : Lexer<HexadecimalDigit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Alternation> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">DIGIT / "A" / "B" / "C" / "D" / "E" / "F"</param>
        public HexadecimalDigitLexer([NotNull] ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        protected override ReadResult<HexadecimalDigit> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HexadecimalDigit>.FromResult(new HexadecimalDigit(result.Element));
            }
            return ReadResult<HexadecimalDigit>.FromSyntaxError(SyntaxError.FromReadResult(result, context));
        }
    }
}
