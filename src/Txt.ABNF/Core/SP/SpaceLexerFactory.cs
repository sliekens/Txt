﻿using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    /// <summary>Creates instances of the <see cref="SpaceLexer" /> class.</summary>
    public class SpaceLexerFactory : ILexerFactory<Space>
    {
        private ILexer<Space> instance;

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
        public ILexer<Space> Create()
        {
            var innerLexer = TerminalLexerFactory.Create("\x20", StringComparer.Ordinal);
            return new SpaceLexer(innerLexer);
        }

        public ILexer<Space> CreateOnce()
        {
            return instance ?? (instance = Create());
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
