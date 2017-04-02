using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public sealed class CarriageReturnLexerFactory : LexerFactory<CarriageReturn>
    {
        static CarriageReturnLexerFactory()
        {
            Default = new CarriageReturnLexerFactory();
        }

        [NotNull]
        public static CarriageReturnLexerFactory Default { get; }

        public override ILexer<CarriageReturn> Create()
        {
            return new CarriageReturnLexer();
        }
    }
}
