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

    [RuleName("CR")]
    public class CarriageReturnLexer : Lexer<CarriageReturn>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x0D</param>
        public CarriageReturnLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out CarriageReturn element)
        {
            Terminal result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new CarriageReturn(result);
                if (previousElementOrNull != null)
                {
                    element.PreviousElement = previousElementOrNull;
                    previousElementOrNull.NextElement = element;
                }

                return true;
            }

            element = default(CarriageReturn);
            return false;
        }
    }
}