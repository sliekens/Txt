using System;
using Txt;

namespace Text.ABNF
{
    /// <summary>Creates instances of the <see cref="OptionLexer" /> class.</summary>
    public class OptionLexerFactory : IOptionLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Repetition> Create(ILexer lexer)
        {
            if (lexer == null)
            {
                throw new ArgumentNullException(nameof(lexer));
            }
            return new OptionLexer(lexer);
        }
    }
}
