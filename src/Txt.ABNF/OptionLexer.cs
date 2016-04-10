﻿using Jetbrains.Annotations;
using Txt;

namespace Text.ABNF
{
    /// <summary>Provides the base class for lexers whose lexer rule is an optional element.</summary>
    public class OptionLexer : RepetitionLexer
    {
        public OptionLexer([NotNull] ILexer lexer)
            : base(lexer, 0, 1)
        {
        }
    }
}