namespace SLANG.Core.WSP
{
    using System;

    public class WhiteSpaceLexerFactory : ILexerFactory<WhiteSpace>
    {
        private readonly ILexer<Space> spaceLexer;

        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public WhiteSpaceLexerFactory(ILexer<Space> spaceLexer, ILexer<HorizontalTab> horizontalTabLexer, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (spaceLexer == null)
            {
                throw new ArgumentNullException("spaceLexer", "Precondition: spaceLexer != null");
            }

            if (horizontalTabLexer == null)
            {
                throw new ArgumentNullException("horizontalTabLexer", "Precondition: horizontalTabLexer != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            this.spaceLexer = spaceLexer;
            this.horizontalTabLexer = horizontalTabLexer;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<WhiteSpace> Create()
        {
            var whiteSpaceAlternativeLexer = this.alternativeLexerFactory.Create(this.spaceLexer, this.horizontalTabLexer);
            return new WhiteSpaceLexer(whiteSpaceAlternativeLexer);
        }
    }
}
