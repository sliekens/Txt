using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.LF;

namespace Txt.ABNF.Core.CRLF
{
    /// <summary>Creates instances of the <see cref="EndOfLineLexer" /> class.</summary>
    public class EndOfLineLexerFactory : ILexerFactory<EndOfLine>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<CarriageReturn> carriageReturnLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<LineFeed> lineFeedLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carriageReturnLexerFactory"></param>
        /// <param name="lineFeedLexerFactory"></param>
        /// <param name="concatenationLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EndOfLineLexerFactory(
            [NotNull] ILexerFactory<CarriageReturn> carriageReturnLexerFactory,
            [NotNull] ILexerFactory<LineFeed> lineFeedLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory)
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
            var carriageReturnLexer = carriageReturnLexerFactory.Create();
            var lineFeedLexer = lineFeedLexerFactory.Create();
            var innerLexer = concatenationLexerFactory.Create(carriageReturnLexer, lineFeedLexer);
            return new EndOfLineLexer(innerLexer);
        }
    }
}
