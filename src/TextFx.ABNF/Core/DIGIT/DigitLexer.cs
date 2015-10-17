// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;

    [RuleName("DIGIT")]
    public class DigitLexer : Lexer<Digit>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x30-39</param>
        public DigitLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Digit> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Digit>.FromError(new SyntaxError
                {
                    Message = "Expected 'DIGIT'.",
                    RuleName = "DIGIT",
                    Context = context,
                    InnerError = result.Error
                });
            }

            var element = new Digit(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return ReadResult<Digit>.FromResult(element);
        }
    }
}