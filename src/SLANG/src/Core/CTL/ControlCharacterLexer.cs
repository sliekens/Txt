// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.CTL
{
    using System;

    [RuleName("CTL")]
    public class ControlCharacterLexer : Lexer<ControlCharacter>
    {
        private readonly ILexer<Alternative> controlCharacterAlternativeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlCharacterAlternativeLexer">%x00-1F / %x7F</param>
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
            Alternative result;
            if (this.controlCharacterAlternativeLexer.TryRead(scanner, out result))
            {
                element = new ControlCharacter(result);
                return true;
            }

            element = default(ControlCharacter);
            return false;
        }
    }
}