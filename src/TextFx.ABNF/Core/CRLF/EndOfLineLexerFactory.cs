namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="EndOfLineLexer" /> class.</summary>
    public class EndOfLineLexerFactory : ILexerFactory<EndOfLine>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<CarriageReturn> carriageReturnLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<LineFeed> lineFeedLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        public EndOfLineLexerFactory(
            ILexerFactory<CarriageReturn> carriageReturnLexerFactory,
            ILexerFactory<LineFeed> lineFeedLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory)
        {
            if (carriageReturnLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(carriageReturnLexerFactory));
            }

            if (lineFeedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(lineFeedLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            this.carriageReturnLexerFactory = carriageReturnLexerFactory;
            this.lineFeedLexerFactory = lineFeedLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<EndOfLine> Create()
        {
            var carriageReturnLexer = this.carriageReturnLexerFactory.Create();
            var lineFeedLexer = this.lineFeedLexerFactory.Create();
            var endOfLineSequenceLexer = this.concatenationLexerFactory.Create(carriageReturnLexer, lineFeedLexer);
            return new EndOfLineLexer(endOfLineSequenceLexer);
        }
    }
}