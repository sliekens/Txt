using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    /// <summary>Creates instances of the <see cref="SpaceLexer" /> class.</summary>
    public class SpaceLexerFactory : LexerFactory<Space>
    {
        static SpaceLexerFactory()
        {
            Default = new SpaceLexerFactory(ABNF.TerminalLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SpaceLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
        }

        [NotNull]
        public static SpaceLexerFactory Default { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<Space> Create()
        {
            var innerLexer = TerminalLexerFactory.Create("\x20", StringComparer.Ordinal);
            return new SpaceLexer(innerLexer);
        }

        [NotNull]
        public SpaceLexerFactory UseTerminalLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            return new SpaceLexerFactory(terminalLexerFactory);
        }
    }
}
