namespace SLANG.Core
{
    using System;

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
            var sp = this.spaceLexerFactory.Create();
            var htab = this.horizontalTabLexerFactory.Create();
            var innerLexer = this.alternativeLexerFactory.Create(sp, htab);
            return new WhiteSpaceLexer(innerLexer);
        }
    }
}