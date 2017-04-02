using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    public sealed class SpaceLexerFactory : LexerFactory<Space>
    {
        static SpaceLexerFactory()
        {
            Default = new SpaceLexerFactory();
        }

        [NotNull]
        public static SpaceLexerFactory Default { get; }

        public override ILexer<Space> Create()
        {
            return new SpaceLexer();
        }
    }
}
