// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class SpaceLexer : Lexer<Space>
    {
        /// <summary>Initializes a new instance of the <see cref="SpaceLexer"/> class.</summary>
        /// <param name="serviceLocator">The object that retrieves instances of <see cref="ILexer{TElement}"/> by type and optional rule name.</param>
        public SpaceLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "SP")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Space element)
        {
            Element space;
            if (!TryReadTerminal(scanner, '\x20', out space))
            {
                element = default(Space);
                return false;
            }

            element = new Space(space);
            return true;
        }
    }
}