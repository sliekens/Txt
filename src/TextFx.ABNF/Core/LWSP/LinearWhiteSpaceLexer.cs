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

    public class LinearWhiteSpaceLexer : Lexer<LinearWhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Repetition> innerLexer;

        public LinearWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<LinearWhiteSpace> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<LinearWhiteSpace>.FromError(new SyntaxError
                {
                    Message = "Expected 'LWSP'.",
                    RuleName = "LWSP",
                    Context = context,
                    InnerError = result.Error
                });
            }

            var element = new LinearWhiteSpace(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return ReadResult<LinearWhiteSpace>.FromResult(element);
        }
    }
}