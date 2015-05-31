namespace SLANG.Core.LWSP
{
    using System;

    public class LinearWhiteSpaceLexerFactory : ILexerFactory<LinearWhiteSpace>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexer<EndOfLine> endOfLineLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        public LinearWhiteSpaceLexerFactory(
            ILexer<WhiteSpace> whiteSpaceLexer,
            ILexer<EndOfLine> endOfLineLexer,
            ISequenceLexerFactory sequenceLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory)
        {
            if (whiteSpaceLexer == null)
            {
                throw new ArgumentNullException("whiteSpaceLexer", "Precondition: whiteSpaceLexer != null");
            }

            if (endOfLineLexer == null)
            {
                throw new ArgumentNullException("endOfLineLexer", "Precondition: endOfLineLexer != null");
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

            this.whiteSpaceLexer = whiteSpaceLexer;
            this.endOfLineLexer = endOfLineLexer;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        public ILexer<LinearWhiteSpace> Create()
        {
            var foldLexer = this.sequenceLexerFactory.Create(this.endOfLineLexer, this.whiteSpaceLexer);
            var breakingWhiteSpaceLexer = this.alternativeLexerFactory.Create(this.whiteSpaceLexer, foldLexer);
            var linearWhiteSpaceRepetitionLexer = this.repetitionLexerFactory.Create(breakingWhiteSpaceLexer, 0, int.MaxValue);
            return new LinearWhiteSpaceLexer(linearWhiteSpaceRepetitionLexer);
        }
    }
}