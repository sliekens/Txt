using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public sealed class CarriageReturnLexerFactory : RuleLexerFactory<CarriageReturn>
    {
        static CarriageReturnLexerFactory()
        {
            Default = new CarriageReturnLexerFactory();
        }

        [NotNull]
        public static CarriageReturnLexerFactory Default { get; }

        public override ILexer<CarriageReturn> Create()
        {
            var innerLexer = Terminal.Create(@"\x0D", StringComparer.Ordinal);
            return new CarriageReturnLexer(innerLexer);
        }
    }
}
