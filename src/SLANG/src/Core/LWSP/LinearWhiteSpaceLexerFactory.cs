namespace SLANG.Core
{
    using System;

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
                throw new ArgumentNullException(
                    "whiteSpaceLexerFactory",
                    "Precondition: whiteSpaceLexerFactory != null");
            }

            if (endOfLineLexerFactory == null)
            {
                throw new ArgumentNullException("endOfLineLexerFactory", "Precondition: endOfLineLexerFactory != null");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory", "Precondition: sequenceLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "alternativeLexerFactory",
                    "Precondition: alternativeLexerFactory != null");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "repetitionLexerFactory",
                    "Precondition: repetitionLexerFactory != null");
            }

            this.whiteSpaceLexerFactory = whiteSpaceLexerFactory;
            this.endOfLineLexerFactory = endOfLineLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        public ILexer<LinearWhiteSpace> Create()
        {
            var endOfLineLexer = this.endOfLineLexerFactory.Create();
            var whiteSpaceLexer = this.whiteSpaceLexerFactory.Create();
            var foldLexer = this.sequenceLexerFactory.Create(endOfLineLexer, whiteSpaceLexer);
            var breakingWhiteSpaceLexer = this.alternativeLexerFactory.Create(whiteSpaceLexer, foldLexer);
            var linearWhiteSpaceRepetitionLexer = this.repetitionLexerFactory.Create(
                breakingWhiteSpaceLexer,
                0,
                int.MaxValue);
            return new LinearWhiteSpaceLexer(linearWhiteSpaceRepetitionLexer);
        }
    }
}