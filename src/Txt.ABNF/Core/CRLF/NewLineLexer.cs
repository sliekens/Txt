// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewLineLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using Jetbrains.Annotations;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLineLexer : Lexer<NewLine>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Concatenation> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">CR LF</param>
        public NewLineLexer([NotNull] ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<NewLine> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<NewLine>.FromResult(new NewLine(result.Element));
            }
            return ReadResult<NewLine>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
