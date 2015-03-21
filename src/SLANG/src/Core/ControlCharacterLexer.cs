// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public partial class ControlCharacterLexer : AlternativeLexer<ControlCharacter, ControlCharacter.Controls, Element>
    {
        private readonly ILexer<ControlCharacter.Controls> element1Lexer;

        private readonly ILexer<Element> element2Lexer;

        public ControlCharacterLexer()
            : this(new ControlsLexer(), new DeleteLexer())
        {
        }

        public ControlCharacterLexer(ILexer<ControlCharacter.Controls> element1Lexer, ILexer<Element> element2Lexer)
            : base("CTL")
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
        }

        protected override ControlCharacter CreateInstance1(ControlCharacter.Controls element, ITextContext context)
        {
            return new ControlCharacter(element, context);
        }

        protected override ControlCharacter CreateInstance2(Element element, ITextContext context)
        {
            return new ControlCharacter(element, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out ControlCharacter.Controls element)
        {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Element element)
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

            protected override ControlCharacter.Controls CreateInstance(char element, ITextContext context)
            {
                return new ControlCharacter.Controls(element, context);
            }
        }
    }

    public partial class ControlCharacterLexer
    {
        public class DeleteLexer : Lexer<Element>
        {
            public override bool TryRead(ITextScanner scanner, out Element element)
            {
                return TryReadTerminal(scanner, '\x7F', out element);
            }
        }
    }
}