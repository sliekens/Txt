// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpaceLexer.cs" company="Steven Liekens">
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

    public class LinearWhiteSpaceLexer : Lexer<LinearWhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Repetition> innerLexer;

        public LinearWhiteSpaceLexer([NotNull] ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<LinearWhiteSpace> Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner);
            if (!result.Success)
            {
                return ReadResult<LinearWhiteSpace>.FromError(
                    new SyntaxError
                    {
                        Message = "Expected 'LWSP'.",
                        RuleName = "LWSP",
                        Context = context,
                        InnerError = result.Error
                    });
            }
            return ReadResult<LinearWhiteSpace>.FromResult(new LinearWhiteSpace(result.Element));
        }
    }
}
