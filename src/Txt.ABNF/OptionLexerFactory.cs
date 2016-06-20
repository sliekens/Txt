using System;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Creates instances of the <see cref="OptionLexer" /> class.</summary>
    public class OptionLexerFactory : IOptionLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Repetition> Create(ILexer<Element> lexer)
        {
            if (lexer == null)
            {
                throw new ArgumentNullException(nameof(lexer));
            }
            return new OptionLexer(lexer);
        }
    }
}
