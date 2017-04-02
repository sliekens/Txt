using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    public sealed class BitLexerFactory : LexerFactory<Bit>
    {
        static BitLexerFactory()
        {
            Default = new BitLexerFactory();
        }

        [NotNull]
        public static BitLexerFactory Default { get; }

        public override ILexer<Bit> Create()
        {
            return new BitLexer();
        }
    }
}
