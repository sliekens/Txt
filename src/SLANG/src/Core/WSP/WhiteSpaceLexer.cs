// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.WSP
{
    using System;

    [RuleName("WSP")]
    public class WhiteSpaceLexer : Lexer<WhiteSpace>
    {
        private readonly ILexer<Alternative> whiteSpaceAlternativeLexer;

        public WhiteSpaceLexer(ILexer<Alternative> whiteSpaceAlternativeLexer)
        {
            if (whiteSpaceAlternativeLexer == null)
            {
                throw new ArgumentNullException("whiteSpaceAlternativeLexer", "Precondition: whiteSpaceAlternativeLexer != null");
            }

            this.whiteSpaceAlternativeLexer = whiteSpaceAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out WhiteSpace element)
        {
            Alternative result;
            if (this.whiteSpaceAlternativeLexer.TryRead(scanner, out result))
            {
                element = new WhiteSpace(result);
                return true;
            }

            element = default(WhiteSpace);
            return false;
        }
    }
}