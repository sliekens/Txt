using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public sealed class DoubleQuoteLexerFactory : RuleLexerFactory<DoubleQuote>
    {
        static DoubleQuoteLexerFactory()
        {
            Default = new DoubleQuoteLexerFactory();
        }

        [NotNull]
        public static DoubleQuoteLexerFactory Default { get; }

        public override ILexer<DoubleQuote> Create()
        {
            var innerLexer = Terminal.Create("\x22", StringComparer.Ordinal);
            return new DoubleQuoteLexer(innerLexer);
        }
    }
}
