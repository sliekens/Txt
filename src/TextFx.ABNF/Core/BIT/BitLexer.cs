// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;

    [RuleName("BIT")]
    public class BitLexer : Lexer<Bit>
    {
        private readonly ILexer<Alternative> innerLexer;

        public BitLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Bit> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Bit>.FromError(new SyntaxError
                {
                    Message = "Expected 'BIT'.",
                    RuleName = "BIT",
                    Context = context,
                    InnerError = result.Error
                });
            }

            var element = new Bit(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return ReadResult<Bit>.FromResult(element);
        }
    }
}