// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class VisibleCharacterLexer : Lexer<VisibleCharacter>
    {
        private readonly ILexer<Element> visibleCharacterValueRangeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibleCharacterValueRangeLexer">%x21-7E</param>
        public VisibleCharacterLexer(ILexer<Element> visibleCharacterValueRangeLexer)
            : base("VCHAR")
        {
            if (visibleCharacterValueRangeLexer == null)
            {
                throw new ArgumentNullException("visibleCharacterValueRangeLexer", "Precondition: visibleCharacterValueRangeLexer != null");
            }

            this.visibleCharacterValueRangeLexer = visibleCharacterValueRangeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out VisibleCharacter element)
        {
            Element result;
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