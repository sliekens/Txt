namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;

    /// <summary>Creates instances of the <see cref="WhiteSpaceLexer" /> class.</summary>
    public class WhiteSpaceLexerFactory : ILexerFactory<WhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<HorizontalTab> horizontalTabLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<Space> spaceLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spaceLexerFactory"></param>
        /// <param name="horizontalTabLexerFactory"></param>
        /// <param name="alternativeLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhiteSpaceLexerFactory(
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory,
            [NotNull] IAlternativeLexerFactory alternativeLexerFactory)
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
            var sp = spaceLexerFactory.Create();
            var htab = horizontalTabLexerFactory.Create();
            var innerLexer = alternativeLexerFactory.Create(sp, htab);
            return new WhiteSpaceLexer(innerLexer);
        }
    }
}
