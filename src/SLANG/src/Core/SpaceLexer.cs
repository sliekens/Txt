// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public partial class SpaceLexer : Lexer<Space>
    {
        private readonly ILexer<Element> spaceTerminalLexer;

        public SpaceLexer(ILexer<Element> spaceTerminalLexer)
            : base("SP")
        {
            if (spaceTerminalLexer == null)
            {
                throw new ArgumentNullException("spaceTerminalLexer", "Precondition: spaceTerminalLexer != null");
            }

            this.spaceTerminalLexer = spaceTerminalLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Space element)
        {
            Element terminal;
            if (this.spaceTerminalLexer.TryRead(scanner, out terminal))
            {
                element = new Space(terminal);
                return true;
            }

            element = default(Space);
            return false;
        }
    }

    public partial class SpaceLexer
    {
        public class SpaceTerminalLexer : TerminalsLexer
        {
            public SpaceTerminalLexer()
                : base('\x20')
            {
            }
        }
    }
}