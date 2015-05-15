// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public partial class ControlCharacterLexer : AlternativeLexer<ControlCharacter, ControlCharacter.Controls, ControlCharacter.Delete>
    {
        public ControlCharacterLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "CTL")
        {
        }
    }

    public partial class ControlCharacterLexer
    {
        public class ControlsLexer : AlternativeLexer<ControlCharacter.Controls>
        {
            public ControlsLexer(IServiceLocator serviceLocator)
                : base(serviceLocator, '\0', '\x1F')
            {
            }
        }
    }

    public partial class ControlCharacterLexer
    {
        public class DeleteLexer : Lexer<Element>
        {
            public DeleteLexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out Element element)
            {
                return TryReadTerminal(scanner, '\x7F', out element);
            }
        }
    }
}