namespace SLANG.Core.WSP
{
    using System;

    using SLANG.Core.HTAB;
    using SLANG.Core.SP;

    public class WhiteSpaceLexerFactory : ILexerFactory<WhiteSpace>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<HorizontalTab> horizontalTabLexerFactory;

        private readonly ILexerFactory<Space> spaceLexerFactory;

        public WhiteSpaceLexerFactory(
            ILexerFactory<Space> spaceLexerFactory,
            ILexerFactory<HorizontalTab> horizontalTabLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException("spaceLexerFactory", "Precondition: spaceLexerFactory != null");
            }

            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "horizontalTabLexerFactory",
                    "Precondition: horizontalTabLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "alternativeLexerFactory",
                    "Precondition: alternativeLexerFactory != null");
            }

            this.spaceLexerFactory = spaceLexerFactory;
            this.horizontalTabLexerFactory = horizontalTabLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<WhiteSpace> Create()
        {
            var spaceLexer = this.spaceLexerFactory.Create();
            var horizontalTabLexer = this.horizontalTabLexerFactory.Create();
            var whiteSpaceAlternativeLexer = this.alternativeLexerFactory.Create(spaceLexer, horizontalTabLexer);
            return new WhiteSpaceLexer(whiteSpaceAlternativeLexer);
        }
    }
}