namespace TextFx.ABNF.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="WhiteSpaceLexer" /> class.</summary>
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
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }

            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            this.spaceLexerFactory = spaceLexerFactory;
            this.horizontalTabLexerFactory = horizontalTabLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<WhiteSpace> Create()
        {
            var sp = this.spaceLexerFactory.Create();
            var htab = this.horizontalTabLexerFactory.Create();
            var innerLexer = this.alternativeLexerFactory.Create(sp, htab);
            return new WhiteSpaceLexer(innerLexer);
        }
    }
}