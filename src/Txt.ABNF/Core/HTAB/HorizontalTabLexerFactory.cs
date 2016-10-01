﻿using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    /// <summary>Creates instances of the <see cref="HorizontalTabLexer" /> class.</summary>
    public class HorizontalTabLexerFactory : ILexerFactory<HorizontalTab>
    {
        private ILexer<HorizontalTab> instance;

        static HorizontalTabLexerFactory()
        {
            Default = new HorizontalTabLexerFactory(ABNF.TerminalLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HorizontalTabLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
        }

        [NotNull]
        public static HorizontalTabLexerFactory Default { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        /// <inheritdoc />
        public ILexer<HorizontalTab> Create()
        {
            var innerLexer = TerminalLexerFactory.Create("\x09", StringComparer.Ordinal);
            return new HorizontalTabLexer(innerLexer);
        }

        public ILexer<HorizontalTab> CreateOnce()
        {
            return instance ?? (instance = Create());
        }

        [NotNull]
        public HorizontalTabLexerFactory UseTerminalLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            return new HorizontalTabLexerFactory(terminalLexerFactory);
        }
    }
}
