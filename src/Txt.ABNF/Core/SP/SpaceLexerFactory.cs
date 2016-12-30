using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    public sealed class SpaceLexerFactory : RuleLexerFactory<Space>
    {
        static SpaceLexerFactory()
        {
            Default = new SpaceLexerFactory();
        }

        [NotNull]
        public static SpaceLexerFactory Default { get; }

        public override ILexer<Space> Create()
        {
            var innerLexer = Terminal.Create("\x20", StringComparer.Ordinal);
            return new SpaceLexer(innerLexer);
        }
    }
}
