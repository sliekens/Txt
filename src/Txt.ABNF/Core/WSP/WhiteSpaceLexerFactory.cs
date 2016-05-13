using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    /// <summary>Creates instances of the <see cref="WhiteSpaceLexer" /> class.</summary>
    public class WhiteSpaceLexerFactory : ILexerFactory<WhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternationLexerFactory alternationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Space> spaceLexer;

        /// <summary>
        /// </summary>
        /// <param name="alternationLexerFactory"></param>
        /// <param name="spaceLexer"></param>
        /// <param name="horizontalTabLexer"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhiteSpaceLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<Space> spaceLexer,
            [NotNull] ILexer<HorizontalTab> horizontalTabLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (spaceLexer == null)
            {
                throw new ArgumentNullException(nameof(spaceLexer));
            }
            if (horizontalTabLexer == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.spaceLexer = spaceLexer;
            this.horizontalTabLexer = horizontalTabLexer;
        }

        /// <inheritdoc />
        public ILexer<WhiteSpace> Create()
        {
            var innerLexer = alternationLexerFactory.Create(spaceLexer, horizontalTabLexer);
            return new WhiteSpaceLexer(innerLexer);
        }
    }
}
