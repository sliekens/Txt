namespace SLANG.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="LinearWhiteSpaceLexer" /> class.</summary>
    public class LinearWhiteSpaceLexerFactory : ILexerFactory<LinearWhiteSpace>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<EndOfLine> endOfLineLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

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
                throw new ArgumentNullException("whiteSpaceLexerFactory");
            }

            if (endOfLineLexerFactory == null)
            {
                throw new ArgumentNullException("endOfLineLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
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