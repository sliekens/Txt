// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core.VCHAR
{
    using System;

    [RuleName("VCHAR")]
    public class VisibleCharacterLexer : Lexer<VisibleCharacter>
    {
        private readonly ILexer<Terminal> visibleCharacterValueRangeLexer;

        /// <summary>
        /// </summary>
        /// <param name="visibleCharacterValueRangeLexer">%x21-7E</param>
        public VisibleCharacterLexer(ILexer<Terminal> visibleCharacterValueRangeLexer)
        {
            if (visibleCharacterValueRangeLexer == null)
            {
                throw new ArgumentNullException(
                    "visibleCharacterValueRangeLexer",
                    "Precondition: visibleCharacterValueRangeLexer != null");
            }

            this.visibleCharacterValueRangeLexer = visibleCharacterValueRangeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out VisibleCharacter element)
        {
            Terminal result;
            if (this.visibleCharacterValueRangeLexer.TryRead(scanner, out result))
            {
                element = new VisibleCharacter(result);
                return true;
            }

            element = default(VisibleCharacter);
            return false;
        }
    }
}