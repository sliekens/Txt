// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public class DigitLexer : Lexer<Digit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x30-39</param>
        public DigitLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        protected override ReadResult<Digit> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return new ReadResult<Digit>(new Digit(result.Element));
            }
            return new ReadResult<Digit>(SyntaxError.FromReadResult(result, context));
        }
    }
}
