// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.SP
{
    using System;

    public class SpaceLexer : Lexer<Space>
    {
        private readonly ILexer<Element> spaceTerminalLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spaceTerminalLexer">%x20</param>
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
}