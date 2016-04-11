using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.WSP;

namespace Txt.ABNF.Core.LWSP
{
    /// <summary>Creates instances of the <see cref="LinearWhiteSpaceLexer" /> class.</summary>
    public class LinearWhiteSpaceLexerFactory : ILexerFactory<LinearWhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<EndOfLine> endOfLineLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<WhiteSpace> whiteSpaceLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whiteSpaceLexerFactory"></param>
        /// <param name="endOfLineLexerFactory"></param>
        /// <param name="concatenationLexerFactory"></param>
        /// <param name="alternativeLexerFactory"></param>
        /// <param name="repetitionLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public LinearWhiteSpaceLexerFactory(
            [NotNull] ILexerFactory<WhiteSpace> whiteSpaceLexerFactory,
            [NotNull] ILexerFactory<EndOfLine> endOfLineLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IAlternativeLexerFactory alternativeLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory)
        {
            if (whiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexerFactory));
            }
            if (endOfLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(endOfLineLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            this.whiteSpaceLexerFactory = whiteSpaceLexerFactory;
            this.endOfLineLexerFactory = endOfLineLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<LinearWhiteSpace> Create()
        {
            var endOfLineLexer = endOfLineLexerFactory.Create();
            var whiteSpaceLexer = whiteSpaceLexerFactory.Create();
            var foldLexer = concatenationLexerFactory.Create(endOfLineLexer, whiteSpaceLexer);
            var breakingWhiteSpaceLexer = alternativeLexerFactory.Create(whiteSpaceLexer, foldLexer);
            var innerLexer = repetitionLexerFactory.Create(breakingWhiteSpaceLexer, 0, int.MaxValue);
            return new LinearWhiteSpaceLexer(innerLexer);
        }
    }
}
