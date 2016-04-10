using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt;

namespace Text.ABNF.Core.CHAR
{
    /// <summary>Creates instances of the <see cref="CharacterLexer" /> class.</summary>
    public class CharacterLexerFactory : ILexerFactory<Character>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueRangeLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CharacterLexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Character> Create()
        {
            var valueRangeLexer = valueRangeLexerFactory.Create('\x01', '\x7F');
            return new CharacterLexer(valueRangeLexer);
        }
    }
}
