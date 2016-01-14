// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpaceLexer.cs" company="Steven Liekens">
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

    public class WhiteSpaceLexer : Lexer<WhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Alternative> innerLexer;

        public WhiteSpaceLexer([NotNull] ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<WhiteSpace> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<WhiteSpace>.FromError(
                    new SyntaxError
                    {
                        Message = "Expected 'WSP'.",
                        RuleName = "WSP",
                        Context = context,
                        InnerError = result.Error
                    });
            }
            var element = new WhiteSpace(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }
            return ReadResult<WhiteSpace>.FromResult(element);
        }
    }
}
