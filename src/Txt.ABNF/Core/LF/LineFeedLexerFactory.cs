using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public sealed class LineFeedLexerFactory : LexerFactory<LineFeed>
    {
        static LineFeedLexerFactory()
        {
            Default = new LineFeedLexerFactory();
        }

        [NotNull]
        public static LineFeedLexerFactory Default { get; }

        public override ILexer<LineFeed> Create()
        {
            return new LineFeedLexer();
        }
    }
}
