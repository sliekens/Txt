// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public partial class ControlCharacterLexer : AlternativeLexer<ControlCharacter, ControlCharacter.Controls, ControlCharacter.Delete>
    {
        private readonly ILexer<ControlCharacter.Controls> element1Lexer;

        private readonly ILexer<ControlCharacter.Delete> element2Lexer;

        public ControlCharacterLexer(ILexer<ControlCharacter.Controls> element1Lexer, ILexer<ControlCharacter.Delete> element2Lexer)
            : base("CTL")
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
        }

        protected override bool TryRead1(ITextScanner scanner, out ControlCharacter.Controls element)
    {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out ControlCharacter.Delete element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }
    }

    public partial class ControlCharacterLexer
    {
        public class ControlsLexer : AlternativeLexer<ControlCharacter.Controls>
        {
            public ControlsLexer()
                : base('\0', '\x1F')
            {
            }
        }
    }

    public partial class ControlCharacterLexer
    {
        public class DeleteLexer : Lexer<ControlCharacter.Delete>
        {
            public override bool TryRead(ITextScanner scanner, out ControlCharacter.Delete element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, '\x7F', out terminal))
                {
                    element = default(ControlCharacter.Delete);
                    return false;
                }

                element = new ControlCharacter.Delete(terminal);
                return true;
            }
        }
    }
}