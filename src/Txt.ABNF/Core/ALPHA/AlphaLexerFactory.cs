using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaLexerFactory : LexerFactory<Alpha>
    {
        static AlphaLexerFactory()
        {
            Default = new AlphaLexerFactory();
        }

        [NotNull]
        public static AlphaLexerFactory Default { get; }

        /// <inheritdoc />
        public override ILexer<Alpha> Create()
        {
            return new AlphaLexer();
        }
    }
}
