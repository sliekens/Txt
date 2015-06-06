// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.DIGIT
{
    using System;

    [RuleName("DIGIT")]
    public class DigitLexer : Lexer<Digit>
    {
        private readonly ILexer<Terminal> digitValueRangeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digitValueRangeLexer">%x30-39</param>
        public DigitLexer(ILexer<Terminal> digitValueRangeLexer)
        {
            if (digitValueRangeLexer == null)
            {
                throw new ArgumentNullException("digitValueRangeLexer", "Precondition: digitValueRangeLexer != null");
            }

            this.digitValueRangeLexer = digitValueRangeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Digit element)
        {
            Terminal result;
            if (this.digitValueRangeLexer.TryRead(scanner, out result))
            {
                element = new Digit(result);
                return true;
            }

            element = default(Digit);
            return false;
        }
    }
}