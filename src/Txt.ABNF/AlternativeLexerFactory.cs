﻿using System;
using Jetbrains.Annotations;
using Txt;

namespace Text.ABNF
{
    /// <summary>Creates instances of the <see cref="AlternativeLexer" /> class.</summary>
    public class AlternativeLexerFactory : IAlternativeLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Alternative> Create([ItemNotNull] params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            return new AlternativeLexer(lexers);
        }
    }
}