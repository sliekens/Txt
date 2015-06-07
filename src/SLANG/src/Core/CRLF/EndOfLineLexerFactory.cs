namespace SLANG.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="EndOfLineLexer" /> class.</summary>
    public class EndOfLineLexerFactory : ILexerFactory<EndOfLine>
    {
        private readonly ILexerFactory<CarriageReturn> carriageReturnLexerFactory;

        private readonly ILexerFactory<LineFeed> lineFeedLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        public EndOfLineLexerFactory(
            ILexerFactory<CarriageReturn> carriageReturnLexerFactory,
            ILexerFactory<LineFeed> lineFeedLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory)
        {
            if (carriageReturnLexerFactory == null)
            {
                throw new ArgumentNullException("carriageReturnLexerFactory");
            }

            if (lineFeedLexerFactory == null)
            {
                throw new ArgumentNullException("lineFeedLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            this.carriageReturnLexerFactory = carriageReturnLexerFactory;
            this.lineFeedLexerFactory = lineFeedLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<EndOfLine> Create()
        {
            var carriageReturnLexer = this.carriageReturnLexerFactory.Create();
            var lineFeedLexer = this.lineFeedLexerFactory.Create();
            var endOfLineSequenceLexer = this.sequenceLexerFactory.Create(carriageReturnLexer, lineFeedLexer);
            return new EndOfLineLexer(endOfLineSequenceLexer);
        }
    }
}