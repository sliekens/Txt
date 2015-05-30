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
        private readonly ILexer<ControlCharacter.Controls> controlsControlCharacterLexer;

        private readonly ILexer<ControlCharacter.Delete> element2Lexer;

        public ControlCharacterLexer(ILexer<ControlCharacter.Controls> controlsControlCharacterLexer, ILexer<ControlCharacter.Delete> element2Lexer)
            : base("CTL")
        {
            this.controlsControlCharacterLexer = controlsControlCharacterLexer;
            this.element2Lexer = element2Lexer;
        }

        protected override bool TryRead1(ITextScanner scanner, out ControlCharacter.Controls element)
        {
            return this.controlsControlCharacterLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out ControlCharacter.Delete element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }
    }

    public partial class ControlCharacterLexer
    {
        public class ControlsLexer : ValueRangeLexer
        {
            public ControlsLexer()
                : base('\x00', '\x1F')
            {
            }
        }
    }

    public partial class ControlCharacterLexer
    {
        public class DeleteLexer : TerminalsLexer
        {
            public DeleteLexer()
                : base('\x7F')
            {
            }
        }
    }
}