// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleQuoteLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;

    public class DoubleQuoteLexer : Lexer<DoubleQuote>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x22</param>
        public DoubleQuoteLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<DoubleQuote> Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<DoubleQuote>.FromResult(new DoubleQuote(result.Element));
            }
            return ReadResult<DoubleQuote>.FromSyntaxError(SyntaxError.FromReadResult(result, context));
        }
    }
}
