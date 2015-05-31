// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class ControlCharacterLexer : Lexer<ControlCharacter>
    {
        private readonly ILexer<Alternative> controlCharacterAlternativeLexer;

        public ControlCharacterLexer(ILexer<Alternative> controlCharacterAlternativeLexer)
        {
            if (controlCharacterAlternativeLexer == null)
            {
                throw new ArgumentNullException("controlCharacterAlternativeLexer", "Precondition: controlCharacterAlternativeLexer != null");
            }

            this.controlCharacterAlternativeLexer = controlCharacterAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out ControlCharacter element)
        {
            Element terminal;
            if (this.controlCharacterAlternativeLexer.TryReadElement(scanner, out terminal))
            {
                element = new ControlCharacter(terminal);
                return true;
            }

            element = default(ControlCharacter);
            return false;
        }
    }
}