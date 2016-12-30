using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    public sealed class BitLexerFactory : RuleLexerFactory<Bit>
    {
        static BitLexerFactory()
        {
            Default = new BitLexerFactory();
        }

        [NotNull]
        public static BitLexerFactory Default { get; }

        public override ILexer<Bit> Create()
        {
            var innerLexer = Alternation.Create(
                Terminal.Create(@"0", StringComparer.Ordinal),
                Terminal.Create(@"1", StringComparer.Ordinal));
            return new BitLexer(innerLexer);
        }
    }
}
