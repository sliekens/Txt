// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturnLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    public class CarriageReturnLexer : Lexer<CarriageReturn>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x0D</param>
        public CarriageReturnLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<CarriageReturn> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<CarriageReturn>.FromError(new SyntaxError
                {
                    Message = "Expected 'CR'.",
                    RuleName = "CR",
                    Context = context,
                    InnerError = result.Error
                });
            }

            var element = new CarriageReturn(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return ReadResult<CarriageReturn>.FromResult(element);
        }
    }
}