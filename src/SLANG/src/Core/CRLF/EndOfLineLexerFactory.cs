namespace SLANG.Core.CRLF
{
    using System;

    public class EndOfLineLexerFactory : ILexerFactory<EndOfLine>
    {
        private readonly ILexer<CarriageReturn> carriageReturnLexer;

        private readonly ILexer<LineFeed> lineFeedLexer; 
        private readonly ISequenceLexerFactory sequenceLexerFactory;

        public EndOfLineLexerFactory(ILexer<CarriageReturn> carriageReturnLexer, ILexer<LineFeed> lineFeedLexer, ISequenceLexerFactory sequenceLexerFactory)
        {
            if (carriageReturnLexer == null)
            {
                throw new ArgumentNullException("carriageReturnLexer", "Precondition: carriageReturnLexer != null");
            }

            if (lineFeedLexer == null)
            {
                throw new ArgumentNullException("lineFeedLexer", "Precondition: lineFeedLexer != null");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory", "Precondition: sequenceLexerFactory != null");
            }

            this.carriageReturnLexer = carriageReturnLexer;
            this.lineFeedLexer = lineFeedLexer;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        public ILexer<EndOfLine> Create()
        {
            var endOfLineSequenceLexer = this.sequenceLexerFactory.Create(this.carriageReturnLexer, this.lineFeedLexer);
            return new EndOfLineLexer(endOfLineSequenceLexer);
        }
    }
}
