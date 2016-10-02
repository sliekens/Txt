using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    /// <summary>Creates instances of the <see cref="LineFeedLexer" /> class.</summary>
    public class LineFeedLexerFactory : LexerFactory<LineFeed>
    {
        private ILexer<LineFeed> instance;

        static LineFeedLexerFactory()
        {
            Default = new LineFeedLexerFactory(ABNF.TerminalLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public LineFeedLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
        }

        [NotNull]
        public static LineFeedLexerFactory Default { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<LineFeed> Create()
        {
            var innerLexer = TerminalLexerFactory.Create("\x0A", StringComparer.Ordinal);
            return new LineFeedLexer(innerLexer);
        }

        [NotNull]
        public LineFeedLexerFactory UseTerminalLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            return new LineFeedLexerFactory(terminalLexerFactory);
        }
    }
}
