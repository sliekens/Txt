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
        private readonly ILexer visibleCharacterValueRangeLexer;

        /// <summary>Initializes a new instance of the <see cref="VisibleCharacterLexer"/> class.</summary>
        public VisibleCharacterLexer(ILexer visibleCharacterValueRangeLexer)
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
            Element value;
            if (this.visibleCharacterValueRangeLexer.TryReadElement(scanner, out value))
            {
                element = new VisibleCharacter(value);
                return true;
            }

            element = default(VisibleCharacter);
            return false;
        }
    }
}