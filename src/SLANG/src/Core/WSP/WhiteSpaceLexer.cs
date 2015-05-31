// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class WhiteSpaceLexer : Lexer<WhiteSpace>
    {
        private readonly ILexer<Alternative> whiteSpaceAlternativeLexer;

        public WhiteSpaceLexer(ILexer<Alternative> whiteSpaceAlternativeLexer)
            : base("WSP")
        {
            if (whiteSpaceAlternativeLexer == null)
            {
                throw new ArgumentNullException("whiteSpaceAlternativeLexer", "Precondition: whiteSpaceAlternativeLexer != null");
            }

            this.whiteSpaceAlternativeLexer = whiteSpaceAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out WhiteSpace element)
        {
            Element result;
            if (this.whiteSpaceAlternativeLexer.TryReadElement(scanner, out result))
            {
                element = new WhiteSpace(result);
                return true;
            }

            element = default(WhiteSpace);
            return false;
        }
    }
}