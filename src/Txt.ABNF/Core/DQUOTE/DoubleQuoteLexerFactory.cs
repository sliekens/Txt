using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public sealed class DoubleQuoteLexerFactory : LexerFactory<DoubleQuote>
    {
        static DoubleQuoteLexerFactory()
        {
            Default = new DoubleQuoteLexerFactory();
        }

        [NotNull]
        public static DoubleQuoteLexerFactory Default { get; }

        public override ILexer<DoubleQuote> Create()
        {
            return new DoubleQuoteLexer();
        }
    }
}
