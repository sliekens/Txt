using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    /// <summary>Creates instances of the <see cref="DoubleQuoteLexer" /> class.</summary>
    public class DoubleQuoteLexerFactory : ILexerFactory<DoubleQuote>
    {
        private ILexer<DoubleQuote> instance;

        static DoubleQuoteLexerFactory()
        {
            Default = new DoubleQuoteLexerFactory(ABNF.TerminalLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DoubleQuoteLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
        }

        [NotNull]
        public static DoubleQuoteLexerFactory Default { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        /// <inheritdoc />
        public ILexer<DoubleQuote> Create()
        {
            var innerLexer = TerminalLexerFactory.Create("\x22", StringComparer.Ordinal);
            return new DoubleQuoteLexer(innerLexer);
        }

        public ILexer<DoubleQuote> CreateOnce()
        {
            return instance ?? (instance = Create());
        }

        [NotNull]
        public DoubleQuoteLexerFactory UseTerminalLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            return new DoubleQuoteLexerFactory(terminalLexerFactory);
        }
    }
}
