using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.WSP;
using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    /// <summary>Creates instances of the <see cref="LinearWhiteSpaceLexer" /> class.</summary>
    public class LinearWhiteSpaceLexerFactory : ILexerFactory<LinearWhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternationLexerFactory alternationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<NewLine> newLineLexer;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        /// <summary>
        /// </summary>
        /// <param name="alternationLexerFactory"></param>
        /// <param name="concatenationLexerFactory"></param>
        /// <param name="repetitionLexerFactory"></param>
        /// <param name="whiteSpaceLexer"></param>
        /// <param name="newLineLexer"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public LinearWhiteSpaceLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<WhiteSpace> whiteSpaceLexer,
            [NotNull] ILexer<NewLine> newLineLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (whiteSpaceLexer == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexer));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.whiteSpaceLexer = whiteSpaceLexer;
            this.newLineLexer = newLineLexer;
        }

        /// <inheritdoc />
        public ILexer<LinearWhiteSpace> Create()
        {
            var foldLexer = concatenationLexerFactory.Create(newLineLexer, whiteSpaceLexer);
            var breakingWhiteSpaceLexer = alternationLexerFactory.Create(whiteSpaceLexer, foldLexer);
            var innerLexer = repetitionLexerFactory.Create(breakingWhiteSpaceLexer, 0, int.MaxValue);
            return new LinearWhiteSpaceLexer(innerLexer);
        }
    }
}
