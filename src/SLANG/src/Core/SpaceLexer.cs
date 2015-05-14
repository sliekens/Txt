// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class SpaceLexer : Lexer<Space>
    {
        /// <summary>Initializes a new instance of the <see cref="SpaceLexer"/> class.</summary>
        public SpaceLexer()
            : base("SP")
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