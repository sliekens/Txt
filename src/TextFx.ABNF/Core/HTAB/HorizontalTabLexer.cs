// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalTabLexer.cs" company="Steven Liekens">
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

    public class HorizontalTabLexer : Lexer<HorizontalTab>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x09</param>
        public HorizontalTabLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<HorizontalTab> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<HorizontalTab>.FromError(
                    new SyntaxError
                    {
                        Message = "Expected 'HTAB'.",
                        RuleName = "HTAB",
                        Context = context,
                        InnerError = result.Error
                    });
            }
            var element = new HorizontalTab(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }
            return ReadResult<HorizontalTab>.FromResult(element);
        }
    }
}
