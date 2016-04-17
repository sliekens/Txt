using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;

namespace Txt.ABNF.Core.WSP
{
    /// <summary>Creates instances of the <see cref="WhiteSpaceLexer" /> class.</summary>
    public class WhiteSpaceLexerFactory : ILexerFactory<WhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternationLexerFactory alternationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<HorizontalTab> horizontalTabLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<Space> spaceLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spaceLexerFactory"></param>
        /// <param name="horizontalTabLexerFactory"></param>
        /// <param name="alternationLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhiteSpaceLexerFactory(
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            this.spaceLexerFactory = spaceLexerFactory;
            this.horizontalTabLexerFactory = horizontalTabLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<WhiteSpace> Create()
        {
            var sp = spaceLexerFactory.Create();
            var htab = horizontalTabLexerFactory.Create();
            var innerLexer = alternationLexerFactory.Create(sp, htab);
            return new WhiteSpaceLexer(innerLexer);
        }
    }
}
