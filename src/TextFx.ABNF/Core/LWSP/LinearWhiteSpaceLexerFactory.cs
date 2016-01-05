namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="LinearWhiteSpaceLexer" /> class.</summary>
    public class LinearWhiteSpaceLexerFactory : ILexerFactory<LinearWhiteSpace>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<EndOfLine> endOfLineLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ISequenceLexerFactory sequenceLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<WhiteSpace> whiteSpaceLexerFactory;

        public LinearWhiteSpaceLexerFactory(
            ILexerFactory<WhiteSpace> whiteSpaceLexerFactory,
            ILexerFactory<EndOfLine> endOfLineLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory)
        {
            if (whiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexerFactory));
            }

            if (endOfLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(endOfLineLexerFactory));
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(sequenceLexerFactory));
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
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<LinearWhiteSpace> Create()
        {
            var endOfLineLexer = this.endOfLineLexerFactory.Create();
            var whiteSpaceLexer = this.whiteSpaceLexerFactory.Create();
            var foldLexer = this.sequenceLexerFactory.Create(endOfLineLexer, whiteSpaceLexer);
            var breakingWhiteSpaceLexer = this.alternativeLexerFactory.Create(whiteSpaceLexer, foldLexer);
            var innerLexer = this.repetitionLexerFactory.Create(breakingWhiteSpaceLexer, 0, int.MaxValue);
            return new LinearWhiteSpaceLexer(innerLexer);
        }
    }
}