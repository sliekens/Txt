using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public sealed class LineFeedLexerFactory : RuleLexerFactory<LineFeed>
    {
        static LineFeedLexerFactory()
        {
            Default = new LineFeedLexerFactory();
        }

        [NotNull]
        public static LineFeedLexerFactory Default { get; }

        public override ILexer<LineFeed> Create()
        {
            var innerLexer = Terminal.Create("\x0A", StringComparer.Ordinal);
            return new LineFeedLexer(innerLexer);
        }
    }
}
