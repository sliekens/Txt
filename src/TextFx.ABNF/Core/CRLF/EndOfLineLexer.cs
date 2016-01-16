// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLineLexer.cs" company="Steven Liekens">
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

    public class EndOfLineLexer : Lexer<EndOfLine>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Concatenation> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">CR LF</param>
        public EndOfLineLexer([NotNull] ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<EndOfLine> Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner);
            if (!result.Success)
            {
                return ReadResult<EndOfLine>.FromError(
                    new SyntaxError
                    {
                        Message = "Expected 'CRLF'.",
                        RuleName = "CRLF",
                        Context = context,
                        InnerError = result.Error
                    });
            }
            return ReadResult<EndOfLine>.FromResult(new EndOfLine(result.Element));
        }
    }
}
